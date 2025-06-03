using System.Net.Sockets;
using ClassicConnect.Command;
using ClassicConnect.Event;
using ClassicConnect.Network;
using ClassicConnect.Player;

namespace ClassicConnect
{
    public class ClassicClient
    {
        public string Name;
        string Verification = "None";
   
        public ClassicServer ConnectedServer = new ClassicServer();

        public ClassicPlayer LocalPlayer;
        public ClassicPlayerList PlayerList;
        public ClassicLevel Level = new ClassicLevel();
        public CommandHandler CommandHandler;
        public EventManager Events = new EventManager();

        public TcpClient Client;
        internal NetworkStream NetworkStream;

        private Task connectionTask;
        private Task writeConnectionTask;

        public CancellationTokenSource cancelToken = new CancellationTokenSource();
        public ClassicClient(string name)
        {
            Name = name;
            ClassicPacketHandler.RegisterPackets();
            LocalPlayer = new ClassicPlayer(this, -1, name, 0, 0, 0, 0, 0);
            PlayerList = new ClassicPlayerList(this,LocalPlayer);
            CommandHandler = new CommandHandler(this);
            Rank.Load(Path.Join(AppContext.BaseDirectory, "ranks.data"));
        }

        public ClassicClient(string name, string password)
        {
            Name = name;
            ClassicubeAPI.Login(name, password);
            ClassicPacketHandler.RegisterPackets();
            LocalPlayer = new ClassicPlayer(this, -1, name, 0, 0, 0, 0, 0);
            PlayerList = new ClassicPlayerList(this, LocalPlayer);

        }

        public bool Connect(string ip, string mppass="")
        {
            int port = 25565;
            if (ip.Contains(":"))
            {
                string[] split = ip.Split(":");
                ip = split[0];
                port = int.Parse(split[1]);
            }
            return Connect(ip, port, mppass);
        }
        public bool Connect(string ip, int port=25566, string mppass="")
        {
            if (mppass == "")
                mppass = Verification;
            try
            {
                Client = new TcpClient(ip, port);
                NetworkStream = Client.GetStream();

                SendBytes(Network.Core.ClientIdentify.GetBytes(this.Name, mppass, true));
                ClassicPacketHandler.ReadPacket(this, NetworkStream);

                connectionTask = Task.Run(ReadThread);
                writeConnectionTask = Task.Run(WriteThread);

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error logging in to server {ip}:{port}: " + e.ToString());
                return false;
            }
        }

        public bool ConnectClassicube(string hash, string mppass="")
        {
            var serverlist = ClassicubeAPI.GetServerInfo(hash);
            if (serverlist.Count == 0)
            {
                Console.WriteLine($"Couldn't find server with hash {hash}");
                return false;
            }
            var server = serverlist[0];

            Console.WriteLine(server.name);
            Console.WriteLine(server.ip);

            Verification = mppass == "" ?  server.mp_pass : mppass;

            Console.WriteLine($"Verification pass is now {Verification}");

            return Connect(server.ip, server.port, Verification);
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
           // Console.WriteLine("Sending bytes " + string.Join(", ", bytes));
            NetworkStream.Write(bytes);
            NetworkStream.Flush();
        }

        public void SendMessage(string message)
        {
            //Console.WriteLine("Sending message " + message);
            SendBytes(Network.Player.Message.GetBytes(message));
            //Console.WriteLine("Sent?");
        }

        public void PlaceBlock(short x, short y, short z, byte block)
        {
            if (!Level.ValidPos(x, y, z)) return;
            if (Level.GetBlock(x, y, z) == block) return;
            Level.SetBlock(x, y, z, block);
            SendBytes(Network.Level.SetBlock.GetBytes(x, y, z, block, false));
        }
        public void BreakBlock(short x, short y, short z, byte block)
        {
            if (!Level.ValidPos(x, y, z)) return;
            if (Level.GetBlock(x, y, z) == 0) return;
            Level.SetBlock(x, y, z, 0);
            SendBytes(Network.Level.SetBlock.GetBytes(x, y, z, block, true));
        }

        public void ModifyBlock(short x, short y, short z, byte block)
        {
            if (!Level.ValidPos(x, y, z)) return;
            if (Level.GetBlock(x, y, z) == block) return;
            BreakBlock(x, y, z, block);
            if (block == 0) return;
            PlaceBlock(x, y, z, block);
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
            }
            Console.WriteLine("Connection closed");
        }
        public async Task WriteThread()
        {
            while (Client.Connected)
            {
                if (DateTime.Now > nextSendPosition && !Level.Loading)
                {
                    nextSendPosition = DateTime.Now.AddMilliseconds(40);
                    SendBytes(Network.Player.Teleport.GetBytes(LocalPlayer));
                }
            }
        }

        public void CancelTasks()
        {
            cancelToken.Cancel();
        }

    }
}
