using System;
using System.Net;
using System.Net.Sockets;
using NgwTask.Common;

namespace NgwTask.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(Config.Ip), Config.Port));
            Console.WriteLine("连接服务器成功");
            var receive = new byte[1024];
            socket.Receive(receive);
            Console.ReadLine();
        }
    }
}
