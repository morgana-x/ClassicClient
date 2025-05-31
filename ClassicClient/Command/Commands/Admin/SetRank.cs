using ClassicConnect.Player;

namespace ClassicConnect.Command.Commands.Admin
{
    public class SetRank : Command
    {
        public override string Name => "setrank";

        public override int RankRequired => 100;

        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            if (arguments.Length < 2) return false;
            Console.WriteLine(arguments[0]);
            ClassicPlayer? target = client.PlayerList.SearchPlayer(arguments[0]);
            if (target == null) return false;
            int rank = 0;
            if (!int.TryParse(arguments[1], out rank)) return false;

            target.Rank = rank;
            client.SendMessage($"%fSet %a{target.Name}%f's rank to %a{rank}!");
            return true;
        }
    }
}
