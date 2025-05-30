namespace ClassicConnect.Network.Player
{
    public class PositionOrientationUpdate : ClassicPacket
    {
        public override byte PacketID => 0x09;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 6);

            sbyte playerId = (sbyte)data[0];
            sbyte dx = (sbyte)data[1];
            sbyte dy = (sbyte)data[2];
            sbyte dz = (sbyte)data[3];
            byte yaw = (byte)data[4];
            byte pitch = (byte)data[5];

            connection.PlayerList.UpdatePos(playerId, dx, dy, dz, yaw, pitch);
        }
    }
}
