namespace ClassicConnect.Network.Core
{
    public class ClientIdentify : ClassicPacket
    {
        public override byte PacketID { get { return 0x00; } }
        public static byte[] GetBytes(string Username, string VerificationKey, bool CPE=false)
        {
            byte[] bytes = new byte[131];
            bytes[0] = 0x00;
            bytes[1] = 0x07;

            Util.InsertBytes(ref bytes, 2, Util.EncodeString(Username));
            Util.InsertBytes(ref bytes, 2 + 64, Util.EncodeString(VerificationKey != null ? VerificationKey : ""));

            bytes[2 + 64 + 64] = CPE ? (byte)0x42 : (byte)0x00;

            return bytes;
        }
    }
}
