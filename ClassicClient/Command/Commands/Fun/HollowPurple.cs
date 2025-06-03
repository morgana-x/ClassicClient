using ClassicConnect.Player;

namespace ClassicConnect.Command.Commands.Fun
{
    public class HollowPurple : Command
    {
        public override string Name => "hollowpurple";

        public override int RankRequired => 100;

        private async void PlaceSphere(ClassicClient client, short x, short y, short z, byte b, int r = 3)
        {
            client.LocalPlayer.X = (short)(x << 5);
            client.LocalPlayer.Y = (short)(y << 5);
            client.LocalPlayer.Z = (short)(z << 5);

            bool modified = false;
            for (int bx = -r/2; bx <= r/2; bx++)
                for (int by = -r/2; by <= r/2; by++)
                    for (int bz = -r/2; bz <= r/2; bz++)
                    {
                        if (Math.Abs(bx) + Math.Abs(by) + Math.Abs(bz) >= r-1) continue;
                        if (client.Level.GetBlock((short)(x+bx), (short)(y+by), (short)(z+bz)) ==b) continue;
                        modified = true;
                        client.ModifyBlock((short)(x + bx), (short)(y + by), (short)(z + bz), b);
                        Thread.Sleep(20);
                    }
            if (modified) Thread.Sleep(25);
        }
        private async void PlaceOrb(ClassicClient client, short x, short y, short z)
        {
             PlaceSphere(client, x, y, z, 30, 3);
        }
        private async void BreakOrb(ClassicClient client, short x, short y, short z, int size=10)
        {

            PlaceSphere(client, x, y, z, 0, size);
        }

        private async Task DoHollowPurple(ClassicClient client, short x, short y, short z, byte yaw, byte pitch, int size, int range)
        {
            client.Building = true;
            try
            {
                short[] orbpos = new short[] { x, y, z };
                short[] oldorbpos = new short[] { x, y, z };
                var dir = Util.GetLookVector(yaw, pitch); // new float[] { 2f, 0f, 0f };// Util.DirVec(pitch, yaw);
                int d = 0;
                while (!client.Level.Loading && client.Level.ValidPos(orbpos) && d < range && client.Building)
                {
                    d++;

                    BreakOrb(client, oldorbpos[0], oldorbpos[1], oldorbpos[2], size);
                    PlaceOrb(client, orbpos[0], orbpos[1], orbpos[2]);
                    for (int i = 0; i < 3; i++)
                    {
                        oldorbpos[i] = orbpos[i];
                        orbpos[i] += (short)(dir[i] * 2);
                    }
                }
                BreakOrb(client, orbpos[0], orbpos[1], orbpos[2], size);
                BreakOrb(client, oldorbpos[0], oldorbpos[1], oldorbpos[2], size);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            client.Building = false;
        }
        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            if (client.Building) return false;

            byte size = 10;
            int range = 20;
            if (arguments.Length > 0 && !byte.TryParse(arguments[0], out size))
                return false;
            if (arguments.Length > 1 && !int.TryParse(arguments[1], out range))
                return false;

            Task.Run(() => { 
                DoHollowPurple(client, executor.BlockX, executor.BlockY, executor.BlockZ, executor.Yaw, executor.Pitch, size, range); 
            },
                 client.cancelToken.Token);
            return true;
        }
    }
}
