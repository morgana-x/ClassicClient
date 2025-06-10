using ClassicConnect.Player;

namespace ClassicConnect.Command.Commands.Fun
{
    public class HollowPurple : Command
    {
        public override string Name => "hollowpurple";

        public override int RankRequired => 100;

  
        private async void PlaceOrb(ClassicClient client, short x, short y, short z)
        {
             Util.PlaceSphere(client, x, y, z, 30, 3);
        }
        private async void BreakOrb(ClassicClient client, short x, short y, short z, int size=10)
        {

            Util.PlaceSphere(client, x, y, z, 0, size);
        }

        private async Task DoHollowPurple(ClassicClient client, short x, short y, short z, float[] dir, int size, int range)
        {
            client.Building = true;
            try
            {
                short[] orbpos = new short[] { x, y, z };
                short[] oldorbpos = new short[] { x, y, z };
                //var dir = dir;//Util.GetLookVector(yaw, pitch); // new float[] { 2f, 0f, 0f };// Util.DirVec(pitch, yaw);
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
                DoHollowPurple(client, executor.BlockX, executor.BlockY, executor.BlockZ, executor.LookVector, size, range); 
            },
                 client.cancelToken.Token);
            return true;
        }
    }
}
