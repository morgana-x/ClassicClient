namespace ClassicConnect
{
    public class ClassicServer
    {
        public int ServerProtocol = 0x07;

        public string ServerName = "Unknown Name";
        public string ServerMotd = "";

        public int UserType = 0x0; // 0x64 for op

        public bool CPE = false;

        public List<string> CPEExtensions = new List<string>();
    }
}
