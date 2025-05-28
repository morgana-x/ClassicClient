using ICSharpCode.SharpZipLib.GZip;

namespace ClassicConnect
{
    public class ClassicLevel
    {
        public short Width;
        public short Height;
        public short Length;
        public volatile byte[] Data = new byte[0];

        public string TexturePack = "";

        private MemoryStream CompressedStream;

        public bool Loading = true;

        public Dictionary<byte, short[]> Colours = new Dictionary<byte, short[]>();

        public byte Weather;

        public byte LightingMode;

        public ClassicLevel()
        {
        }
        public void StartLoading()
        {
            Loading = true;
            CompressedStream = new();
        }
        public void LoadChunk(byte[] chunkdata)
        {
            CompressedStream.Write(chunkdata);
        }
        public void FinishLoading(short width, short height, short length)
        {
            Width = width;
            Height = height;
            Length = length;

            CompressedStream.Position = 0;
            MemoryStream uncompressed = new MemoryStream();
            GZip.Decompress(CompressedStream, uncompressed, false);
            Data = new byte[uncompressed.Length];
            uncompressed.Position = 0;
            uncompressed.Read(Data);

            Loading = false;
        }

        public short GetBlock(short x, short y, short z)
        {
            if (Loading) return 0;
            if (x < 0 || y < 0 || z < 0) return 0;
            if (x >= Width || y >= Height || z >= Length) return 0;
            return (short)(Data[x * Width + z * Width * Height + y]);
        }

        public void SetBlock(int indice, byte block)
        {
            Data[indice] = block;
        }
        public void SetBlock(short x, short y, short z, byte block)
        {
            if (Loading) return;
            if (x < 0 || y < 0 || z < 0) return;
            if (x >= Width || y >= Height || z >= Length) return;
            Data[x * Width + z * Width * Height + y] = block;
        }

        public void SetEnvColor(byte id, short r, short g, short b)
        {
            short[] packed = new short[3] { r, g, b };
            if (!Colours.ContainsKey(id))
                Colours.Add(id, packed);
            Colours[id] = packed;
        }
    }
}
