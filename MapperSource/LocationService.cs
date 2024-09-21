using System.ComponentModel;
using Mapper;
using UnityEngine;

namespace MapperSource;

public class LocationService
{
    public static LocationService Instance
    {
        get {
            instance ??= new LocationService();
            return instance;
        }
    }
    private static LocationService? instance;
    private BackgroundWorker? worker;
    private readonly Dictionary<int, PlayerInfo> playerInfos = [];
    private readonly Dictionary<int, PlayerInfo> sentPlayerInfos = [];
    private FileStream? outputStream;
    private StreamWriter? outputWriter;

    private LocationService()
    {
    }

    public void UpdateLocation(int playerId, Vector3 location)
    {
        if (!playerInfos.TryAdd(playerId, new(playerId, location)))
        {
            playerInfos[playerId] = new(playerId, location);
        }
    }

    public void Start()
    {
        if (worker != null)
        {
            return;
        }

        worker = new BackgroundWorker();
        worker.DoWork += WorkerThread;
        worker.WorkerSupportsCancellation = true;
        worker.RunWorkerAsync();
    }

    private void WorkerThread(object sender, DoWorkEventArgs e)
    {
        MapClient client = new();
        client.Start();

        while (!worker!.CancellationPending)
        {
            if (!Plugin.Enabled)
            {
                Thread.Sleep(1000);
                continue;
            }

            try
            {
                foreach (PlayerInfo info in playerInfos.Values)
                {
                    bool shouldSend = true;

                    if (sentPlayerInfos.TryGetValue(info.PlayerId, out PlayerInfo previousInfo))
                    {
                        float diff = Math.Abs((info.Location - previousInfo.Location).magnitude);
                        // One meter
                        shouldSend = diff >= 1f;
                    }

                    if (shouldSend)
                    {
                        string payloadStr = info.ToJSON();
                        WriteToFile(payloadStr);
                        client.Send(payloadStr);

                        if (!sentPlayerInfos.TryAdd(info.PlayerId, info))
                        {
                            sentPlayerInfos[info.PlayerId] = info;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            Thread.Sleep(100);
        }

        client.Stop();
    }

    private void WriteToFile(string payload)
    {
        outputStream ??= new(
            string.Format("coordinates {0}.txt", DateTime.Now.ToString("yyyy-MM-dd HH-mm")), 
            FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        outputWriter ??= new(outputStream) {
            AutoFlush = true
        };
        outputWriter.WriteLine(payload);
    }

    public void Stop()
    {
        worker?.CancelAsync();
        worker?.Dispose();
        worker = null;
        
        outputWriter?.Dispose();
        outputWriter = null;

        outputStream?.Dispose();
        outputStream = null;
    }
}