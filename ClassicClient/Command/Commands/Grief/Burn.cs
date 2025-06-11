using ClassicConnect.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Command.Commands.Grief
{
    internal class Burn : Command
    {
        public override string Name => "burn";
        public override int RankRequired => 100;

        private static List<int> Burnables = new List<int>()
        {
            17,
            5,
            18,
            47
        };

        private static async Task Explosion(ClassicClient client, short x, short y, short z, int r, bool fire = true)
        {
            client.Building = true;
            bool modified = false;
            for (int bx = -r / 2; bx <= r / 2; bx++)
                for (int by = -r / 2; by <= r / 2; by++)
                    for (int bz = -r / 2; bz <= r / 2; bz++)
                    {
                        if (!client.Building) break;
                        if (Math.Abs(bx) + Math.Abs(by) + Math.Abs(bz) >= r - 1) continue;
                        short gx = (short)(x + bx);
                        short gy = (short)(y + by);
                        short gz = (short)(z + bz);
                        if (!client.Level.ValidPos(gx, gy, gz)) continue;
                        var block = client.Level.GetBlock(gx, gy, gz);
                        if (block == 0) continue;
                       // if (!Burnables.Contains(block)) continue;
                        client.LocalPlayer.X = (short)(gx << 5);
                        client.LocalPlayer.Y = (short)(gy << 5);
                        client.LocalPlayer.Z = (short)(gz << 5);
                        modified = true;
                        client.ModifyBlock(gx, gy, gz, Util.Random.Next(0, 10) == 1 ? (byte)54 : (byte)0);
                        Thread.Sleep(50);
                        //Thread.Sleep(1);
                    }
            if (modified) Thread.Sleep(25);
            client.Building = false;
        }

        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            if (client.Building) return false;

            byte size = 10;
            int range = 25;
            if (arguments.Length > 1 && !byte.TryParse(arguments[1], out size))
                return false;
            if (arguments.Length > 0 && !int.TryParse(arguments[0], out range))
                return false;

            Task.Run(() => {
                Explosion(client, executor.BlockX, executor.BlockY, executor.BlockZ, range, true);
            },
           client.cancelToken.Token);

            return true;
        }
    }
}
