using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MapperSource
{
    public class MapClient
    {
        private readonly IPEndPoint remote = new(IPAddress.Loopback, 11000);
        private readonly IPEndPoint local = new(IPAddress.Loopback, 11001);
        private UdpClient? client;

        public void Start()
        {
            if (client != null)
            {
                return;
            }

            Console.WriteLine("Starting MapClient!");
            client = new();
            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.Client.Bind(local);
        }

        public void Stop()
        {
            Console.WriteLine("Stopping MapClient!");
            client?.Close();
            client = null;
        }

        public void Send(string message)
        {
            if (client == null)
            {
                throw new InvalidOperationException();
            }

            if (!client.Client.Connected)
            {
                client.Connect(remote);
            }

            try
            {
                byte[] payload = Encoding.ASCII.GetBytes(message);
                client.Send(payload, payload.Length);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}