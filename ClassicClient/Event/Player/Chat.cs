

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
    }
    
}
