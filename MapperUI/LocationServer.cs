using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Numerics;

namespace MapperUI;
internal class LocationServer
{
    public event EventHandler<PlayerInfo> LocationReceived = delegate { };
    public event EventHandler<string> LogProduced = delegate { };

    private BackgroundWorker? worker;
    private readonly List<Vector3> locations = [];

    private static readonly PointF offset = new(-108f, 5310f);
    private static readonly PointF factor = new(2.53f, -2.74f);

    public LocationServer() 
    {
    }

    public void Start()
    {
        if (worker != null)
        {
            return;
        }

        worker = new();
        worker.DoWork += WorkerThread;
        worker.WorkerSupportsCancellation = true;
        worker.RunWorkerAsync();
    }

    public void Stop()
    {
        worker?.CancelAsync();
        worker?.Dispose();
        worker = null;
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
                    PlayerInfo? info;

                    try
                    {
                        info = JsonConvert.DeserializeObject<PlayerInfo>(payloadStr);
                        if (info == null)
                        {
                            LogProduced(this, string.Format("Failed to parse json: \"{0}\"", payloadStr));
                            continue;
                        }
                    }
                    catch (Exception)
                    {
                        LogProduced(this, string.Format("Failed to parse json: \"{0}\"", payloadStr));
                        continue;
                    }

                    info = PopulateMapLocation(info.Value);

                    lock (locations)
                    {
                        locations.Add(info.Value.Location);
                    }

                    LogProduced(this, payloadStr);
                    LocationReceived(this, info.Value);
                }
            }
            catch (Exception ex)
            {
                LogProduced(this, ex.Message);
            }
        }
    }

    private static PlayerInfo PopulateMapLocation(PlayerInfo info)
    {
        // Apply scale and offset for the location.
        // Note that Z and Y is swapped around.
        return new() {
            PlayerId = info.PlayerId,
            Location = info.Location,
            MapLocation = info.MapLocation = new(
                (info.Location.X * factor.X) + offset.X,
                (info.Location.Z * factor.Y) + offset.Y,
                info.Location.Y
            )
        };
    }

    public void AddLocation(PlayerInfo info)
    {
        info = PopulateMapLocation(info);

        lock (locations)
        {
            locations.Add(info.Location);
        }

        LocationReceived(this, info);
    }
}
