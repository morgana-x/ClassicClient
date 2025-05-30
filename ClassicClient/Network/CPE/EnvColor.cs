namespace ClassicConnect.Network.CPE
{
    internal class EnvColor : ClassicPacket
    {
        public override byte PacketID => 0x19;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 7);

            byte v = data[0];
            short r = Util.ReadShort(data,1);
            short g = Util.ReadShort(data,3);
            short b = Util.ReadShort(data,5);
            connection.Level.SetEnvColor(v, r, g, b);
        }
    }
}
