using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class HackControl : ClassicPacket
    {
        public override byte PacketID => 0x20;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 7);

            byte[] settings = new byte[5];
            Array.Copy(data, settings, 5);
            short jumpheight = Util.ReadShort(data, 5);
        }
    }
}
