using System.Text;

namespace ClassicConnect
{
    public class Util
    {
        public static bool CPE = false;
        public static bool FullCP437 = false;

        public static System.Random Random = new System.Random();

        // https://github.com/ClassiCube/MCGalaxy/blob/master/MCGalaxy/Chat/EmotesHandler.cs#L46
       
        /// <summary> Conversion for code page 437 characters from index 0 to 31 to unicode. </summary>
        public const string ControlCharReplacements = "\0☺☻♥♦♣♠•◘○◙♂♀♪♫☼►◄↕‼¶§▬↨↑↓→←∟↔▲▼";

        /// <summary> Conversion for code page 437 characters from index 127 to 255 to unicode. </summary>
        public const string ExtendedCharReplacements = "⌂ÇüéâäàåçêëèïîìÄÅÉæÆôöòûùÿÖÜ¢£¥₧ƒáíóúñÑªº¿⌐¬½¼¡«»" +
            "░▒▓│┤╡╢╖╕╣║╗╝╜╛┐└┴┬├─┼╞╟╚╔╩╦╠═╬╧╨╤╥╙╘╒╓╫╪┘┌" +
            "█▄▌▐▀αßΓπΣσµτΦΘΩδ∞φε∩≡±≥≤⌠⌡÷≈°∙·√ⁿ²■\u00a0";

        public static byte EncodeCP437(char c)
        {
            if (c >= ' ' && c <= '~') return (byte)c;
            int cpIndex = 0;
            if ((cpIndex = ControlCharReplacements.IndexOf(c)) >= 0)
                return (byte)cpIndex;

            if ((cpIndex = ExtendedCharReplacements.IndexOf(c)) >= 0)
                return (byte)cpIndex;

            return (byte)'?';

        }
        public static byte[] EncodeCP437(string input)
        {
            List<byte> encoded = new List<byte>();
            foreach (var a in input)
                encoded.Add(EncodeCP437(a));

            return encoded.ToArray();
        }
        public static string DecodeCP437(byte[] input)
        {
            return Encoding.ASCII.GetString(input);
        }
        private static byte[] EncodeString(byte[] rawBytes)
        {
            byte[] converted = new byte[64];
            for (int i = 0; i < 64; i++)
                converted[i] = (i < rawBytes.Length) ? rawBytes[i] : (byte)0x20;

            return converted;
        }
        public static byte[] EncodeString(string input)
        {
            return EncodeString( FullCP437 ? EncodeCP437(input) : Encoding.ASCII.GetBytes(input));
        }
        public static byte[][] EncodeStringMultiline(string input)
        {
            List<byte[]> convertedBytes = new List<byte[]>();
            byte[] rawbytes = FullCP437 ? EncodeCP437(input) : Encoding.ASCII.GetBytes(input);

            for (int i=0; i< rawbytes.Length; i += 64)
                convertedBytes.Add(EncodeString(rawbytes.Skip(i).ToArray()));

            return convertedBytes.ToArray();
        }

        public static string DecodeString(byte[] input)
        {
            return FullCP437 ? DecodeCP437(input).TrimEnd() : Encoding.ASCII.GetString(input).TrimEnd();
        }

        public static string ReadString(Stream stream)
        {
            byte[] buffer = new byte[64];
            stream.Read(buffer);
            return DecodeString(buffer);
        }

        public static string ReadString(byte[] array, int index = 0)
        {
            byte[] buffer = new byte[64];
            Array.Copy(array, index, buffer, 0, 64);
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

        public static short ReadShort(Stream stream, bool bigendian=true)
        {
            return BitConverter.ToInt16(ReadBytes(stream, 2, bigendian));
        }
        public static short ReadShort(byte[] data, int offset=0, bool bigendian = true)
        {
            byte[] buffer = new byte[2];
            Array.Copy(data, offset, buffer, 0, 2);
            if (bigendian)
                Array.Reverse(buffer);
            return BitConverter.ToInt16(buffer);
        }

        public static int ReadInt(Stream stream, bool bigendian = true)
        {
            return BitConverter.ToInt32(ReadBytes(stream, 4, bigendian));
        }

        public static int ReadInt(byte[] data, int offset=0, bool bigendian = true)
        {
            byte[] buffer = new byte[4];
            Array.Copy(data, offset, buffer, 0, 4);
            if (bigendian)
                Array.Reverse(buffer);
            return BitConverter.ToInt32(buffer);
        }

        public static void InsertBytes(ref byte[] target, int index, byte[] insert)
        {
            for (int i=0; i<insert.Length && i+index < target.Length; i++)
                target[i+index] = insert[i];
        }

        public static float[] DirVec(byte pitch, byte yaw)
        {
            return new float[3] { 0, 0, 0 };
        }
    }
}
