
namespace ClassicConnect.Network.Player
{
    public class Message : ClassicPacket
    {
        public override byte PacketID { get { return 0x0D; } }

        private static byte[] GetBytesSingle(byte[] encodedMessage, int unusedByte = 0xFF)
        {
            byte[] packet = new byte[66];
            packet[0] = 0x0D;
            packet[1] = (byte)unusedByte;
            Util.InsertBytes(ref packet, 2, encodedMessage);
            return packet;
        }

        public static byte[] GetBytes(string message, bool CPE=false)
        {
            byte[][] splitmessages = Util.EncodeStringMultiline(message);
            byte[] packetsData = new byte[66 * splitmessages.Length];

            for (int i = 0; i < splitmessages.Length; i++)
                Util.InsertBytes(ref packetsData, i * 66, GetBytesSingle(splitmessages[i], CPE ? (i < splitmessages.Length-1 ? 1 : 0) : 0xFF));

            return packetsData;
        }

        public override void Read(ClassicClient connection, Stream stream)
        {
            sbyte playerId = (sbyte)stream.ReadByte();
            string message = Util.DecodeString(stream);

            connection.Events.PlayerEvents.OnPlayerChat(new(playerId, message));
        }
    }
}
