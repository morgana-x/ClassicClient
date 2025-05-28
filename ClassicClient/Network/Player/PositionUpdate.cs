namespace ClassicConnect.Network.Player
{
    public class PositionUpdate : ClassicPacket
    {
        public override byte PacketID => 0x0a;

        public override void Read(ClassicClient connection, Stream stream)
        {
            sbyte playerid = (sbyte)stream.ReadByte();
            sbyte dx = (sbyte)stream.ReadByte();
            sbyte dy = (sbyte)stream.ReadByte();
            sbyte dz = (sbyte)stream.ReadByte();

            connection.PlayerList.UpdatePos(playerid, dx, dy, dz);
        }
    }
}
