
namespace ClassicConnect.Network.Level
{
    public class LevelFinalize : ClassicPacket
    {
        public override byte PacketID => 0x04;

        public override void Read(ClassicClient connection, Stream stream)
        {
            short width  = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
            short height = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
            short length = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));

            connection.Level.FinishLoading(width, height, length);
            connection.Events.LevelEvents.OnFinishLoad(new());
        }
    }
}
