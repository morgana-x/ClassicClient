namespace ClassicConnect.Network.Player
{
    internal class Teleport : ClassicPacket
    {
        public override byte PacketID => 0x08;

        public override void Read(ClassicClient connection, Stream stream)
        {
            sbyte playerID = (sbyte)stream.ReadByte();
            short x = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
            short y = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
            short z = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
            byte yaw = (byte)stream.ReadByte();
            byte pitch = (byte)stream.ReadByte();

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
