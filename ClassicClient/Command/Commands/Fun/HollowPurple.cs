using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        Thread.Sleep(1);
                    }
            if (modified) Thread.Sleep(10);
        }
        private async void PlaceOrb(ClassicClient client, short x, short y, short z)
        {
             PlaceSphere(client, x, y, z, 30, 3);
        }
        private async void BreakOrb(ClassicClient client, short x, short y, short z)
        {

            PlaceSphere(client, x, y, z, 0, 10);
        }
        private async Task rant(ClassicClient client)
        {
            client.SendMessage("Sorry, Amanai.");
            Thread.Sleep(1000);
            client.SendMessage("I'm not even angry over you right now.");
            Thread.Sleep(1000);
            client.SendMessage("I bear no grudge against anyone.");
            Thread.Sleep(1000);
            client.SendMessage("It's just that the world feels so, so wonderful right now.");
            Thread.Sleep(1000);
            client.SendMessage("Throughout Heaven and Earth, I alone am the honored one.");

        }
        private async Task DoHollowPurple(ClassicClient client, short x, short y, short z, byte yaw, byte pitch, bool rant)
        {
            if (rant)
                await this.rant(client);
            short[] orbpos = new short[] { x, y, z };
            short[] oldorbpos = new short[] { x, y, z };
            var dir = Util.GetLookVector(yaw, pitch); // new float[] { 2f, 0f, 0f };// Util.DirVec(pitch, yaw);
            int d = 0;
            while (!client.Level.Loading && client.Level.ValidPos(orbpos) && d < 20)
            {
                d++;
              
                BreakOrb(client, oldorbpos[0], oldorbpos[1], oldorbpos[2]);
                PlaceOrb(client, orbpos[0], orbpos[1], orbpos[2]);
                for (int i = 0; i < 3; i++)
                {
                    oldorbpos[i] = orbpos[i];
                    orbpos[i] += (short)(dir[i] * 2);
                }
            }
            BreakOrb(client, orbpos[0], orbpos[1], orbpos[2]);
            BreakOrb(client, oldorbpos[0], oldorbpos[1], oldorbpos[2]);
        }
        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            Task.Run(() => DoHollowPurple(client, executor.BlockX, executor.BlockY, executor.BlockZ, executor.Yaw, executor.Pitch, arguments.Length>0),
                 client.cancelToken.Token);
            return true;
        }
    }
}
