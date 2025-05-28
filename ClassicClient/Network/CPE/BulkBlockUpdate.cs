using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class BulkBlockUpdate : ClassicPacket
    {
        public override byte PacketID => 0x26;

        public override void Read(ClassicClient connection, Stream stream)
        {
            int numUpdates = stream.ReadByte() + 1;
            byte[] indiceBuffer = new byte[1024];
            stream.Read(indiceBuffer);
            byte[] blockBuffer = new byte[256];
            stream.Read(blockBuffer);
        }
    }
}
