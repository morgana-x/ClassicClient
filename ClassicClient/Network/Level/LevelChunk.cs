namespace ClassicConnect.Network.Level
{
    public class LevelChunk : ClassicPacket
    {
        public override byte PacketID => 0x03;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 1027);

            short chunklength = Util.ReadShort(data);
          
            byte[] chunkdata = new byte[chunklength];
            Array.Copy(data, 2, chunkdata, 0, chunklength);

            byte percentComplete = data[1026];

            connection.Level.LoadChunk(chunkdata);

            connection.Events.LevelEvents.OnLoadChunk(new(percentComplete));
            
        }
    }
}
