namespace ClassicConnect.Network.Player
{
    public class OrientationUpdate : ClassicPacket
    {
        public override byte PacketID => 0x0b;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 3);
            sbyte playerId = (sbyte)data[0];
            byte yaw = (byte)data[1];
            byte pitch = (byte)data[2];

            connection.PlayerList.UpdateRot(playerId, yaw, pitch);
        }

    }
}
