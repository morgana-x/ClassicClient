using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassicConnect.Player;

namespace ClassicConnect.Event
{
    public class CoreEvents
    {
        public class DisconnectEventArgs : EventArgs
        {
            public string Reason;

            public DisconnectEventArgs(string reason)
            {
                Reason = reason;
            }
        }
        public event EventHandler<DisconnectEventArgs> DisconnectEvent;
        internal void OnDisconnect(DisconnectEventArgs e)
        {
            DisconnectEvent.Invoke(this, e);
        }
    }
}
