using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class CustomBlocks : ClassicPacket
    {
        public override byte PacketID => 0x13;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte supportedlevel = (byte)stream.ReadByte();
            connection.SendBytes(GetBytes(supportedlevel));
        }

        public static byte[] GetBytes(byte supportedlevel=1)
        {
            return new byte[2] { 0x13, supportedlevel };
        }
    }
}
