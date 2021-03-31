using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NgwTask.Common;

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
            var text = "测试发送";
            clientSocket.Send(new MessageProtocol {Length = (byte) text.Length, Head = 1, Text = text}.ToByte());
            //while (true)
            //{
            //    Thread.Sleep(5000);
            //}
            socket.Close();
            clientSocket.Close();
            Console.WriteLine();
        }
    }
}
