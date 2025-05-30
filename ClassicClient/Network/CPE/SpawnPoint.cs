using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class SpawnPoint : ClassicPacket
    {
        public override byte PacketID => 0x2E;

        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 8);

            short[] spawn = new short[3];
            for (int i = 0; i < 3; i++)
                spawn[i] = Util.ReadShort(data, i*2);

            byte yaw = data[6];
            byte pitch = data[7];
        }
    }
}
