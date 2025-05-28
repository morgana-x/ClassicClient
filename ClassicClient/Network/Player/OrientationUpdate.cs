namespace ClassicConnect.Network.Player
{
    public class OrientationUpdate : ClassicPacket
    {
        public override byte PacketID => 0x0b;

        public override void Read(ClassicClient connection, Stream stream)
        {
            sbyte playerId = (sbyte)stream.ReadByte();
            byte yaw = (byte)stream.ReadByte();
            byte pitch = (byte)stream.ReadByte();

            connection.PlayerList.UpdateRot(playerId, yaw, pitch);
        }

    }
}
