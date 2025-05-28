namespace ClassicConnect.Player
{
    public class ClassicPlayer
    {
        public sbyte ID = -1;

        public string Name = "";

        public volatile short X = 0;
        public volatile short Y = 0;
        public volatile short Z = 0;

        public volatile byte Yaw = 0;
        public volatile byte Pitch = 0;

        public bool LocalPlayer => ID == -1;

        public void SetPositionRotation(short x, short y, short z, byte yaw, byte pitch)
        {
            X = x; Y = y; Z = z;
            Yaw = yaw;
            Pitch = pitch;
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
            Name = name;
            SetPositionRotation(x, y, z, yaw, pitch);
        }
    }
}
