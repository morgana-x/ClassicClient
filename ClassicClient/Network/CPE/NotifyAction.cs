using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class NotifyAction : ClassicPacket
    {
        public override byte PacketID => 0x39;

        public byte[] GetBytes(ushort action, short data)
        {
            byte[] packet = new byte[4];
            Util.InsertBytes(ref packet, 0,  Util.Reverse(BitConverter.GetBytes(action)));

            Util.InsertBytes(ref packet, 2, Util.Reverse(BitConverter.GetBytes(data)));
            return packet;
        }
    }
}
