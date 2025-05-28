namespace ClassicConnect.Network
{
    public class ClassicPacket
    {
        public virtual byte PacketID { get { return 0x00; } }
        public virtual void Read(ClassicClient connection, Stream stream)
        {

        }
    }
}
