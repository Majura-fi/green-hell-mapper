using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using MapperUI.Properties;
using Newtonsoft.Json;

namespace MapperUI;

public partial class Form1 : Form
{   
    private readonly LocationServer locationServer = new();
    private readonly static int maxLogLength = 10000;

    public Form1()
    {
        InitializeComponent();
        locationServer.LocationReceived += OnLocationReceived;
        locationServer.LogProduced += OnLogProduced;
        locationServer.Start();

        FormClosing += (_, _) => {
            locationServer.Stop();
        };

        //LoadPreviousCoordinates();
    }

    private void OnLogProduced(object? sender, string e)
    {
        SafeInvoke(() => {
            AppendLog(e);
        });
    }

    private void OnLocationReceived(object? sender, PlayerInfo e)
    {
        SafeInvoke(() => { mapControl1.DrawMark(e, true); });
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
                    AppendLog($"Failed to parse json: \"{line}\"");
                    continue;
                }

                locationServer.AddLocation(info.Value);
            }
            catch (Exception ex)
            {
                AppendLog($"[{path}] {ex.Message} - Line: \"{line}\"");
            }
        }

        AppendLog($"Imported {path}");
    }

    private void AppendLog(string message)
    {
        string contents = $"{richTextBox1.Text}{message}{Environment.NewLine}";
        
        if (contents.Length > maxLogLength)
        {
            contents = contents[^maxLogLength..];
        }

        richTextBox1.Text = contents;
        richTextBox1.SelectionStart = richTextBox1.TextLength;
        richTextBox1.ScrollToCaret();
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

    private void LoadPreviousCoordinates()
    {
        DirectoryInfo srcDir = new(@"C:\Program Files (x86)\Steam\steamapps\common\Green Hell\SavedCoordinates");
        IEnumerable<FileInfo> files = srcDir.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);

        foreach (FileInfo file in files)
        {
            LoadCoordinatesFromFile(file.FullName);
        }
    }

    private void LoadCoordinatesMenuClick(object sender, EventArgs e)
    {
        using OpenFileDialog dialog = new() {
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
    }

    private void QuitMenuClick(object sender, EventArgs e)
    {
        Close();
    }

    private void PointSizeInput_ValueChanged(object sender, EventArgs e)
    {
        // pointSize = (int)pointSizeInput.Value;
    }
}
