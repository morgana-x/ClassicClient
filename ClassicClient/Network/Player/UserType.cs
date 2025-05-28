


namespace ClassicConnect.Network.Player
{
    public class UserType : ClassicPacket
    {
        public override byte PacketID => 0x0f;
        public override void Read(ClassicClient connection, Stream stream)
        {
            int type = stream.ReadByte();
        }
    }
}
