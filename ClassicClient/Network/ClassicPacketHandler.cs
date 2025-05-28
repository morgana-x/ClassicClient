namespace ClassicConnect.Network
{
    public class ClassicPacketHandler
    {
        public static Dictionary<byte, ClassicPacket> ReadPackets = new Dictionary<byte, ClassicPacket>();
        
        public static bool ReadPacket(ClassicClient client, Stream stream)
        {
            byte id = (byte)stream.ReadByte();

            if (!ReadPackets.ContainsKey(id))
            {
                Console.WriteLine($"Received unknown packet 0x{id.ToString("X")}!");
                return false;
            }
            Console.WriteLine("Reading packet 0x" + id.ToString("X"));
            ReadPackets[id].Read(client, stream);
            return true;
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
            RegisterPacket(new Level.SetBlock());
            RegisterPacket(new Player.Spawn());
            RegisterPacket(new Player.Teleport());
            RegisterPacket(new Player.PositionOrientationUpdate());
            RegisterPacket(new Player.PositionUpdate());
            RegisterPacket(new Player.OrientationUpdate());
            RegisterPacket(new Player.Despawn());
            RegisterPacket(new Player.Message());
            RegisterPacket(new Core.Disconnect());
            RegisterPacket(new Player.UserType());

        }
    }
}
