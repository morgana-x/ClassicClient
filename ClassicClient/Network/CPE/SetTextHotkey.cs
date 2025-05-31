using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class SetTextHotkey : ClassicPacket
    {
        public override byte PacketID => 0x15;
        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 133);
        }
    }
}
