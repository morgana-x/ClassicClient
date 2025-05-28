using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class VelocityControlPacket : ClassicPacket
    {
        public override byte PacketID => 0x2F;

        public override void Read(ClassicClient connection, Stream stream)
        {
            int VelX = Util.ReadInt(stream);
            int VelY = Util.ReadInt(stream);
            int VelZ = Util.ReadInt(stream);

            byte modeX = (byte)stream.ReadByte();
            byte modeY = (byte)stream.ReadByte();
            byte modeZ = (byte)stream.ReadByte();
        }
    }
}
