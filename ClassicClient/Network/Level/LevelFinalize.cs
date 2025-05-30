
namespace ClassicConnect.Network.Level
{
    public class LevelFinalize : ClassicPacket
    {
        public override byte PacketID => 0x04;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 6);

            short width = Util.ReadShort(data, 0);
            short height = Util.ReadShort(data, 2);
            short length = Util.ReadShort(data, 4);

            connection.Level.FinishLoading(width, height, length);
            connection.Events.LevelEvents.OnFinishLoad(new());
        }
    }
}
