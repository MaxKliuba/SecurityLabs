using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    class XSalsa20Cracker
    {
        public static void Analyze(List<byte[]> ciphers, string word)
        {
            float bestTextPercent = 0;
            List<string> bestText = new List<string>(ciphers.Count);

            for (int first = 0; first < ciphers.Count; first++)
            {
                List<string> currentText = new List<string>(ciphers.Count);

                for (int second = 0; second < ciphers.Count; second++)
                {
                    int minLength = Math.Min(ciphers[first].Length, ciphers[second].Length);
                    byte[] cipherArrayFirst = new byte[minLength];
                    Array.Copy(ciphers[first], cipherArrayFirst, minLength);
                    byte[] cipherArraySecond = new byte[minLength];
                    Array.Copy(ciphers[second], cipherArraySecond, minLength);

                    byte[] xorArray = XSalsa20Cracker.Xor(cipherArrayFirst, cipherArraySecond);
                    byte[] wordArray = XSalsa20Cracker.StringToByteArray(word);
                    byte[] resultArray = XSalsa20Cracker.Xor(xorArray, wordArray);
                    string resultStr = XSalsa20Cracker.ByteArrayToString(resultArray);
                    currentText.Add(resultStr);
                    Console.WriteLine($"[{first}][{second}]: \t" + resultStr);
                }
                Console.WriteLine();

                StringBuilder stringBuilder = new StringBuilder();
                foreach (string line in currentText)
                {
                    int length = Math.Min(word.Length, line.Length);
                    stringBuilder.Append(line.Substring(0, length));
                }

                float currentTextPercent = XSalsa20Cracker.GetTextPercent(stringBuilder.ToString());
                if (bestTextPercent < currentTextPercent)
                {
                    bestTextPercent = currentTextPercent;
                    bestText = currentText;
                }
            }

            Console.WriteLine("[BEST RESULT]:");
            for (int i = 0; i < bestText.Count; i++)
            {
                int length = Math.Min(word.Length, bestText[i].Length);
                Console.Write($"[{i}]:\t");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(bestText[i].Substring(0, length));
                Console.ResetColor();
                Console.WriteLine(bestText[i].Substring(length));
            }
        }

        public static byte[] Xor(byte[] input, byte[] key)
        {
            byte[] output = new byte[input.Length];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (byte)(input[i] ^ key[i % key.Length]);
            }

            return output;
        }

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

        public static float GetTextPercent(string str)
        {
            float n = 0;

            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || ch == '.' || ch == ',' || ch == ' ' || ch == '\'' || ch == ':' || ch == '-')
                {
                    n++;
                }
            }

            return n / str.Length * 100.0f;
        }
    }
}
