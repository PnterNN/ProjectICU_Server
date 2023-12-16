using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProjectICU_Server
{
    internal class Program
    {
        static TcpListener _listener;
        static List<Client> _clients;

        static void Main(string[] args)
        {
            _clients = new List<Client>();

            _listener = new TcpListener(IPAddress.Any, 9001); // 9001 portundan gelen bağlantıları dinle
            _listener.Start();

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    var entry = _listener.AcceptTcpClientAsync(); // gelen bağlantıyı kabul et
                    var client = new Client(await entry); // gelen bağlantıyı client nesnesine ata
                }
            });
            Console.Read();
        }
    }
}
