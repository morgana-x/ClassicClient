
namespace ClassicConnect.Network.Player
{
    public class Spawn : ClassicPacket
    {
        public override byte PacketID => 0x07;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 73);

            sbyte playerId = (sbyte)data[0];
            string playername = Util.ReadString(data,1);

            short x = Util.ReadShort(data, 65);
            short y = Util.ReadShort(data, 67);
            short z = Util.ReadShort(data, 69);

            byte yaw = (byte)data[71];
            byte pitch = (byte)data[72];

            connection.PlayerList.PlayerSpawn(playerId, playername, x, y, z, yaw, pitch);
        }
    }
}
