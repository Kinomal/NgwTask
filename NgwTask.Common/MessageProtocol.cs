using System;
using System.IO;
using System.Text;

namespace NgwTask.Common
{
    public class MessageProtocol
    {
        public byte Head { get; set; }

        public string Text { get; set; }

        public byte[] ToByte()
        {
            //using var memoryStream = new MemoryStream();
            //var binaryWriter = new BinaryWriter(memoryStream);
            //binaryWriter.Write(Head);
            //binaryWriter.Write(Text.Length);
            //binaryWriter.Write(Encoding.UTF8.GetBytes(Text));
            //var bytes = memoryStream.ToArray();
            //binaryWriter.Close();
            //return bytes;
            var textBytes = Encoding.UTF8.GetBytes(Text);
            var totalByte = new byte[1 + 4 + textBytes.Length];
            BitConverter.GetBytes(Head).CopyTo(totalByte, 0);
            BitConverter.GetBytes(totalByte.Length).CopyTo(totalByte, 1);
            textBytes.CopyTo(totalByte, 1+ 4);
            return totalByte;
        }

        public static MessageProtocol FromBytes(byte[] buffer)
        {
            var messageProtocol = new MessageProtocol();
            using var memoryStream = new MemoryStream(buffer);
            var binaryReader = new BinaryReader(memoryStream);
            messageProtocol.Head = binaryReader.ReadByte();
            var length = BitConverter.ToInt16(binaryReader.ReadBytes(4));
            messageProtocol.Text = Encoding.UTF8.GetString(binaryReader.ReadBytes(length));
            return messageProtocol;
        }
    }
}