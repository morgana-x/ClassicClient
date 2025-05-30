using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class TwoWayPing : ClassicPacket
    {
        public override byte PacketID => 0x2B;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 3);

            byte direction = data[0];

            short someData = Util.ReadShort(data,1);

            if (direction != 0)
                connection.SendBytes(GetBytes());
        }
        public static byte[] GetBytes()
        {
            return new byte[4] { 0x2B, 0x0, 0x0, 0x0 };

        }
    }
}
