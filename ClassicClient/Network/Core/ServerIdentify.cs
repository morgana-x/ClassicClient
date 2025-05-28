namespace ClassicConnect.Network.Core
{
    public class ServerIdentify : ClassicPacket
    {
        public override byte PacketID => 0x0;
        public override void Read(ClassicClient connection, Stream stream)
        {
            connection.ConnectedServer.ServerProtocol = stream.ReadByte();

            connection.ConnectedServer.ServerName = Util.DecodeString(stream);
            connection.ConnectedServer.ServerMotd = Util.DecodeString(stream);

            connection.ConnectedServer.UserType = stream.ReadByte();
        }
    }
}
