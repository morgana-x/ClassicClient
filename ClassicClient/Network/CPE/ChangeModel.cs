using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class ChangeModel : ClassicPacket
    {
        public override byte PacketID => 0x1D;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte entityID = (byte)stream.ReadByte();
            string model = Util.ReadString(stream);
        }
    }
}
