namespace ClassicConnect.Network.Player
{
    public class PositionUpdate : ClassicPacket
    {
        public override byte PacketID => 0x0a;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 4);
            sbyte playerid = (sbyte)data[0];
            sbyte dx = (sbyte)data[1];
            sbyte dy = (sbyte)data[2];
            sbyte dz = (sbyte)data[3];

            connection.PlayerList.UpdatePos(playerid, dx, dy, dz);
        }
    }
}
