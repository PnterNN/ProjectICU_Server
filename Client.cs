using ProjectICU_Server.Net.IO;
using ProjectICU_Server.Net.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProjectICU_Server
{
    // username, password, uid, email, IP Address, TcpClient 


    internal class Client
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UID { get; set; }
        public string IPAdress { get; set; }
        public TcpClient ClientSocket { get; set; }

        PacketReader _packetReader;
        public Client(TcpClient client)
        {
            ClientSocket = client;
            _packetReader = new PacketReader(ClientSocket.GetStream()); // client nesnesine gelen mesajları okumak için paket okuyucu
            IPAdress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(); // client nesnesine gelen bağlantının IP adresini ata
            Console.WriteLine("[" + DateTime.Now + "]: [/" + IPAdress + "] Bağlantı kuruldu.");
            MySqlDataBase sql = new MySqlDataBase();
            bool status = false;
            while (!status)// login ve ya register için paketleri okucaz
            {
                var opcode = _packetReader.ReadByte();
                switch (opcode)
                {
                    case 0: // login
                        Email = _packetReader.ReadMessage();
                        Password = _packetReader.ReadMessage();
                        Console.WriteLine("[" + DateTime.Now + "]: [/" + IPAdress + "] Giriş bilgileri kontrol ediliyor... ");
                        if (sql.CheckLoginUser(Email, Password))
                        {
                            Console.WriteLine("[" + DateTime.Now + "]: [/" + IPAdress + "] Giriş başarılı.");
                            status = true;
                            Username = sql.getUsername(Email);
                            UID = sql.getUID(Email);
                            Program.sendLoginInfo(this);

                        }
                        else
                        {
                              Console.WriteLine("[" + DateTime.Now + "]: [/" + IPAdress + "] Giriş başarısız.");
                              status = false;
                        }
                       
                        break;
                    case 1: // register

                        break;
                }

            }





        }
    }
}
