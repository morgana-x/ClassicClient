namespace ClassicConnect.Player
{
    public class ClassicPlayer
    {
        public sbyte ID = -1;

        public string Name = "";

        
        public volatile short X = 0;
        public volatile short Y = 0;
        public volatile short Z = 0;

        public short BlockX { get { return (short)(X >> 5); } }
        public short BlockY { get { return (short)(Y >> 5); } }
        public short BlockZ { get { return (short)(Z >> 5); } }

        public volatile byte Yaw = 0;
        public volatile byte Pitch = 0;

        // https://github.com/ClassiCube/MCGalaxy/blob/master/MCGalaxy/util/Math/DirUtils.cs
        public double[] LookVector { get {
                const double packed2Rad = (2 * Math.PI) / 256.0;

                double yaw = Yaw * packed2Rad;
                double pitch = Pitch * packed2Rad;

                double x = Math.Sin(yaw) * Math.Cos(pitch);
                double y = -Math.Sin(pitch);
                double z = -Math.Cos(yaw) * Math.Cos(pitch);
                return new double[] { x, y, z };
            } }

        public bool LocalPlayer => ID == -1;

        public ClassicClient client;

        public int Rank { get { return LocalPlayer ? int.MaxValue : Command.Rank.GetRank(Name); } set { if (!LocalPlayer) { Command.Rank.SetRank(Name, value); } } }

        public string Color = "";
       
        public void SetPositionRotation(short x, short y, short z, byte yaw, byte pitch)
        {
            SetRotation(yaw, pitch);
            SetPosition(x, y, z);
        }
        public void SetPosition(short x, short y, short z)
        {
            X = x; Y = y; Z = z;
        }

        public void SetBlockPosition(short x, short y, short z)
        {
            SetPosition((short)(x << 5), (short)(y << 5), (short)(z << 5));
        }

        public int BlockDistance(short x, short y, short z)
        {
            return (Math.Abs(x - BlockX) + Math.Abs(y - BlockY) + Math.Abs(z - BlockZ));
        }

        public void SetRotation(byte yaw, byte pitch)
        {
            Yaw = yaw;
            Pitch = pitch;
        }

        public void UpdatePosition(sbyte x, sbyte y, sbyte z)
        {
            X += x;
            Y += y;
            Z += z;
        }

        public ClassicPlayer(ClassicClient client, sbyte id, string name, short x, short y, short z, byte yaw, byte pitch)
        {
            this.client = client;
            ID = id;
            if (name.StartsWith("&") && name.Length >= 3)
            {
                Color = name.Substring(0, 2);
                name = name.Substring(2);
            }
            Name = name;
            SetPositionRotation(x, y, z, yaw, pitch);
        }
    }
}
