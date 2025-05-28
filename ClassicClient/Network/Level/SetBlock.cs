
namespace ClassicConnect.Network.Level
{
    public class SetBlock : ClassicPacket
    {
        public override byte PacketID { get { return 0x05; } }

        public override void Read(ClassicClient connection, Stream stream)
        {
            short[] position = new short[3];

            for (int i = 0; i < 3; i++)
                position[i] = BitConverter.ToInt16( Util.ReadBytes(stream, 2, true));

            short block = (short)stream.ReadByte();
        }

        public static byte[] GetBytes(short x, short y, short z, byte mode, byte block)
        {
            byte[] data = new byte[8];
            Util.InsertBytes(ref data, 0, Util.Reverse(BitConverter.GetBytes(x)));
            Util.InsertBytes(ref data, 2, Util.Reverse(BitConverter.GetBytes(y)));
            Util.InsertBytes(ref data, 4, Util.Reverse(BitConverter.GetBytes(z)));
            data[6] = mode;
            data[7] = block;

            return data;
        }

        public static byte[] GetBytes(short x, short y, short z, byte block)
        {
            return GetBytes(x, y, z, block == 0 ? (byte)0 : (byte)1, block);
        }
    }
}
