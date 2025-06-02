namespace ClassicConnect.Network
{
    public class ClassicPacketHandler
    {
        public static Dictionary<byte, ClassicPacket> ReadPackets = new Dictionary<byte, ClassicPacket>();
        
        public static int ReadPacket(ClassicClient client, Stream stream)
        {
            byte id = (byte)stream.ReadByte();

            if (!ReadPackets.ContainsKey(id))
            {
                Console.WriteLine($"Received unknown packet 0x{id.ToString("X")}!");
                return -1;
            }
           // Console.WriteLine("Reading packet 0x" + id.ToString("X"));
            ReadPackets[id].Read(client, stream);
            stream.Flush();
            return id;
        }

        public static void RegisterReadPacket(byte id, ClassicPacket packet)
        {
            if (ReadPackets.ContainsKey(id))
            {
                ReadPackets[id] = packet;
                return;
            }

            ReadPackets.Add(id, packet);
        }

        public static void RegisterPacket(ClassicPacket packet)
        {
            RegisterReadPacket(packet.PacketID, packet);
        }

        public static void RegisterPackets()
        {
            RegisterPacket(new Core.ServerIdentify());
            RegisterPacket(new Core.Ping());
            RegisterPacket(new Level.LevelInitialize());
            RegisterPacket(new Level.LevelChunk());
            RegisterPacket(new Level.LevelFinalize());
            RegisterPacket(new Level.ServerSetBlock());
            RegisterPacket(new Player.Spawn());
            RegisterPacket(new Player.Teleport());
            RegisterPacket(new Player.PositionOrientationUpdate());
            RegisterPacket(new Player.PositionUpdate());
            RegisterPacket(new Player.OrientationUpdate());
            RegisterPacket(new Player.Despawn());
            RegisterPacket(new Player.Message());
            RegisterPacket(new Core.Disconnect());
            RegisterPacket(new Player.UserType());

            // CPE
            RegisterPacket(new CPE.ExtInfo());
            RegisterPacket(new CPE.ExtEntry());
            RegisterPacket(new CPE.EnvColor());
            RegisterPacket(new CPE.SpawnPoint());
            RegisterPacket(new CPE.TextColour());
            RegisterPacket(new CPE.BulkBlockUpdate());
            RegisterPacket(new CPE.HackControl());
            RegisterPacket(new CPE.WeatherType());
            RegisterPacket(new CPE.ChangeModel());
            RegisterPacket(new CPE.BlockPermission());
            RegisterPacket(new CPE.ExtAddPlayerName());
            RegisterPacket(new CPE.LightingMode());
            RegisterPacket(new CPE.TwoWayPing());
            RegisterPacket(new CPE.TexturePack());
            RegisterPacket(new CPE.CustomBlocks());
            RegisterPacket(new CPE.EnvProperty());
            RegisterPacket(new CPE.VelocityControlPacket());
            RegisterPacket(new CPE.SetTextHotkey());
        }
    }
}
