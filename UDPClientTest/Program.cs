using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using UDPClientTest;

BackgroundWorker worker = new();
worker.DoWork += WorkerThread;
worker.WorkerSupportsCancellation = true;
worker.RunWorkerAsync();

Console.CancelKeyPress += (_, _) => {
    worker.CancelAsync();
};

async void WorkerThread(object? sender, DoWorkEventArgs e)
{
    IPEndPoint remote = new(IPAddress.Loopback, 11000);
    IPEndPoint local = new(IPAddress.Loopback, 11001);
    using UdpClient client = new();
    client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
    client.Client.Bind(local);

    List<SimulatedPlayer> players = [];
    for (int i = 0; i < 5; i++)
    {
        players.Add(new(i));
    }

    while (worker != null && !worker.CancellationPending)
    {
        try
        {
            if (!client.Client.Connected)
            {
                client.Connect(remote);
            }

            foreach (SimulatedPlayer player in players)
            {
                player.Update();
                string payloadStr = player.ToJSON();
                Console.WriteLine(">" + payloadStr);

                byte[] payloadBytes = Encoding.ASCII.GetBytes(payloadStr);
                await client.SendAsync(payloadBytes, payloadBytes.Length);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        await Task.Delay(50);
    }
}

Console.ReadLine();
