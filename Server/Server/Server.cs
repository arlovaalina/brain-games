using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class Server
    {
        private Socket Listener;
        private IPEndPoint ipEndPoint;
        private IPHostEntry ipHost;

        public Server(string ipAddress, int port)
        {
            ipHost = Dns.GetHostEntry(ipAddress);
            ipEndPoint = new IPEndPoint(ipHost.AddressList[0], port);
            Listener = new Socket(ipHost.AddressList[0].AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Listener.Bind(ipEndPoint);
            Listener.Listen(10);
            Listen();
        }

        public void Listen()
        {
            while (true)
            {
                Console.WriteLine("Ожидаем соединение...");
                Socket handler = Listener.Accept();
                ClientProcessing process = new ClientProcessing(handler);
            }
        }
    }
}
