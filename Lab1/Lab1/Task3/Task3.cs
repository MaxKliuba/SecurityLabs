using System;
using System.Collections.Generic;
using System.Text;
using AForge.Genetic;

namespace Lab1
{
    class Task3
    {
        public static void Run(string input)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("*************** [TASK 3] ***************");
            Console.ResetColor();

            // EKMFLODQVXNTGWYHJUSPAIBRCZ
            string key = SubstitutionCipher.GetStartingKey(input);

            List<NGram> bigrams = NGram.Parse(FileReader.Read("..\\..\\..\\ngrams\\english_bigrams_1.txt"));
            List<NGram> trigrams = NGram.Parse(FileReader.Read("..\\..\\..\\ngrams\\english_trigrams.txt"));
            List<NGram> quadgrams = new List<NGram>(); // NGram.Parse(FileReader.Read("..\\..\\..\\ngrams\\english_quadgrams.txt"));
            List<NGram> quintgrams = new List<NGram>();// NGram.Parse(FileReader.Read("..\\..\\..\\ngrams\\english_quintgrams.txt"));
            List<NGram> words = new List<NGram>(); // NGram.Parse(FileReader.Read("..\\..\\..\\ngrams\\english_words.txt"));

            Chromosome baseChromosome = new Chromosome(key);
            FitnessFunction fitnessFunction = new FitnessFunction(input, bigrams, trigrams, quadgrams, quintgrams, words);
            SelectionMethod selectionMethod = new SelectionMethod();
            Population population = new Population(20, baseChromosome, fitnessFunction, selectionMethod);

            ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo();
            for (int i = 0; i < 500; i++)
            {
                population.RunEpoch();
                key = ((Chromosome)population.BestChromosome).Key;
                Console.WriteLine($"[{i}]: \t{key} -> {population.FitnessMax}");

                if (population.FitnessMax < 2.5)
                {
                    break;
                }

                if (Console.KeyAvailable == true)
                {
                    consoleKeyInfo = Console.ReadKey(true);
                    if (consoleKeyInfo.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(SubstitutionCipher.Decode(input, key));
                Console.WriteLine();
                Console.Write("Enter two letters (to swap): "); // For example: A B
                string command = Console.ReadLine().ToUpper();
                key = SubstitutionCipher.SwapSymbolInKey(key, command[0], command[2]);
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("****************************************");
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
