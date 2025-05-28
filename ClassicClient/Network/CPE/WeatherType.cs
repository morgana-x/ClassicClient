using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class WeatherType : ClassicPacket
    {
        public override byte PacketID => 0x1F;

        public override void Read(ClassicClient connection, Stream stream)
        {
            connection.Level.Weather = (byte)stream.ReadByte();
        }
    }
}
