using AForge.Genetic;
using System;

namespace Lab1
{
    class Chromosome : IChromosome
    {
        private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string Key { get; set; }

        private double fitness = 0;

        public Chromosome(string key)
        {
            Key = key;
        }

        public double Fitness => fitness;

        public IChromosome Clone()
        {
            return new Chromosome(Key);
        }

        public int CompareTo(object obj)
        {
            return Fitness.CompareTo(((Chromosome)obj).Fitness);
        }

        public IChromosome CreateNew()
        {
            return new Chromosome(CreateNewKey());
        }

        public void Crossover(IChromosome pair)
        {
            Random rnd = new Random();
            int crossoverCount = rnd.Next(1, 13);

            for (int i = 0; i < crossoverCount; i++)
            {
                int index = rnd.Next(0, Key.Length);
                string key1 = Swap(Key, index, Key.IndexOf(((Chromosome)pair).Key[index]));
                string key2 = Swap(((Chromosome)pair).Key, index, ((Chromosome)pair).Key.IndexOf(Key[index]));

                Key = key1;
                ((Chromosome)pair).Key = key2;
            }
        }

        public void Evaluate(IFitnessFunction function)
        {
            fitness = function.Evaluate(this);
        }

        public void Generate()
        {
            Key = CreateNewKey();
        }

        public void Mutate()
        {
            Random rnd = new Random();
            int mutateCount = rnd.Next(1, 6);

            for (int i = 0; i < mutateCount; i++)
            {
                Key = Swap(Key, rnd.Next(0, Key.Length), rnd.Next(0, Key.Length));
            }
        }

        public static string CreateNewKey()
        {
            Random rnd = new Random();

            string key = "";
            for (int i = 0; i < ALPHABET.Length; i++)
            {
                char ch;
                do
                {
                    ch = ALPHABET[rnd.Next(0, ALPHABET.Length)];
                } while (key.Contains(ch));

                key += ch.ToString();
            }

            return key;
        }

        public static Chromosome NewInstance()
        {
            return new Chromosome(CreateNewKey());
        }

        public override string ToString()
        {
            return $"{Key} -> {Fitness}";
        }

        private string Swap(string str, int index1, int index2)
        {
            char firstChar = str[index1];
            char secondChar = str[index2];

            string newStr = str.Replace(firstChar, '_');
            newStr = newStr.Replace(secondChar, firstChar);
            newStr = newStr.Replace('_', secondChar);

            return newStr;
        }
    }
}
