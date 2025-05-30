namespace ClassicConnect.Network.Core
{
    public class ServerIdentify : ClassicPacket
    {
        public override byte PacketID => 0x0;
        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 130);// new byte[130];

            connection.ConnectedServer.ServerProtocol = data[0];//stream.ReadByte();

            connection.ConnectedServer.ServerName = Util.ReadString(data,1);
            connection.ConnectedServer.ServerMotd = Util.ReadString(data, 65);

            connection.ConnectedServer.UserType = data[129];
        }
    }
}
