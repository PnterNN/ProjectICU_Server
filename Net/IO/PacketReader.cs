using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectICU_Server.Net.IO
{
    internal class PacketReader : BinaryReader
    {
        private NetworkStream _ns;
        public PacketReader(NetworkStream ns) : base(ns)  // sunucu ile client arası mesajlaşma için paket okuyucu
        {
            _ns = ns;
        }

        public string ReadMessage()
        {
            byte[] msgBuffer;
            var length = ReadInt32();
            msgBuffer = new byte[length];
            _ns.Read(msgBuffer, 0, length);

            var msg = Encoding.UTF8.GetString(msgBuffer);
            _ns.Flush();
            return msg;
        }

    }
}
