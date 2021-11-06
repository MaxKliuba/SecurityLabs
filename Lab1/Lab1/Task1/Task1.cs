using System;
using System.Text;

namespace Lab1
{
    class Task1
    {
        public static void Run(string input)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("*************** [TASK 1] ***************");
            Console.ResetColor();

            string inputText = FromHexString(input);
            Result result = CaesarCracker.DecodeXorBruteforce(inputText);

            Console.WriteLine();
            Console.WriteLine("[RESULT]:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.ResetColor();
            Console.WriteLine();
            //Console.WriteLine(Caesar.DecodeXor(inputText, 55));

            /*
             * Now try a repeating-key XOR cipher. E.g. it should take a string ??hello world?? and, given the key is ??key??, xor the first letter ??h?? with ??k??, 
             * then xor ??e?? with ??e??, then ??l?? with ??y??, and then xor next char ??l?? with ??k?? again, then ??o?? with ??e?? and so on. 
             * You may use an index of coincidence, Hamming distance, Kasiski examination, statistical tests or whatever method you feel would show the best result/
             */

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("****************************************");
            Console.WriteLine();
            Console.ResetColor();
        }

        private static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.UTF8.GetString(bytes);
        }
    }
}
