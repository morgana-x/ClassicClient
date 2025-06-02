using ClassicConnect.Player;
namespace ClassicConnect.Command.Commands.Admin
{
    internal class CancelTasks : Command
    {
        public override string Name => "cancel";
        public override int RankRequired => 100;
        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            client.CancelTasks();
            client.SendMessage("Canceld tasks");
            return true;
        }
    }
}
