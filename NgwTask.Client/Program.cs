using System;
using System.Net;
using System.Net.Sockets;

namespace NgwTask.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 3061;
            const string ip = "127.0.0.1";
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            Console.WriteLine("连接服务器成功");
            Console.ReadLine();
        }
    }
}
