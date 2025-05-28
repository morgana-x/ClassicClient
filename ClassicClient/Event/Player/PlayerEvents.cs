

using ClassicConnect.Player;

namespace ClassicConnect.Event
{
    public partial class PlayerEvents
    { 
        public class ChatEventArgs : EventArgs
        {
            public string Message;
            public sbyte PlayerId;

            public ChatEventArgs(sbyte PlayerId, string Message)
            {
                this.PlayerId = PlayerId;
                this.Message = Message;
            }
        }
        public event EventHandler<ChatEventArgs> ChatEvent;
        internal void OnPlayerChat(ChatEventArgs e)
        {
            ChatEvent.Invoke(this, e);
        }


        public class SpawnEventArgs : EventArgs
        {
            public ClassicPlayer Player;
            public sbyte PlayerId;

            public SpawnEventArgs(sbyte PlayerId, ClassicPlayer player)
            {
                this.PlayerId = PlayerId;
                this.Player = player;
            }
        }
        public event EventHandler<SpawnEventArgs> SpawnEvent;
        internal void OnPlayerSpawn(SpawnEventArgs e)
        {
            SpawnEvent.Invoke(this, e);
        }

        public class DespawnPlayerArgs : EventArgs
        {
            public ClassicPlayer Player;
            public sbyte PlayerId;

            public DespawnPlayerArgs(sbyte PlayerId, ClassicPlayer player)
            {
                this.PlayerId = PlayerId;
                this.Player = player;
            }
        }
        public event EventHandler<DespawnPlayerArgs> DepawnEvent;
        internal void OnPlayerDepawn(DespawnPlayerArgs e)
        {
            DepawnEvent.Invoke(this, e);
        }
    }
    
}
