using System.ComponentModel;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using MapperUI.Properties;
using Newtonsoft.Json;

namespace MapperUI;

public partial class Form1 : Form
{
    private readonly BackgroundWorker? worker;
    private Dictionary<int, PlayerInfo> playerLocations = [];
    private readonly List<Vector3> locations = [];
    private PointF offset = PointF.Empty;
    private PointF factor = new(1f, 1f);

    public Form1()
    {
        InitializeComponent();
        UpdateOffsetAndFactor();
        worker = new();
        worker.DoWork += WorkerThread;
        worker.WorkerSupportsCancellation = true;
        worker.RunWorkerAsync();

        FormClosing += (_, _) => {
            worker.CancelAsync();
        };
    }

    private void UpdateOffsetAndFactor()
    {
        try
        {
            offset.X = (float)offsetInputX.Value;
            offset.Y = (float)offsetInputY.Value;
            factor.X = (float)factorInputX.Value;
            factor.Y = (float)factorInputY.Value;
            RefreshMap();
        }
        catch (Exception)
        {
        }
    }

    private void LoadCoordinatesFromFile(string path)
    {
        using FileStream fs = new(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using StreamReader sr = new(fs);

        string? line;
        while ((line = sr.ReadLine()) != null)
        {
            try
            {
                PlayerInfo? info = JsonConvert.DeserializeObject<PlayerInfo>(line);

                if (info == null)
                {
                    AppendLog(string.Format("Failed to parse json: \"{0}\"", line));
                    continue;
                }

                locations.Add(info.Location);
            }
            catch (Exception ex)
            {
                AppendLog(ex.Message);
            }
        }
    }

    private async void WorkerThread(object? sender, DoWorkEventArgs e)
    {
        IPEndPoint local = new(IPAddress.Loopback, 11000);
        IPEndPoint remote = new(IPAddress.Loopback, 11001);
        using UdpClient client = new();
        client.Client.Bind(local);

        while (worker != null && !worker.CancellationPending)
        {
            try
            {
                if (!client.Client.Connected)
                {
                    client.Connect(remote);
                }

                UdpReceiveResult results = await client.ReceiveAsync();

                if (results.Buffer.Length > 0)
                {
                    string payloadStr = Encoding.ASCII.GetString(results.Buffer);
                    PlayerInfo? info = JsonConvert.DeserializeObject<PlayerInfo>(payloadStr);

                    if (info == null)
                    {
                        SafeInvoke(() => {
                            AppendLog(string.Format("Failed to parse json: \"{0}\"", payloadStr));
                        });
                        continue;
                    }

                    lock (playerLocations)
                    {
                        if (!playerLocations.TryAdd(info.PlayerId, info))
                        {
                            playerLocations[info.PlayerId] = info;
                        }
                        locations.Add(info.Location);
                    }

                    SafeInvoke(() => {
                        AppendLog(payloadStr);
                        RefreshMap();
                    });
                }
            }
            catch (Exception ex)
            {
                SafeInvoke(() => AppendLog(ex.Message));
                await Task.Delay(1000);
            }
        }
    }

    private void AppendLog(string message)
    {
        richTextBox1.AppendText(message + Environment.NewLine);
        richTextBox1.SelectionStart = richTextBox1.TextLength;
        richTextBox1.ScrollToCaret();
    }

    private void RefreshMap()
    {
        Bitmap map = (Bitmap)Resources.map.Clone();
        DrawMarks(map);

        pictureBox1.Image?.Dispose();
        pictureBox1.Image = map;
        pictureBox1.Refresh();
    }

    private void DrawMarks(Bitmap img, bool drawCurrentLocations = true)
    {
        using Graphics g = Graphics.FromImage(img);

        List<Vector3> tmpPlayerLocations;
        Vector3[] tmpLocations;

        // Grab a copy so that we don't run into problems when the collection
        // changes.
        lock (playerLocations)
        {
            tmpLocations = [.. locations];
            tmpPlayerLocations = playerLocations
                .Select(pair => pair.Value.Location).ToList();
        }

        // Draw history locations.
        foreach (Vector3 location in tmpLocations)
        {
            Vector3 loc = location;
            loc.X *= factor.X; 
            loc.Z *= factor.Y;
            loc.X += offset.X;
            loc.Z += offset.Y;

            RectangleF point = new(loc.X - 5f, loc.Z - 5f, 10f, 10f);
            g.FillEllipse(Brushes.Blue, point);
        }

        // These are already included in the tmpLocations, but drawn with
        // red dots on the map.
        if (drawCurrentLocations)
        {
            // Draw current player locations.
            foreach (Vector3 location in tmpPlayerLocations)
            {
                Vector3 loc = location;
                loc.X *= factor.X;
                loc.Z *= factor.Y;
                loc.X += offset.X;
                loc.Z += offset.Y;

                RectangleF point = new(loc.X - 10f, loc.Z - 10f, 20f, 20f);
                g.FillEllipse(Brushes.Red, point);
            }
        }
    }

    private void SafeInvoke(Action action)
    {
        try
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }
        catch (Exception)
        {
        }
    }

    private void LoadCoordinatesMenuClick(object sender, EventArgs e)
    {
        OpenFileDialog dialog = new() {
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = true
        };

        if (
            !string.IsNullOrWhiteSpace(Settings.Default.RecentCoordinatesListPath)
            && Directory.Exists(Settings.Default.RecentCoordinatesListPath)
        )
        {
            dialog.InitialDirectory = Settings.Default.RecentCoordinatesListPath;
        }

        if (dialog.ShowDialog() != DialogResult.OK || dialog.FileNames.Length == 0)
        {
            return;
        }

        Settings.Default.RecentCoordinatesListPath = Path.GetDirectoryName(dialog.FileNames[0]);
        Settings.Default.Save();

        foreach (string path in dialog.FileNames)
        {
            LoadCoordinatesFromFile(path);
        }
        RefreshMap();
    }

    private void QuitMenuClick(object sender, EventArgs e)
    {
        Close();
    }

    private void TweakValueChanged(object sender, EventArgs e)
    {
        UpdateOffsetAndFactor();
    }
}
