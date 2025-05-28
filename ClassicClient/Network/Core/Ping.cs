namespace ClassicConnect.Network.Core
{
    public class Ping : ClassicPacket
    {
        public override byte PacketID => 0x01;
    }
}
