using System;
using System.Text;

namespace Lab1
{
    class Task0
    {
        public static void Run(string input)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("*************** [TASK 0] ***************");
            Console.ResetColor();

            string inputBinDecoded = FromBinString(input);
            //Console.WriteLine(inputBinDecoded);

            string inputBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(inputBinDecoded));
            Console.WriteLine(inputBase64Decoded);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("****************************************");
            Console.WriteLine();
            Console.ResetColor();
        }

        private static string FromBinString(string binString)
        {
            var bytes = new byte[binString.Length / 8];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(binString.Substring(i * 8, 8), 2);
            }

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
