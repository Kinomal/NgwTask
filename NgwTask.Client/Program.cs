using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
            byte[] lastRemainBytes = null;
            while (true)
            {
                var receiveBytes = new byte[1024];
                socket.Receive(receiveBytes);
                Console.WriteLine($"接收数据");
                using var memoryStream = new MemoryStream();
                if (lastRemainBytes != null)
                {
                    memoryStream.Write(lastRemainBytes);
                }
                memoryStream.Write(receiveBytes);
                lastRemainBytes = ReceiveData(memoryStream.ToArray());
            }
        }

        private static byte[] ReceiveData(byte[] bytes)
        {
            //数据长度不足无法解析出包头和长度
            if (bytes == null || bytes.Length < Config.HeadByteLength + Config.DataLengthByteLength)
            {
                return bytes;
            }
            using var memoryStream = new MemoryStream(bytes);
            var binaryReader = new BinaryReader(memoryStream);
            //解析包头
            var head = binaryReader.ReadBytes(Config.HeadByteLength)[0];
            if (head != 1)
            {
                //当前数据处理完成
                return null;
            }
            //解析长度
            var length = BitConverter.ToInt16(binaryReader.ReadBytes(Config.DataLengthByteLength));
            //接收包体,若当前数据不完整，则和下次接收数据拼接后再解析
            if (bytes.Length < Config.HeadByteLength + Config.DataLengthByteLength + length)
            {
                return bytes;
            }
            var text = Encoding.UTF8.GetString(binaryReader.ReadBytes(length));
            //剩余数据
            byte[] remainBytes = null;
            var remainLength = bytes.Length - Config.HeadByteLength - Config.DataLengthByteLength - length;
            if (remainLength > 0)
            {
                remainBytes = binaryReader.ReadBytes(remainLength);
            }
            Console.WriteLine($"{head} {length} {text}");
            Console.WriteLine("");
            //写入文件
            File.AppendAllText($"{Directory.GetCurrentDirectory()}/{Config.FileName}", text);
            return ReceiveData(remainBytes);
        }
    }
}
