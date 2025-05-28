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
            byte block = (byte)stream.ReadByte();
            byte allowPlace = (byte)stream.ReadByte();
            byte allowBreak = (byte)stream.ReadByte();
        }
    }
}
