namespace ClassicConnect.Network.CPE
{
    internal class EnvColor : ClassicPacket
    {
        public override byte PacketID => 0x19;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte v = (byte)stream.ReadByte();
            short r = Util.ReadShort(stream);
            short g = Util.ReadShort(stream);
            short b = Util.ReadShort(stream);
            connection.Level.SetEnvColor(v, r, g, b);
        }
    }
}
