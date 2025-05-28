using System.Numerics;

namespace ClassicConnect.Player
{
    public class ClassicPlayerList
    {
        public volatile Dictionary<int, ClassicPlayer> PlayerList = new Dictionary<int, ClassicPlayer>();

        public ClassicPlayer LocalPlayer;
        public ClassicClient Client;
        public ClassicPlayer? GetPlayer(sbyte id)
        {
            if (id == -1)
                return LocalPlayer;

            if (!PlayerList.ContainsKey(id))
                return null;

            return PlayerList[id];
        }
        public void OnStartLoadLevel()
        {
            if (PlayerList.Count > 1)
                for (int i = 0; i < PlayerList.Count - 1; i++)
                    PlayerList.Remove(PlayerList.Keys.ToList()[i]);
        }

        internal void PlayerSpawn(sbyte id, string name, short x, short y, short z, byte yaw, byte pitch)
        {
            if (id == -1)
            {
                LocalPlayer.SetPositionRotation(x, y, z, yaw, pitch);
                Client.Events.PlayerEvents.OnPlayerSpawn(new(id, LocalPlayer));
                return;
            }
            ClassicPlayer player = new ClassicPlayer(id, name, x, y, z, yaw, pitch);

            Client.Events.PlayerEvents.OnPlayerSpawn(new(id, player));

            if (!PlayerList.ContainsKey(id))
            {
                PlayerList.Add(id, player);
                return;
            }

            PlayerList[id] = player;
        }

        internal void PlayerDespawn(sbyte id)
        {
            ClassicPlayer? player = GetPlayer(id);

            if (PlayerList.ContainsKey(id))
                PlayerList.Remove(id);

            Client.Events.PlayerEvents.OnPlayerDepawn(new(id, player != null ? player : new ClassicPlayer(id, "Unknown", 0, 0, 0, 0, 0)));
        }

        internal void SetPosRot(sbyte id, short x, short y, short z, byte yaw, byte pitch)
        {
            if (id == -1)
            {
                LocalPlayer.SetPositionRotation(x, y, z, yaw, pitch);
                return;
            }
            var player = GetPlayer(id);
            if (player == null) return;
            player.SetPositionRotation(x, y, z, yaw, pitch);

        }
        internal void UpdatePos(sbyte id, sbyte x, sbyte y, sbyte z)
        {
            var player = GetPlayer(id);
            if (player == null) return;
            player.UpdatePosition(x, y, z);
        }
        internal void UpdatePos(sbyte id, sbyte x, sbyte y, sbyte z, byte yaw, byte pitch)
        {
            var player = GetPlayer(id);
            if (player == null) return;
            player.UpdatePosition(x, y, z);
            player.SetRotation(yaw, pitch);
        }

        internal void UpdateRot(sbyte id, byte yaw, byte pitch)
        {
            var player = GetPlayer(id);
            if (player == null) return;
            player.SetRotation(yaw, pitch);
        }
        public ClassicPlayerList(ClassicClient client,ClassicPlayer localplayer)
        {
            Client = client;
            LocalPlayer = localplayer;
            PlayerList.Add(-1, localplayer);
        }
    }
}
