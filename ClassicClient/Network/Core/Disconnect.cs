namespace ClassicConnect.Network.Core
{
    internal class Disconnect : ClassicPacket
    {
        public override byte PacketID { get { return 0x0e; } }
        public override void Read(ClassicClient connection, Stream stream)
        {
            string reason = Util.DecodeString(stream);
        }
    }
}
