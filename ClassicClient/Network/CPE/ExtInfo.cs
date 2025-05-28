
namespace ClassicConnect.Network.CPE
{
    public class ExtInfo : ClassicPacket
    {
        public override byte PacketID { get { return 0x10; } }
        public override void Read(ClassicClient connection, Stream stream)
        {
            string appname = Util.ReadString(stream);
            short extensionCount = Util.ReadShort(stream);

            Console.WriteLine("Received" + extensionCount.ToString() + "extension count");

            for (int i = 0; i < extensionCount; i++)
                ClassicPacketHandler.ReadPacket(connection, stream);

            ClassicConnect.CPE.CPEEnabled = true;
            ClassicConnect.Util.CPE = true;

            ClassicConnect.CPE.SendCompatibleCPE(connection);
        }

        public static byte[] GetBytes(short numberOfExtensions)
        {
            byte[] packet = new byte[67];
            packet[0] = 0x10;
            Util.InsertBytes(ref packet, 1, Util.EncodeString("hello"));
            Util.InsertBytes(ref packet, 64, BitConverter.GetBytes(numberOfExtensions));
            return packet;
        }
    }
}
