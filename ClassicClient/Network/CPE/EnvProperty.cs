using System;
using System.Collections.Generic;
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
            byte property = (byte)stream.ReadByte();
            int value = Util.ReadInt(stream);

        }
    }
}
