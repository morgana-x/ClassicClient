using System.Text;

namespace ClassicConnect
{
    public class Util
    {
        private static byte[] EncodeString(byte[] rawBytes)
        {
            byte[] converted = new byte[64];
            for (int i = 0; i < 64; i++)
                converted[i] = (i < rawBytes.Length) ? rawBytes[i] : (byte)0x20;

            return converted;
        }
        public static byte[] EncodeString(string input)
        {
            return EncodeString(Encoding.ASCII.GetBytes(input));
        }
        public static byte[][] EncodeStringMultiline(string input)
        {
            List<byte[]> convertedBytes = new List<byte[]>();
            byte[] rawbytes = Encoding.ASCII.GetBytes(input);

            for (int i=0;i<rawbytes.Length; i += 64)
                convertedBytes.Add(EncodeString(rawbytes.Skip(i*64).ToArray()));

            return convertedBytes.ToArray();
        }

        public static string DecodeString(byte[] input)
        {
            return Encoding.ASCII.GetString(input).TrimEnd();
        }

        public static string DecodeString(Stream stream)
        {
            byte[] buffer = new byte[64];
            stream.Read(buffer);
            return DecodeString(buffer);
        }

        public static byte[] ReadBytes(Stream stream, int amount, bool bigendian=false)
        {
            byte[] buffer = new byte[amount];
            stream.Read(buffer);

            if (bigendian)
                Array.Reverse(buffer);

            return buffer;
        }

        public static byte[] Reverse(byte[] bytes) // quick bad cheap fix for inline reversal
        {
            Array.Reverse(bytes);
            return bytes;
        }

        public static void InsertBytes(ref byte[] target, int index, byte[] insert)
        {
            for (int i=0; i<insert.Length && i+index < target.Length; i++)
                target[i+index] = insert[i];
        }
    }
}
