using ClassicConnect.Player;

namespace ClassicConnect.Command
{
    public class Command
    {
        public virtual string Name => "";
        public virtual int RankRequired => 0;

        public virtual bool CheckPermission(ClassicClient client, ClassicPlayer player)
        {
            return player.Rank >= RankRequired;
        }
        public virtual bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            return true;
        }

        public bool Execute(ClassicClient client, ClassicPlayer executor, string[] args)
        {
            if (!CheckPermission(client, executor))
                return false;

            return OnExecute(client, executor, args);
        }

    }
}
