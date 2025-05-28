
namespace ClassicConnect.Network.Player
{
    public class Spawn : ClassicPacket
    {
        public override byte PacketID => 0x07;

        public override void Read(ClassicClient connection, Stream stream)
        {
            sbyte playerId = (sbyte)stream.ReadByte();
            string playername = Util.ReadString(stream);
            short x = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
            short y = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
            short z = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
            byte yaw = (byte)stream.ReadByte();
            byte pitch = (byte)stream.ReadByte();

            connection.PlayerList.PlayerSpawn(playerId, playername, x, y, z, yaw, pitch);
        }
    }
}
