
namespace ClassicConnect.Network.CPE
{
    public class ExtEntry : ClassicPacket
    {
        public override byte PacketID => 0x11;

        public override void Read(ClassicClient connection, Stream stream)
        {
            string name = Util.ReadString(stream);
            int version = BitConverter.ToInt32(Util.ReadBytes(stream, 4, true));

            Console.WriteLine($"Received ExtEntry {name} {version}");
            if (!connection.ConnectedServer.CPEExtensions.Contains(name))
                connection.ConnectedServer.CPEExtensions.Add(name);

            if (ClassicConnect.CPE.EnabledCPE.ContainsKey(name))
                ClassicConnect.CPE.EnabledCPE[name] = true;

            if (!ClassicConnect.CPE.CPEPurportedVersions.ContainsKey(name))
                ClassicConnect.CPE.CPEPurportedVersions.Add(name, version);
            else
                ClassicConnect.CPE.CPEPurportedVersions[name] = version;

        }

        public static byte[] GetBytes(string name, int version)
        {
            byte[] packet = new byte[69];
            packet[0] = 0x11;
            Util.InsertBytes(ref packet, 1, Util.EncodeString(name));
            Util.InsertBytes(ref packet, 64, Util.Reverse(BitConverter.GetBytes(version)));
            return packet;
        }
    }
}
