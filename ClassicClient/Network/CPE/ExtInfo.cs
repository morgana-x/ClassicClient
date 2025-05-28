
namespace ClassicConnect.Network.CPE
{
    public class ExtInfo : ClassicPacket
    {
        public override byte PacketID { get { return 0x10; } }
        public override void Read(ClassicClient connection, Stream stream)
        {
            string appname = Util.DecodeString(stream);
            short extensionCount = BitConverter.ToInt16(Util.ReadBytes(stream, 2));
        }

        public static byte[] GetBytes(short numberOfExtensions)
        {
            byte[] packet = new byte[66];
            Util.InsertBytes(ref packet, 0, Util.EncodeString("hello"));
            Util.InsertBytes(ref packet, 64, BitConverter.GetBytes(numberOfExtensions));
            return packet;
        }
    }
}
