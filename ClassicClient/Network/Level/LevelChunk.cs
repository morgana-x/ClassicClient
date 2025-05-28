namespace ClassicConnect.Network.Level
{
    public class LevelChunk : ClassicPacket
    {
        public override byte PacketID => 0x03;

        public override void Read(ClassicClient connection, Stream stream)
        {
            short chunklength = BitConverter.ToInt16(Util.ReadBytes(stream, 2, true));
          

            byte[] chunkbuffer = new byte[1024];
            stream.Read(chunkbuffer);

            byte percentComplete = (byte)stream.ReadByte();

            byte[] chunkdata = new byte[chunklength];
            Array.Copy(chunkbuffer, chunkdata, chunklength);

            connection.Level.LoadChunk(chunkdata);

            connection.Events.LevelEvents.OnLoadChunk(new(percentComplete));
            
        }
    }
}
