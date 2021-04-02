using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using NgwTask.Common;

namespace NgwTask.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse(Config.Ip), Config.Port));
            socket.Listen(10);
            Console.WriteLine("启动监听{0}成功", socket.LocalEndPoint);
            var random = new Random();
            while (true)
            {
                var clientSocket = socket.Accept();
                Console.WriteLine("客户端{0}成功连接", clientSocket.RemoteEndPoint);
                while (true)
                {
                    for (var i = 0; i < 100; i++)
                    {
                        var flag = Guid.NewGuid().ToString("N").Substring(0, random.Next(1, 33));
                        var text =
                            $@"近段时间以来，主要谷物国际价格同比大幅度上涨。其中2月份，高粱价格涨幅超过80%，玉米价格同比上涨45.5%，小麦价格涨幅接近两成。这也让不少人担心，全球粮食上涨会不会影响到中国人的“饭碗”呢？对此，国家粮食和物资储备局31日表示，当前我国粮食库存充实、供给充裕，保障粮食市场供应和平稳运行有基础有条件。从综合生产能力来看，我国粮食总产量已连续6年稳定在13000亿斤以上。今年冬小麦播种面积有所增加，夏粮丰收基础较好。国家粮油信息中心首席分析师 王晓辉：单产方面，春节后北方冬麦区气温偏高，利于冬小麦安全越冬和萌动返青；降水明显增加，利于冬小麦生长。今年新小麦面积、单产和产量有望实现齐增，丰收在望。从人均占有量来看，我国人均粮食占有量超过470公斤，远高于人均400公斤的国际粮食安全标准线。综合粮食生产、库存、贸易等因素，我国粮食安全形势总体向好，保障粮食市场供应和平稳运行有基础有条件。{i}_{flag}";
                        clientSocket.Send(BuildData(1, text));
                        Console.WriteLine($"已发送 {text}");
                        Console.WriteLine("");
                    }
                    Thread.Sleep(5000);
                }
            }
        }

        private static byte[] BuildData(byte head, string text)
        {
            var textBytes = Encoding.UTF8.GetBytes(text);
            var totalByte = new byte[Config.HeadByteLength + Config.DataLengthByteLength + textBytes.Length];
            //头部
            BitConverter.GetBytes(head).CopyTo(totalByte, 0);
            //长度
            BitConverter.GetBytes(textBytes.Length).CopyTo(totalByte, Config.HeadByteLength);
            //内容
            textBytes.CopyTo(totalByte, Config.HeadByteLength + Config.DataLengthByteLength);
            return totalByte;
        }
    }
}
