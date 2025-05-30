
namespace ClassicConnect.Network.Level
{
    public class ServerSetBlock : ClassicPacket
    {
        public override byte PacketID { get { return 0x06; } }

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 7);

            short[] position = new short[3];

            for (int i = 0; i < 3; i++)
                position[i] = Util.ReadShort(data, i * 2);

            short block = data[6];

            connection.Level.SetBlock(position[0], position[1], position[2], (byte)block);
            connection.Events.LevelEvents.OnSetBlock(new(position[0], position[1], position[2], block));
        }

      
    }
}
