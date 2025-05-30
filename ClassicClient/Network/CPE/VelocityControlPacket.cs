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
            byte[] buffer = Util.ReadBytes(stream, 15);

            int VelX = Util.ReadInt(buffer,0);
            int VelY = Util.ReadInt(buffer,4);
            int VelZ = Util.ReadInt(buffer,8);

            byte modeX = buffer[12];
            byte modeY = buffer[13];
            byte modeZ = buffer[14];
        }
    }
}
