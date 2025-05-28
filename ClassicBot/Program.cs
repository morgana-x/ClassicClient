using ClassicConnect;

public partial class Program
{
    private static void OnMessage(object? sender, ClassicConnect.Event.PlayerEvents.ChatEventArgs eventargs)
    {
        Console.WriteLine(eventargs.Message);
    }
    public static void Main(string[] args)
    {
        string name = "testbot! :D";

        ClassicClient client = new ClassicClient(name);

        client.Events.PlayerEvents.ChatEvent += OnMessage;

        client.Connect("131.161.69.89:25566");

        while (true)
        {
            client.SendMessage(Console.ReadLine());
        }

    }
}
