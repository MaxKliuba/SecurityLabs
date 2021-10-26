using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    class Task2
    {
        public static void Run(string input)
        {
            string inputBase64Decoded = ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(input));
            //Console.WriteLine(inputBase64Decoded);

            Vigenere.AnalyzeKeyLength(inputBase64Decoded);
            int keyLength = 3;

            Result result = Vigenere.DecodeXorBruteforce(inputBase64Decoded, keyLength);
            Console.WriteLine(result);

            /*
             * Write a code to attack some simple substitution cipher. To reduce the complexity of this one we will use only uppercase letters, so the keyspace is only 26! 
             * To get this one right automatically you will probably need to use some sort of genetic algorithm (which worked the best last year), 
             * simulated annealing or gradient descent. Seriously, write it right now, you will need it to decipher the next one as well. 
             * Bear in mind, thereSs☼s no spaces.
             * https://docs.google.com/document/d/1HY7Dl-5itYD3C_gkueBvvBFpT4CecGPiR30BsARlTpQ/edit?usp=sharing+.
             */
        }
    }
}
