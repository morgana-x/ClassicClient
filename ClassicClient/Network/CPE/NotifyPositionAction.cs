using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassicConnect.Player;

namespace ClassicConnect.Network.CPE
{
    internal class NotifyPositionAction : ClassicPacket
    {
        public override byte PacketID => 0x3A;

        public static byte[] GetBytes(ushort action, ushort x, ushort y, ushort z)
        {
            byte[] packet = new byte[9];
            packet[0] = 0x3A;

            Util.InsertBytes(ref packet, 1, Util.Reverse(BitConverter.GetBytes(action)));

            Util.InsertBytes(ref packet, 3, Util.Reverse(BitConverter.GetBytes(x)));
            Util.InsertBytes(ref packet, 5, Util.Reverse(BitConverter.GetBytes(y)));
            Util.InsertBytes(ref packet, 7, Util.Reverse(BitConverter.GetBytes(z)));
            return packet;
        }
        public static byte[] GetBytes(ushort action, short x, short y, short z) {
            return GetBytes(action, (ushort)x, (ushort)y, (ushort)(z));
        }
        public static byte[] GetBytes(ushort action, ClassicPlayer pl)
        {
            return GetBytes(action, (ushort)pl.X, (ushort)pl.Y, (ushort)pl.Z);
        }
    }
}
