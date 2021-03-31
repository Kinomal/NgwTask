using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NgwTask.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 3061;
            const string ip = "127.0.0.1";
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            socket.Listen(10);
            Console.WriteLine("启动监听{0}成功", socket.LocalEndPoint);
            var clientSocket = socket.Accept();
            Console.WriteLine("客户端{0}成功连接", clientSocket.RemoteEndPoint);
            while (true)
            {
                Thread.Sleep(5000);
            }
        }
    }
}
