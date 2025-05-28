namespace ClassicConnect.Network.CPE
{
    internal class TexturePack : ClassicPacket
    {
        public override byte PacketID => 0x28;

        public override void Read(ClassicClient connection, Stream stream)
        {
            string texturepack = Util.ReadString(stream);
            connection.Level.TexturePack = texturepack;
        }
    }
}
