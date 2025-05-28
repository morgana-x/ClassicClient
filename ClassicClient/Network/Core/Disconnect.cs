namespace ClassicConnect.Network.Core
{
    internal class Disconnect : ClassicPacket
    {
        public override byte PacketID { get { return 0x0e; } }
        public override void Read(ClassicClient connection, Stream stream)
        {
            string reason = Util.ReadString(stream);
            connection.Events.CoreEvents.OnDisconnect(new(reason));
            connection.Disconnect();
        }
    }
}
