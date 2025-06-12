using ClassicConnect.Player;

namespace ClassicConnect.Command.Commands.Building
{
    public class BuildDelay : Command
    {
        public override string Name => "builddelay";

        public override int RankRequired => 50;

        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            if (arguments.Length < 1) return false;
            int delay = 30;
            if (!int.TryParse(arguments[0], out delay)) return false;
            if (delay < 0) delay = 0;
            client.BuildDelay = delay;
            return true;
        }
    }
}
