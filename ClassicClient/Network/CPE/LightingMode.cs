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
            byte lightingmode = (byte)stream.ReadByte();
            byte locked = (byte)stream.ReadByte();
        }
    }
}
