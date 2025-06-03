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
        string name = "morgana2";
        string password = "";
        string remember_token = "";
        string logindetailpath = Path.Join(Directory.GetCurrentDirectory(), "login.txt");
        if (File.Exists(logindetailpath))
        {
            string[] lines = File.ReadAllLines(logindetailpath);
            name = lines[0].Trim();
            password = lines[1].Trim();
            remember_token = lines[2].Trim();
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


        bool result = client.ConnectClassicube("5bd63bee8f109bbf619234e5d533b445"); //client.Connect("131.161.69.89",25566);

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
