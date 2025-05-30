using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class BlockPermission : ClassicPacket
    {
        public override byte PacketID => 0x1C;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 3);

            byte block = data[0];
            byte allowPlace = data[1];
            byte allowBreak = data[2];
        }
    }
}
