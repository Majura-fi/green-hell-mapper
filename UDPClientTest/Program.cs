using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
    Random rnd = new();

    while (worker != null && !worker.CancellationPending)
    {
        try
        {
            if (!client.Client.Connected)
            {
                client.Connect(remote);
            }

            string x = (rnd.NextSingle() * 800f + 400f).ToString("0.000", CultureInfo.InvariantCulture);
            string y = rnd.NextSingle().ToString("0.000", CultureInfo.InvariantCulture);
            string z = (rnd.NextSingle() * 1000f + 1400f).ToString("0.000", CultureInfo.InvariantCulture);
            string payloadStr = $"{{ \"PlayerId\": {rnd.Next(0, 3)}, \"Location\": {{ \"X\": {x}, \"Y\": {y}, \"Z\": {z} }} }}";
            Console.WriteLine(">" + payloadStr);

            byte[] payloadBytes = Encoding.ASCII.GetBytes(payloadStr);
            await client.SendAsync(payloadBytes, payloadBytes.Length);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        await Task.Delay(50);
    }
}

Console.ReadLine();
