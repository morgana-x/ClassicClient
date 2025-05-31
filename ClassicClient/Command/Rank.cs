namespace ClassicConnect.Command
{
    public class Rank
    {
        public static Dictionary<string, int> Ranks = new Dictionary<string, int>();
        public static int GetRank(string name)
        {
            return Ranks.ContainsKey(name) ? Ranks[name] : 0;
        }
        public static string SaveFilePath = "";
        public static void SetRank(string name, int rank)
        {
            if (!Ranks.ContainsKey(name))
                Ranks.Add(name, rank);
            Ranks[name] = rank;

            if (SaveFilePath != "")
                Save(SaveFilePath);
        }

        public static void Load(string filepath)
        {
            SaveFilePath = filepath;
            if (!File.Exists(filepath)) return;

            using FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            using BinaryReader br = new BinaryReader(fs);

            while (fs.Position < fs.Length)
            {
                string name = br.ReadString();
                int rank = br.ReadInt32();
                if (Ranks.ContainsKey(name))
                    Ranks[name] = rank;
                else
                    Ranks.Add(name, rank);
            }
            br.Dispose();
        }

        public static void Save(string filepath)
        {

            using FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            using BinaryWriter br = new BinaryWriter(fs);
            foreach(var pair in Ranks)
            {
                br.Write(pair.Key);
                br.Write(pair.Value);
            }
            br.Dispose();
        }

    }
}
