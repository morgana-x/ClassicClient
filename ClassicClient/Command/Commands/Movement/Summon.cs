using System;
using ClassicConnect.Player;

namespace ClassicConnect.Command.Commands.Movement
{
    public class Summon : Command
    {
        public override string Name => "summon";
        public override int RankRequired => 50;

        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            if (arguments.Length < 3)
            {
                client.LocalPlayer.SetPosition(executor.X, executor.Y, executor.Z);
                client.SendMessage($"%fTeleported to %a{executor.Name}!");
                return true;
            }
            
            return true;
        }
    }
}
