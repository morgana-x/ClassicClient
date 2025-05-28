using System.Net.Sockets;
using ClassicConnect.Event;
using ClassicConnect.Network;

namespace ClassicConnect
{
    public class ClassicClient
    {
        public string Name;
   
        public ClassicServer ConnectedServer = new ClassicServer();
        public EventManager Events = new EventManager();
        internal TcpClient Client;
        internal NetworkStream NetworkStream;
        private Task connectionTask;

        public ClassicClient(string name)
        {
            this.Name = name;
            ClassicPacketHandler.RegisterPackets();
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

            SendBytes(Network.Core.ClientIdentify.GetBytes(this.Name, "I love anime"));

            connectionTask = Task.Run(ReadThread);
        }

        public void Disconnect()
        {
            Client.Close();
            connectionTask.Dispose();
        }
        public void SendBytes(byte[] bytes)
        {
            NetworkStream.Write(bytes);
        }

        public void SendMessage(string message)
        {
            SendBytes(Network.Player.Message.GetBytes(message));
        }

        public async Task ReadThread()
        {
            while (Client.Connected)
            {
                bool success = ClassicPacketHandler.ReadPacket(this, this.NetworkStream);
                if (!success)
                {
                    Disconnect();
                    break;
                }
            }
        }
    }
}
