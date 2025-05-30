using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class EnvProperty : ClassicPacket
    {
        public override byte PacketID => 0x29;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 5);

            byte property = data[0];
            int value = Util.ReadInt(data, 1);

        }
    }
}
