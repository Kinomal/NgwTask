namespace NgwTask.Common
{
    public class Config
    {
        //监听端口
        public const int Port = 3061;
        //监听ip
        public const string Ip = "127.0.0.1";
        //头部字节长度
        public const int HeadByteLength = 1;
        //数据长读字节长度
        public const int DataLengthByteLength = 4;
        //日志文件目录
        public const string FileName = "/ReceiveData.txt";
    }
}