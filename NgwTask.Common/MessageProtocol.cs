using System.IO;
using System.Text;

namespace NgwTask.Common
{
    public class MessageProtocol
    {
        public byte Head { get; set; }

        public byte Length { get; set; }

        public string Text { get; set; }

        public byte[] ToByte()
        {
            using var memoryStream = new MemoryStream();
            var binaryWriter = new BinaryWriter(memoryStream);
            binaryWriter.Write(Head);
            binaryWriter.Write(Length);
            binaryWriter.Write(Text);
            var bytes = memoryStream.ToArray();
            binaryWriter.Close();
            return bytes;
        }

        public static MessageProtocol FromBytes(byte[] bytes)
        {
            var messageProtocol = new MessageProtocol();
            using var memoryStream = new MemoryStream(bytes);
            var binaryReader = new BinaryReader(memoryStream);
            messageProtocol.Head = binaryReader.ReadByte();
            messageProtocol.Length = binaryReader.ReadByte();
            messageProtocol.Text = Encoding.UTF8.GetString(binaryReader.ReadBytes(messageProtocol.Length));
            return messageProtocol;
        }
    }
}