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

        public bool LocalPlayer => ID == -1;

        public int Rank { get { return LocalPlayer ? int.MaxValue : Command.Rank.GetRank(Name); } set { if (!LocalPlayer) { Command.Rank.SetRank(Name, value); } } }

        public string Color = "";
       
        public void SetPositionRotation(short x, short y, short z, byte yaw, byte pitch)
        {
            SetPosition(x, y, z);
            SetRotation(yaw, pitch);
        }
        public void SetPosition(short x, short y, short z)
        {
            X = x; Y = y; Z = z;
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

        public ClassicPlayer(sbyte id, string name, short x, short y, short z, byte yaw, byte pitch)
        {
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
