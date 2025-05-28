namespace ClassicConnect.Network.Player
{
    public class PositionOrientationUpdate : ClassicPacket
    {
        public override byte PacketID => 0x09;

        public override void Read(ClassicClient connection, Stream stream)
        {
            sbyte playerId = (sbyte)stream.ReadByte();
            sbyte dx = (sbyte)stream.ReadByte();
            sbyte dy = (sbyte)stream.ReadByte();
            sbyte dz = (sbyte)stream.ReadByte();
            byte yaw = (byte)stream.ReadByte();
            byte pitch = (byte)stream.ReadByte();

            connection.PlayerList.UpdatePos(playerId, dx, dy, dz, yaw, pitch);
        }
    }
}
