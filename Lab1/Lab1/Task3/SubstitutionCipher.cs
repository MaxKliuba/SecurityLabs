using System;
using System.Collections.Generic;

namespace Lab1
{
    class SubstitutionCipher
    {
        private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string Decode(string input, string key)
        {
            string cipher = input.ToUpper();
            string output = "";

            for (int i = 0; i < cipher.Length; i++)
            {
                int index = key.IndexOf(cipher[i]);
                output += ALPHABET[index];
            }

            return output;
        }

        public static string GetStartingKey(string input)
        {
            List<NGram> monograms = NGram.Parse(FileReader.Read("..\\..\\..\\ngrams\\english_monograms.txt"));
            //foreach (NGram nGram in monograms)
            //{
            //    Console.WriteLine(nGram);
            //}
            //Console.WriteLine();

            List<NGram> inputMonograms = NGram.CreateList(input);
            //foreach (NGram monogram in inputMonograms)
            //{
            //    Console.WriteLine(monogram);
            //}
            //Console.WriteLine();

            char[] array = new char[ALPHABET.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = '_';
            }

            for (int i = 0; i < inputMonograms.Count; i++)
            {
                int index = ALPHABET.IndexOf(monograms[i].Value[0]);
                array[index] = inputMonograms[i].Value[0];
            }

            Random rnd = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals('_'))
                {
                    char ch;
                    do
                    {
                        ch = ALPHABET[rnd.Next(0, ALPHABET.Length)];
                    } while ((new string(array)).Contains(ch));

                    array[i] = ch;
                }
            }

            return new string(array);
        }

        public static string SwapSymbolInKey(string key, char inputChar1, char inputChar2)
        {
            char char1 = key[ALPHABET.IndexOf(inputChar1)];
            char char2 = key[ALPHABET.IndexOf(inputChar2)];

            string newKey = key.Replace(char1, '_');
            newKey = newKey.Replace(char2, char1);
            newKey = newKey.Replace('_', char2);

            return newKey;
        }
    }
}
