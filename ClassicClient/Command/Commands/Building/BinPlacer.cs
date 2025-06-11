using ClassicConnect.Player;

namespace ClassicConnect.Command.Commands.Building
{


    public class BinPlacer : Command
    {

        public override string Name => "binplace";

        public override int RankRequired => 50;

        private async void BuildBin(ClassicClient client, short ax, short ay, short az, string buildPath)
        {
            string filePath = Path.Join(Path.Join(Directory.GetCurrentDirectory(),"schematic"), buildPath);
            if (!File.Exists(filePath)) return;
            BinBuild build = new BinBuild(filePath);
         
            client.Building = true;
            int index = 0;
            for (int x = 0; x < build.Width; x++)
                for (int y = 0; y < build.Height; y++)
                    for (int z = 0; z < build.Length; z++)
                    {
                        if (!client.Building) break;
                       // if (build.Blocks[index] == 0) { index++; continue; }

                        short bx = (short)(ax + x);
                        short by = (short)(ay + y);
                        short bz = (short)(az + z);

                        if (!client.Level.ValidPos(bx, by, bz)) { index++; continue; }
                        if (client.Level.GetBlock(bx,by,bz) == build.Blocks[index] ) { index++; continue; }

                        client.LocalPlayer.SetBlockPosition((short)(bx+2), (short)(by), (short)(bz));
                        client.SendBytes(Network.Player.Teleport.GetBytes(client.LocalPlayer));
                        client.PlaceBlock(bx, by, bz, (byte)build.Blocks[index]);
                        index++;
                         Thread.Sleep(30);
                        //Thread.Sleep(1);
                    }

            client.Building = false;
        }
        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            if (client.Building)
                return false;

            if (arguments.Length < 1) return false;

            Task.Run(() =>
            {
                try
                {
                    BuildBin(client, executor.BlockX, executor.BlockY, executor.BlockZ, arguments[0]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    client.Building = false;
                }
            }
                , client.cancelToken.Token);
            return true;
        }
        class BinBuild
        {
            public int Width;
            public int Height;
            public int Length;
            public ushort[] Blocks;
            public int[] Anchor = new int[3] { 0, 0, 0 };

            public BinBuild(string filePath)
            {
                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                Width = br.ReadInt32();
                Height = br.ReadInt32();
                Length = br.ReadInt32();
                for (int i = 0; i < 3; i++)
                    Anchor[i] = br.ReadInt32();

                int totalsize = (Width * Height * Length);
                Blocks = new ushort[totalsize];
                for (int i=0; i < totalsize; i++)
                    Blocks[i] = br.ReadUInt16();
            }
            public ushort GetBlock(int x, int y, int z)
            {
                if (x > Width) return 0;
                if (y > Height) return 0;
                if (z > Length) return 0;
                return Blocks[x + y * Width + z * Width * Length];
            }
        }
    }
}
