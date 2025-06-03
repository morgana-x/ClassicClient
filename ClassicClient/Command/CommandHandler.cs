using ClassicConnect.Player;

namespace ClassicConnect.Command
{
    public class CommandHandler
    {
        public Dictionary<string, Command> Commands = new Dictionary<string, Command>();

        public ClassicClient Client;

        public CommandHandler(ClassicClient client)
        {
            this.Client = client;

            RegisterCommand(new Commands.Movement.Teleport());
            RegisterCommand(new Commands.Admin.SetRank());
            RegisterCommand(new Commands.Admin.CancelTasks());
            RegisterCommand(new Commands.Fun.HollowPurple());
            RegisterCommand(new Commands.Building.OneBlockTower());
            RegisterCommand(new Commands.Building.ImagePlacer());

            client.Events.PlayerEvents.ChatEvent += this.OnMessage;

        }

        public string[] Prefix = new string[] { "?", ";", "!" };
        public void RegisterCommand(Command command)
        {
            if (!Commands.ContainsKey(command.Name))
                Commands.Add(command.Name.ToLower(), command);

            Commands[command.Name.ToLower()] = command;
        }

        public bool HandleMessage(string playername, string message, sbyte playerid=-1)
        {
            ClassicPlayer? player = Client.PlayerList.GetPlayer(playername);
            if (player == null && !(CPE.EnabledCPE.ContainsKey("MessageTypes") && CPE.EnabledCPE["MessageTypes"])) { player = Client.PlayerList.GetPlayer(playerid); }
            if (player == null) return false;


            List<string> split = message.Split(" ").ToList();
            if (split.Count < 1) return false;

            string command = split.First().ToLower();

            if (command.StartsWith("&"))
                command = command.Substring(2);


            bool cmd = false;
            foreach (var p in Prefix)
            {
                if (command.StartsWith(p))
                {
                    cmd = true;
                    break;
                }
            }
            if (!cmd) return false;
            command = command.Substring(1);

            if (!Commands.ContainsKey(command))
            {
                return false;
            }

            split.RemoveAt(0);
            Commands[command].Execute(this.Client, player, split.ToArray());
            return true;
        }
        public void OnMessage(object sender, ClassicConnect.Event.PlayerEvents.ChatEventArgs ev)
        {
            string message = ev.Message.Trim();
            if (message.Length <= 1)
                return;

            if (!message.Contains(":")) return;

            int colonIndex = message.IndexOf(":") + 1;
            if (colonIndex >= message.Length) return;

            string playername = message.Substring(0, colonIndex-1).Trim();
            message = message.Substring(colonIndex).Trim();

            if (message.StartsWith("&"))
                message = message.Substring(2);
            if (playername.StartsWith("&"))
                playername = playername.Substring(2);


            HandleMessage(playername, message, ev.PlayerId);
        }
    }
}
