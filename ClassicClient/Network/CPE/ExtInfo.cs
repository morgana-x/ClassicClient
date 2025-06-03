
namespace ClassicConnect.Network.CPE
{
    public class ExtInfo : ClassicPacket
    {
        public override byte PacketID { get { return 0x10; } }
        public override void Read(ClassicClient connection, Stream stream)
        {
            byte[] data = Util.ReadBytes(stream, 66);

            string appname = Util.ReadString(data);
            short extensionCount = Util.ReadShort(data, 64);

            Console.WriteLine("Received" + extensionCount.ToString() + "extension count");

            for (int i = 0; i < extensionCount; i++)
                ClassicPacketHandler.ReadPacket(connection, stream);

            ClassicConnect.CPE.CPEEnabled = true;
            ClassicConnect.Util.CPE = true;

            ClassicConnect.CPE.SendCompatibleCPE(connection);
        }

        public static byte[] GetBytes(short numberOfExtensions, string clientName = "ClassiCube 1.3.7")
        {
            byte[] packet = new byte[67];
            packet[0] = 0x10;
            Util.InsertBytes(ref packet, 1, Util.EncodeString(clientName));
            Util.InsertBytes(ref packet, 64, BitConverter.GetBytes(numberOfExtensions));
            return packet;
        }
    }
}
