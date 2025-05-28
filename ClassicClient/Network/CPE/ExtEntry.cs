
namespace ClassicConnect.Network.CPE
{
    public class ExtEntry : ClassicPacket
    {
        public override byte PacketID => 0x11;

        public override void Read(ClassicClient connection, Stream stream)
        {
            string name = Util.DecodeString(stream);
            int version = BitConverter.ToInt32(Util.ReadBytes(stream, 4));
        }
    }
}
