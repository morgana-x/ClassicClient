using ClassicConnect.Player;

namespace ClassicConnect.Command.Commands.Grief
{
    public class NoBuild : Command
    {
        public override string Name => "nobuild";

        public override int RankRequired => 100;

        public bool Active = false;
        ClassicClient client;
        DateTime nextBuild = DateTime.Now;
        public override bool OnExecute(ClassicClient client, ClassicPlayer executor, string[] arguments)
        {
            this.client = client;
            if (!Active)
            {
                client.Events.LevelEvents.SetBlockEvent += OnBuild;
                Console.WriteLine("Hooked setblock");
            }
            else
            {
                client.Events.LevelEvents.SetBlockEvent -= OnBuild;
                Console.WriteLine("Unhooked setblock");
            }
            Active = !Active;
            return true;
        }

        private void OnBuild(object? sender, Event.LevelEvents.SetBlockEventArgs ev)
        {
            if (DateTime.Now < nextBuild)
                Thread.Sleep((int)nextBuild.Subtract(DateTime.Now).TotalMilliseconds);

            nextBuild = DateTime.Now.AddMilliseconds(client.BuildDelay);
            client.LocalPlayer.SetBlockPosition(ev.X, ev.Y, ev.Z);
            client.SendBytes(Network.Player.Teleport.GetBytes(client.LocalPlayer));
            client.ModifyBlock(ev.X, ev.Y, ev.Z, (byte)ev.PreviousBlock);
        }
    }
}
