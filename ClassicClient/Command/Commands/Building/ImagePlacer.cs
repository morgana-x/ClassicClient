using ClassicConnect.Player;
using System.Drawing;

namespace ClassicConnect.Command.Commands.Building
{
    
    internal class ImagePlacer : Command
    {
        public override string Name => "image";
        public override int RankRequired => 50;

        private void GetPixelPos(short x, short y, short z, int px, int py, uint width, uint height, int rightleft, bool vertical, out short bx, out short by, out short bz)
        {
            bx = x;
            by = y;
            bz = z;

            if (vertical)
            {
                by = (short)((y+height) - py);
                if (rightleft == 0)
                {
                    bx = (short)(x + px - (width / 2));
                }
                else
                {
                    bz = (short)(z + px - (width / 2));
                }
                return;
            }
            if (rightleft == 0)
            {
                bx = (short)(x + px - (width / 2));
                bz = (short)(z + py - (width / 2));
                return;
            }
            bx = (short)(x + py - (width / 2));
            bz = (short)(z + px - (width / 2));
            return;
        }
        private async void PlaceImage(ClassicClient client, string filepath, short x, short y, short z, int dirX, int dirY, int dirZ)
        {
            if (y >= client.Level.Height) return;
            if (y < 0) return;

            string fp = Path.Join(Path.Join(Directory.GetCurrentDirectory(), "image"), filepath);
            Console.WriteLine(fp);
            if (!File.Exists(fp)) return;


            client.LocalPlayer.SetBlockPosition(x, y, z);

            // client.SendMessage($"&ePlacing &c{filepath}&e...");
            using var img = new ImageMagick.MagickImage(fp);

            var pixels = img.GetPixels();

            client.ModifyBlock(x, y, z, 1);


            ushort halfTrans = 500;//ushort.MaxValue / 2;
            for (int px = 0; px < img.Width; px++)
            {
                if (!client.Building) break;
                for (int py = 0; py < img.Height; py++)
                {
                    if (!client.Building) break;
                    var color = pixels.GetPixel(px, py).ToColor();
                    if (color == null) continue;
                    if (color.A < halfTrans) continue;

                    GetPixelPos(x, y, z, px, py, img.Width, img.Height, dirX, dirY == 0, out short bx, out short by, out short bz);

                    if (bx > client.Level.Width) continue;
                    if (bx < 0) continue;
                    if (bz > client.Level.Length) continue;
                    if (bz < 0) continue;
                    if (by > client.Level.Height) continue;
                    if (by < 0) continue;

                    var bytearray = new byte[4] { (byte)(((float)color.B / (float)ushort.MaxValue) * 255), (byte)(((float)color.G / (float)ushort.MaxValue) * 255), (byte)(((float)color.R / (float)ushort.MaxValue) * 255), (byte)255 };
                    byte block = ImageConverter.GetNearestBlock(bytearray);

                    if (client.Level.GetBlock(bx, y, bz) == block) continue;
                    if (client.LocalPlayer.BlockDistance(bx, by, bz) > 20)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            client.LocalPlayer.SetBlockPosition(Util.Lerp(client.LocalPlayer.X, bx, 0.25f), Util.Lerp(client.LocalPlayer.Y, by, 0.25f), Util.Lerp(client.LocalPlayer.Z, bz, 0.25f));
                        }
                    }
                    client.LocalPlayer.SetBlockPosition(bx, (short)(by + 1), bz);
                    client.SendBytes(Network.Player.Teleport.GetBytes(client.LocalPlayer));
                    client.PlaceBlock(bx, by, bz, block);
                    Thread.Sleep(client.BuildDelay);
                    //Thread.Sleep(1);
                }
            }
        }
        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            if (arguments.Length < 1)
            {
     
                return false;
            }
            if (client.Building) return false;
            Task.Run(() => {
                client.Building = true;
                try
                {
                    Util.FourYaw(executor.Yaw, out int dirX, out int dirZ);
                    Util.Pitch(executor.Pitch, out int dirY);
                    PlaceImage(client, arguments[0], executor.BlockX, (short)(executor.BlockY > 0 ? executor.BlockY-1 : executor.BlockY), executor.BlockZ, dirX, dirY, dirZ );
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                client.Building = false;

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
                [0x828995] = 1,
                [0x917d75] = 1,
                [0xa7afb9] = 1,
                //  [0x3c3c3e] = 4,
                [0x875704] = 3,
                [0x996c55] = 3,
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
                [0xe2cfc1] = 12,
                [0xf7e8d9] = 12,
                [0xf0e8d7] = 12,
                [0xf0e8de] = 12,
                [0xf8c8bf] = 12,
                // [0xc7a28f] = 12,
                //[0xebd4c6] = 12,
                // [0xefd9c9] = 12,
                [0xff0000] = 21,
                [0xc32420] = 21,
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
                [0x6e7cec] = 28,
                [0x0090ff] = 29,
                [0x0000ff] = 29,
                [0x1e840c] = 29,
                [0x317f8a] = 29,
                [0x53c2be] = 29,
                [0x6877e3] = 29,
                [0x2c306e] = 29,
                [0x9000ff] = 30,
                [0x4f1663] = 30,
                [0x5b4b97] = 30, // Remove if causing issues with blue
                [0xd000ff] = 31,
                [0x945f76] = 31,
                [0x8d75ba] = 31,
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
                [0xfd72ac] = 33,
                [0xfb7292] = 33,
                [0x202020] = 34,
                [0x584f56] = 34,
                [0x305a56] = 34,
                [0x234547] = 34,
                [0x52342d] = 34,
                [0x322e2e] = 34,
                [0x413b3a] = 34,
                [0x48494d] = 34,
                [0x9c9fa1] = 35,
                [0x3d414a] = 35,
                [0xaea4b9] = 35,
                [0x505050] = 35,
                [0xbababa] = 35,
                [0xbab6cf] = 35,
                [0xc4c1dc] = 35,
                [0xd8d1c0] = 35,
                [0xcdd6e0] = 35,
                [0xced8de] = 35,
                [0xbfc9d1] = 35,
                [0xbec8d0] = 35,
                // [0x8b9ca2] = 35,
                [0xc9d4da] = 35,
                [0xc9cfd2] = 35,
                [0x9fa9b1] = 35,
                [0x898991] = 35,
                [0xffffff] = 36,
                [0xe6afe1] = 36,
                [0xebe6ed] = 36,
                [0xd8c5e1] = 36,
                [0xe0cbe9] = 36,
                [0xd2d0d1] = 36,
                [0xeae7dd] = 36,

                //[0xf3b835] = 41,
                //[0xfeb855] = 41,
                //[0xa3a3a3] = 43,
                //[0xc8c1b8] = 43,
                // [0xaaa69a] = 43,

                [0x9d481f] = 46,
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


            static Dictionary<uint, byte> CPEImageBlock = new Dictionary<uint, byte>()
            {
                [0x1e02a3] = 58
            };

        private static int distance(byte[] col1bytes, byte[] col2bytes)
            {
                //Array.Reverse(col1bytes);
                int dist = 0;
                for (int i = 0; i < 3; i++)
                {
                    int d = (col1bytes[i] - col2bytes[i]);
                    dist += (d*d);
                }
                return dist;
            }
            static Dictionary<uint, byte> Cache = new Dictionary<uint, byte>();
            
            public static byte GetNearestBlock(byte[] colour)
            {
                byte block = 49;
                int closestDist = int.MaxValue;
                uint colorInt =  BitConverter.ToUInt32(colour);
                if (CPE.CPEEnabled && CPEImageBlock.ContainsKey(colorInt)) return CPEImageBlock[colorInt];
                if (Cache.ContainsKey(colorInt)) return Cache[colorInt];
                if (ImageBlock.ContainsKey(colorInt)) return ImageBlock[colorInt];
               // Console.WriteLine(string.Join(",", colour));
                foreach (var pair in ImageBlock)
                {
                    var keybytes = BitConverter.GetBytes(pair.Key).ToArray();
                 //   Console.WriteLine(string.Join(",", keybytes));
                    if (colorInt == pair.Key)//(Enumerable.SequenceEqual(keybytes, colour))
                        return pair.Value;
                    int dist = distance(keybytes, colour);
                    if (dist >= closestDist)
                        continue;
                    closestDist = dist;
                    block = pair.Value;
                }
                if (CPE.CPEEnabled)
                    foreach(var pair in CPEImageBlock)
                    {
                        var keybytes = BitConverter.GetBytes(pair.Key).ToArray();
                        //   Console.WriteLine(string.Join(",", keybytes));
                        if (colorInt == pair.Key)//(Enumerable.SequenceEqual(keybytes, colour))
                            return pair.Value;
                        int dist = distance(keybytes, colour);
                        if (dist >= closestDist)
                            continue;
                        closestDist = dist;
                        block = pair.Value;
                    }
                Cache.Add(colorInt, block);
                return block;
            }
        }

    }

}
