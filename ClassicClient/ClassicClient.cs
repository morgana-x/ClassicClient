using System.Net.Sockets;
using ClassicConnect.Event;
using ClassicConnect.Network;
using ClassicConnect.Player;

namespace ClassicConnect
{
    public class ClassicClient
    {
        public string Name;
        string Verification;
   
        public ClassicServer ConnectedServer = new ClassicServer();

        public ClassicPlayer LocalPlayer;
        public ClassicPlayerList PlayerList;
        public ClassicLevel Level = new ClassicLevel();

        public EventManager Events = new EventManager();

        public TcpClient Client;
        internal NetworkStream NetworkStream;

        private Task connectionTask;

        public ClassicClient(string name, string mppass="")
        {
            Name = name;
            ClassicPacketHandler.RegisterPackets();
            LocalPlayer = new ClassicPlayer(-1, name, 0, 0, 0, 0, 0);
            PlayerList = new ClassicPlayerList(this,LocalPlayer);
            Verification = mppass;
        }

        public void Connect(string ip)
        {
            int port = 25566;
            if (ip.Contains(":"))
            {
                string[] split = ip.Split(":");
                ip = split[0];
                port = int.Parse(split[1]);
            }
            Connect(ip, port);
        }
        public void Connect(string ip, int port=25566)
        {
            
            Client = new TcpClient(ip, port);
            NetworkStream = Client.GetStream();

            SendBytes(Network.Core.ClientIdentify.GetBytes(this.Name, Verification, true));
            ClassicPacketHandler.ReadPacket(this, NetworkStream);

            connectionTask = Task.Run(ReadThread);
        }

        public void Disconnect()
        {
            if (!Client.Connected) return;

            Client.Close();
            //connectionTask.Dispose();
        }
        public void SendBytes(byte[] bytes)
        {
            if (!Client.Connected) return;
            Console.WriteLine("Sending bytes " + string.Join(", ", bytes));
            NetworkStream.Write(bytes);
        }

        public void SendMessage(string message)
        {
            SendBytes(Network.Player.Message.GetBytes(message));
        }

        public void PlaceBlock(short x, short y, short z, byte block)
        {
            Level.SetBlock(x, y, z, block);
            SendBytes(Network.Level.SetBlock.GetBytes(x, y, z, block, false));
        }
        public void BreakBlock(short x, short y, short z, byte block)
        {
            Level.SetBlock(x, y, z, 0);
            SendBytes(Network.Level.SetBlock.GetBytes(x, y, z, block, true));
        }

        DateTime nextSendPosition = DateTime.Now;
        public async Task ReadThread()
        {
            while (Client.Connected)
            {
                bool success = -1 != ClassicPacketHandler.ReadPacket(this, this.NetworkStream);
                if (!success)
                {
                    Disconnect();
                    break;
                }

                if (DateTime.Now > nextSendPosition)
                {
                    nextSendPosition = DateTime.Now.AddMilliseconds(50);
                    SendBytes(Network.Player.Teleport.GetBytes(LocalPlayer.X, LocalPlayer.Y, LocalPlayer.Z, LocalPlayer.Yaw, LocalPlayer.Pitch));
                }
            }
            Console.WriteLine("Connection closed");
        }
    }
}
