
namespace ClassicConnect.Network.CPE
{
    internal class TextColour : ClassicPacket
    {
        public override byte PacketID => 0x27;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte r = (byte)stream.ReadByte();
            byte g = (byte)stream.ReadByte();
            byte b = (byte)stream.ReadByte();
            byte a = (byte)stream.ReadByte();
            byte charcode = (byte)stream.ReadByte();
        }
    }
}
