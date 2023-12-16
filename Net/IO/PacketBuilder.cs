using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProjectICU_Server.Net.IO
{
    internal class PacketBuilder // sunucu ile client arası mesajlaşma için paket oluşturucu
    {
        MemoryStream _ms;
        public PacketBuilder()
        {
            _ms = new MemoryStream(); 
        }

        public void WriteOpCode(byte opcode)
        {
            _ms.WriteByte(opcode);
        }

        public void WriteMessage(string msg)  // linki unutma Kubilay
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(msg);
            byte[] lengthBytes = BitConverter.GetBytes(messageBytes.Length);
            _ms.Write(lengthBytes, 0, lengthBytes.Length);
            _ms.Write(messageBytes, 0, messageBytes.Length);
        }

        public byte[] GetPacketBytes()
        {
                var sendPacket = _ms.ToArray();
                Clear(_ms);
                return sendPacket;
        }

        public void Clear(MemoryStream source)
        {
            byte[] buffer = source.GetBuffer();
            Array.Clear(buffer, 0, buffer.Length);
            source.Position = 0;
            source.SetLength(0);
        }
    }
}
