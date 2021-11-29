using System;
using System.Linq;
using System.Text;

namespace Lab4
{
    class Utils
    {
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
        }

        public static string ByteArrayToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] StringToByteArray(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static string FromStringToHex(string str)
        {
            return ByteArrayToHexString(StringToByteArray(str));
        }

        public static string FromHexToString(string hex)
        {
            return ByteArrayToString(HexStringToByteArray(hex));
        }
    }
}
