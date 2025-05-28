
namespace ClassicConnect.Network.Level
{
    public class LevelInitialize : ClassicPacket
    {
        public override byte PacketID => 0x02;

        public override void Read(ClassicClient connection, Stream stream)
        {
            connection.Level.StartLoading();
            connection.Events.LevelEvents.OnStartLoad(new());
        }
    }
}
