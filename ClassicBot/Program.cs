using ClassicConnect;

public partial class Program
{
    private static void OnMessage(object? sender, ClassicConnect.Event.PlayerEvents.ChatEventArgs eventargs)
    {
        Console.WriteLine(eventargs.Message);
    }

    private static void OnStartLoad(object? sender, ClassicConnect.Event.LevelEvents.StartLoadEventArgs eventArgs)
    {
        Console.WriteLine("Loading level...");
    }
    private static void OnLoadChunk(object? sender, ClassicConnect.Event.LevelEvents.LoadChunkEventArgs eventArgs)
    {
        Console.WriteLine($"Loading level... ({eventArgs.Percentage}%)");
    }
    private static void OnFinishLoad(object? sender, ClassicConnect.Event.LevelEvents.FinishLoadEventArgs eventArgs)
    {
        Console.WriteLine("Loaded level!");
    }

    private static void OnPlayerSpawn(object? sender, ClassicConnect.Event.PlayerEvents.SpawnEventArgs ev)
    {
        Console.WriteLine($"Spawned player {ev.Player.Name} at {ev.Player.X} {ev.Player.Y} {ev.Player.Z}");
    }

    private static void OnPlayerDespawn(object? sender, ClassicConnect.Event.PlayerEvents.DespawnPlayerArgs ev)
    {
        Console.WriteLine($"Despawned player {ev.Player.Name}");
    }

    private static void OnBlockPlace(object? sender, ClassicConnect.Event.LevelEvents.SetBlockEventArgs ev)
    {
        Console.WriteLine($"Block {ev.X} {ev.Y} {ev.Z} was set to {ev.Block}");
    }

    private static void OnKick(object? sender, ClassicConnect.Event.CoreEvents.DisconnectEventArgs ev)
    {
        Console.WriteLine($"Kicked! Reason {ev.Reason}");
    }
    public static void Main(string[] args)
    {
        string name = "somenamehere";
        string password = "";
        string remember_token = "";

        string logindetailpath = Path.Join(Directory.GetCurrentDirectory(), "account.txt");

        string server_ip = args.Length > 0 ? args[0] : "";
        string mp_pass = "";
        if (File.Exists(logindetailpath))
        {
            string[] lines = File.ReadAllLines(logindetailpath);

            if (lines.Length > 0)
                name = lines[0].Trim();
            if (lines.Length > 1 )
                password = lines[1].Trim();
            if (lines.Length > 2)
                remember_token = lines[2].Trim();
        }
        else
            File.WriteAllText(logindetailpath, "name_here\n\nclassicube_remember_token_here");

        string serverdetailpath = Path.Join(Directory.GetCurrentDirectory(), "server.txt");
        if (File.Exists(serverdetailpath))
        {
            string[] lines = File.ReadAllLines(serverdetailpath);

            if (lines.Length > 0)
                server_ip = lines[0].Trim();
            if (lines.Length > 1)
                mp_pass = lines[1].Trim();
        }
        else
            File.WriteAllText(serverdetailpath, "127.0.0.1:25565\nmp_pass_here");

        if(server_ip == "")
        {
            Console.WriteLine("Please enter the ip addess of the server");
            Console.WriteLine("x.x.x.x:25565 etc or hash value / id of classicube server");
            server_ip = Console.ReadLine();
        }

        ClassicClient client = password=="" ? new ClassicClient(name) : new ClassicClient(name, password, remember_token);

        client.Events.PlayerEvents.ChatEvent += OnMessage;
        client.Events.PlayerEvents.SpawnEvent += OnPlayerSpawn;
        client.Events.PlayerEvents.DepawnEvent += OnPlayerDespawn;
        client.Events.LevelEvents.StartLoadEvent += OnStartLoad;
        client.Events.LevelEvents.LoadChunkEvent += OnLoadChunk;
        client.Events.LevelEvents.OnFinishLoadEvent += OnFinishLoad;
        client.Events.CoreEvents.DisconnectEvent += OnKick;
        client.Events.LevelEvents.SetBlockEvent += OnBlockPlace;


        bool result = Util.IsHex(server_ip) ? client.ConnectClassicube(server_ip) : client.Connect(server_ip, mp_pass); //client.ConnectClassicube("16ac7ccf5b3a454e7681b2b0ea5d5aa2"); //client.Connect("131.161.69.89",25566);

        if (!result)
        {
            Console.WriteLine("Failed to connect!");
            return;
        }

        while (client.Client.Connected)
        {
            client.SendMessageProcessClientCommands(Console.ReadLine());
        }

    }
}
