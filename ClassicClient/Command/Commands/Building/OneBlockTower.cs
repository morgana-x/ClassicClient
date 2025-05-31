using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassicConnect.Player;

namespace ClassicConnect.Command.Commands.Building
{
    internal class OneBlockTower : Command
    {
        public override string Name => "oneblocktower";
        public override int RankRequired => 50;

        private static byte randomblock()
        {
            if (Util.Random.Next(2) == 1)
            {
                return (byte)Util.Random.Next(1, 6);
            }
            return (byte)Util.Random.Next(12, 47);
        }
        private async void OneBlockBuild(ClassicClient client,short x, short y, short z, short height=50)
        {
            for (int i = 0; i < height; i++)
            {
                client.LocalPlayer.SetPosition((short)(x << 5), (short)(y << 5), (short)(z << 5));
                client.PlaceBlock(client.LocalPlayer.BlockX, client.LocalPlayer.BlockY, client.LocalPlayer.BlockZ, randomblock());
                y++;
                Thread.Sleep(50);
            }

        }
        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            short height = (short)(client.Level.Height - executor.BlockY);

            if (arguments.Length > 0)
                short.TryParse(arguments[0], out height);

            if (height < 0)
                height = 20;

            Task.Run(() => OneBlockBuild(client, executor.BlockX, executor.BlockY, executor.BlockZ, height));
            return true;
        }
    }
}
