namespace ClassicConnect.Network.Level
{
    public class LevelChunk : ClassicPacket
    {
        public override byte PacketID => 0x03;

        public override void Read(ClassicClient connection, Stream stream)
        {
            short chunklength = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
            byte[] chunkdata = new byte[chunklength];

            byte[] chunkbuffer = new byte[1024];
            stream.Read(chunkbuffer);

            Array.Copy(chunkbuffer, chunkdata, chunklength);

            byte percentComplete = (byte)stream.ReadByte();

            
        }
    }
}
