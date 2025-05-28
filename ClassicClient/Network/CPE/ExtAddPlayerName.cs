using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class ExtAddPlayerName : ClassicPacket
    {
        public override byte PacketID => 0x16;

        public override void Read(ClassicClient connection, Stream stream)
        {
            short nameid = Util.ReadShort(stream);
            string playername = Util.ReadString(stream);
            string listname = Util.ReadString(stream);
            string groupname = Util.ReadString(stream);
            byte rank = (byte)stream.ReadByte();
        }
    }
}
