using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    class CaesarCracker
    {
        public static string DecodeXor(string input, int key)
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                output.Append((char)(input[i] ^ key));
            }

            return output.ToString();
        }

        public static Result DecodeXorBruteforce(string input)
        {
            List<Result> results = new List<Result>();

            for (byte key = 0; key < byte.MaxValue; key++)
            {
                results.Add(new Result(((char)key).ToString(), DecodeXor(input, key)));
            }

            int index = 1;
            float maxCount = 0;

            for (int i = 1; i < results.Count; i++)
            {
                float p = GetTextPercent(results[i].Value);

                Console.WriteLine($"[{p}%]\t-> {results[i]}");
                if (p > maxCount)
                {
                    maxCount = p;
                    index = i;
                }
            }

            return results[index];
        }

        private static float GetTextPercent(string str)
        {
            float n = 0;

            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || ch == '.' || ch == ',' || ch == ' ' || ch == '/' || ch == ':' || ch == '-')
                {
                    n++;
                }
            }

            return n / str.Length * 100.0f;
        }
    }
}
