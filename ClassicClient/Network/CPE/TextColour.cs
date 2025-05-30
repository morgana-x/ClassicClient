
namespace ClassicConnect.Network.CPE
{
    internal class TextColour : ClassicPacket
    {
        public override byte PacketID => 0x27;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 5);

            byte r = data[0];
            byte g = data[1];
            byte b = data[2];
            byte a = data[3];
            byte charcode = data[4];
        }
    }
}
