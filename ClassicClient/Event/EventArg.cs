using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Event
{
    public class ClassicEventArgs : EventArgs
    {
        public ClassicClient Client { get; set; }

        public ClassicEventArgs(ClassicClient client)
        {
            Client = client;
        }
    }
}
