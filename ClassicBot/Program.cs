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
        string name = "morgana";
        string password = "";
        ClassicClient client = new ClassicClient(name);//, password);
        client.Events.PlayerEvents.ChatEvent += OnMessage;
        client.Events.PlayerEvents.SpawnEvent += OnPlayerSpawn;
        client.Events.PlayerEvents.DepawnEvent += OnPlayerDespawn;
        client.Events.LevelEvents.StartLoadEvent += OnStartLoad;
        client.Events.LevelEvents.LoadChunkEvent += OnLoadChunk;
        client.Events.LevelEvents.OnFinishLoadEvent += OnFinishLoad;
        client.Events.CoreEvents.DisconnectEvent += OnKick;
        client.Events.LevelEvents.SetBlockEvent += OnBlockPlace;

        bool result = client.Connect("localhost", 25565);// "51.195.219.231", 25565, "ed9bb1717a0caae1904bfde4c53638e6");

        if (!result)
        {
            Console.WriteLine("Failed to connect!");
            return;
        }

        while (client.Client.Connected)
        {
            client.SendMessage(Console.ReadLine());
        }

    }
}
