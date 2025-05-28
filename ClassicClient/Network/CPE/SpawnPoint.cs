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
            short[] spawn = new short[3];
            for (int i = 0; i < 3; i++)
                spawn[i] = Util.ReadShort(stream);

            byte yaw = (byte)stream.ReadByte();
            byte pitch = (byte)stream.ReadByte();
        }
    }
}
