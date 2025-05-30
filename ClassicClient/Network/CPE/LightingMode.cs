using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class LightingMode : ClassicPacket
    {
        public override byte PacketID => 0x37;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream,2);

            byte lightingmode = data[0];
            byte locked = data[1];
        }
    }
}
