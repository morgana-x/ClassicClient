using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicConnect.Network.CPE
{
    internal class BulkBlockUpdate : ClassicPacket
    {
        public override byte PacketID => 0x26;

        public override void Read(ClassicClient connection, Stream stream)
        {
            Console.WriteLine("Reading block buffer update...");
            
            int numUpdates = stream.ReadByte() + 1;
            
            byte[] indiceBuffer = new byte[1024];
            stream.Read(indiceBuffer);

            byte[] blockBuffer = new byte[256];
            stream.Read(blockBuffer);

            int[] indices = new int[numUpdates];
            for (int i = 0; i < numUpdates; i++)
                indices[i] = Util.ReadInt(indiceBuffer, i * 4);

            for (int i = 0; i < numUpdates; i++)
            {
                Console.WriteLine($"Setting block {indices[i]} to {blockBuffer[i]}");
                connection.Level.SetBlock(indices[i], blockBuffer[i]);
            }


            Console.WriteLine("Read block buffer update!");
        }
    }
}
