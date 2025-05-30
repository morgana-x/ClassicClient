namespace ClassicConnect.Network.Player
{
    internal class Teleport : ClassicPacket
    {
        public override byte PacketID => 0x08;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 9);

            sbyte playerID = (sbyte)data[0];
            short x = Util.ReadShort(data, 1);
            short y = Util.ReadShort(data, 3);
            short z = Util.ReadShort(data, 5);
            byte yaw = data[6];
            byte pitch = data[7];

            connection.PlayerList.SetPosRot(playerID, x, y, z, yaw, pitch);
        }

        public static byte[] GetBytes(short x, short y, short z, byte yaw, byte pitch)
        {
            byte[] bytes = new byte[10];
            bytes[0] = 0x08;
            bytes[1] = 0xFF;
            Util.InsertBytes(ref bytes, 2, Util.Reverse(BitConverter.GetBytes(x)));
            Util.InsertBytes(ref bytes, 4, Util.Reverse(BitConverter.GetBytes(y)));
            Util.InsertBytes(ref bytes, 6, Util.Reverse(BitConverter.GetBytes(z)));
            bytes[8] = yaw;
            bytes[9] = pitch;

            return bytes;
        }
    }
}
