
namespace ClassicConnect.Network.Level
{
    public class SetBlock : ClassicPacket
    {
        public override byte PacketID { get { return 0x05; } }
        public static byte[] GetBytes(short x, short y, short z, byte mode, byte block)
        {
            byte[] data = new byte[9];
            data[0] = 0x05;
            Util.InsertBytes(ref data, 1, Util.Reverse(BitConverter.GetBytes(x)));
            Util.InsertBytes(ref data, 3, Util.Reverse(BitConverter.GetBytes(y)));
            Util.InsertBytes(ref data, 5, Util.Reverse(BitConverter.GetBytes(z)));
            data[7] = mode;
            data[8] = block;
            return data;
        }

        public static byte[] GetBytes(short x, short y, short z, byte block)
        {
            return GetBytes(x, y, z, block == 0 ? (byte)0 : (byte)1, block);
        }
        public static byte[] GetBytes(short x, short y, short z, byte block, bool breaking)
        {
            return GetBytes(x, y, z, breaking ? (byte)0 : (byte)1, block);
        }
    }
}
