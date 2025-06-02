using ClassicConnect.Player;
using System.Drawing;

namespace ClassicConnect.Command.Commands.Building
{
    
    internal class ImagePlacer : Command
    {
        public override string Name => "image";
        public override int RankRequired => 50;


        private async void PlaceImage(ClassicClient client, string filepath, short x, short y, short z )
        {
            if (y >= client.Level.Height) return;
            if (y < 0) return;
            client.LocalPlayer.SetBlockPosition(x, y, z);

            string fp = Path.Join(Path.Join(Directory.GetCurrentDirectory(), "image"), filepath);
            Console.WriteLine(fp);
            if (!File.Exists(fp)) return;
            using var img = new ImageMagick.MagickImage(fp);

            var pixels= img.GetPixels();

            client.ModifyBlock(x, y, z, 1);


            ushort halfTrans = 500;//ushort.MaxValue / 2;
            for (int px =0; px < img.Width; px++)
            {
                for (int py = 0; py < img.Height; py++)
                {
                    var color = pixels.GetPixel(px, py).ToColor();
                    if (color == null) continue;
                    if (color.A < halfTrans) continue;
                    var bytearray = new byte[3] { (byte)(((float)color.R / (float)ushort.MaxValue) * 255), (byte)(((float)color.G / (float)ushort.MaxValue) * 255), (byte)(((float)color.B / (float)ushort.MaxValue) * 255) };
                    byte block = ImageConverter.GetNearestBlock(bytearray);

                    short bx = (short)(x + px);
                    short bz =  (short)(z + py);

                    if (bx > client.Level.Width) break;
                    if (bz > client.Level.Length) break;

                    if (client.LocalPlayer.BlockDistance(bx, y, bz) > 2)
                        client.LocalPlayer.SetBlockPosition(bx, (short)(y+1), bz);

                    client.PlaceBlock(bx, y, bz, block);
                    Thread.Sleep(2);
                }
            }
        }
        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            if (arguments.Length < 1)
            {
     
                return false;
            }

            Task.Run(() => {
                try
                {
                    PlaceImage(client, arguments[0], executor.BlockX, executor.BlockY, executor.BlockZ);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }, client.cancelToken.Token);
            return true;
        }
        class ImageConverter
        {
            static Dictionary<uint, byte> ImageBlock = new Dictionary<uint, byte>()
            {
                [0x707070] = 1,
                [0x7d8b91] = 1,
                [0x828894] = 1,
                [0x875704] = 3,
                [0xbc9862] = 5,
                [0xdad0a9] = 5,
                [0xe3c2b7] = 12,
                [0xff4aa0] = 12,
                [0xffffaa] = 12,
                [0xfef4e0] = 12,
                [0xe7c9e1] = 12,
                [0xe6cae1] = 12,
                [0xe9cae1] = 12,
                [0xc097be] = 12,
                [0xebc6e1] = 12,
                [0xe8cae1] = 12,
                [0xfbdaf2] = 12,
                [0xebe3d1] = 12,
                [0xfffc38] = 12,
                //[0xebd4c6] = 12,
                // [0xefd9c9] = 12,
                [0xff0000] = 21,
                [0xff5500] = 22,
                [0xffff00] = 23,
                [0x77ff00] = 24,
                [0x00ff00] = 25,
                [0x00ff88] = 26,
                [0x00bbff] = 27,
                [0x95d1d3] = 27,
                [0x97d0d3] = 27,
                [0x96d2d5] = 27,
                [0x9fd9de] = 27,
                [0x71d3d3] = 27,
                [0xa3dfe6] = 27,
                [0x8aeff3] = 27,
                [0x5ab7c0] = 27,
                [0x8ecfd2] = 27,
                [0xc0ecef] = 27,
                [0x95cfd2] = 27,
                [0x4d979e] = 28,
                [0x458a8d] = 28,
                [0x4d979e] = 28,
                [0x83c9d0] = 28,
                [0x0090ff] = 28,
                [0x63bcff] = 28,
                [0x7ba7a5] = 28,
                [0x96cecf] = 28,
                [0x65b2b6] = 28,
                [0x0090ff] = 29,
                [0x0000ff] = 29,
                [0x1e840c] = 29,
                [0x317f8a] = 29,
                [0x53c2be] = 29,
                [0x9000ff] = 30,
                [0x4f1663] = 30,
                [0xd000ff] = 31,
                [0x945f76] = 31,
                [0xea00ff] = 32,
                [0xc989b5] = 32,
                [0xb56689] = 32,
                [0xd381c2] = 32,
                [0xd64cae] = 33,
                [0xce44a3] = 33,
                [0xff00cc] = 33,
                [0xca48a3] = 33,
                [0xd381c2] = 33,
                [0xd54bad] = 33,
                [0x202020] = 34,
                [0x584f56] = 34,
                [0x305a56] = 34,
                [0x234547] = 34,
                [0x52342d] = 34,
                [0x322e2e] = 34,
                [0xaea4b9] = 35,
                [0x505050] = 35,
                [0xbababa] = 35,
                [0xbab6cf] = 35,
                [0xc4c1dc] = 35,
                [0xd8d1c0] = 35,
                [0xcdd6e0] = 35,
                [0xced8de] = 35,
                [0xffffff] = 36,
                [0xe6afe1] = 36,
                [0xebe6ed] = 36,
                [0xd8c5e1] = 36,
                [0xe0cbe9] = 36,
                //[0xa3a3a3] = 43,
                //[0xc8c1b8] = 43,
               // [0xaaa69a] = 43,
                [0x000000] = 49,
                [0x2c2528] = 49,
                [0x251f2c] = 49,
                [0x0c0002] = 49,
                [0x121b22] = 49,
                [0x242326] = 49,
                [0x521942] = 49,
                [0x2a2728] = 49,
                //[0x2f3633] = 49

              //  [0xf9dccc] = 55

            };

            private static int distance(byte[] col1bytes, byte[] col2bytes)
            {
                Array.Reverse(col1bytes);
                int dist = 0;
                for (int i = 0; i < 3; i++)
                {
                    int d = (col1bytes[i] - col2bytes[i]);
                    dist += (d*d);
                }
                return dist;
            }
            public static byte GetNearestBlock(byte[] colour)
            {
                byte block = 49;
                int closestDist = int.MaxValue;
                foreach (var pair in ImageBlock)
                {
                    var keybytes = BitConverter.GetBytes(pair.Key).SkipLast(1).ToArray();
                    Console.WriteLine(string.Join(",", keybytes));
                    if (Enumerable.SequenceEqual(keybytes, colour))
                        return pair.Value;
                    int dist = distance(keybytes, colour);
                    if (dist >= closestDist)
                        continue;
                    closestDist = dist;
                    block = pair.Value;
                }
                return block;
            }
        }

    }

}
