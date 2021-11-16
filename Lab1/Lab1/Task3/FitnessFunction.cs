using AForge.Genetic;
using System;
using System.Collections.Generic;

namespace Lab1
{
    class FitnessFunction : IFitnessFunction
    {
        public string Text { get; set; }

        public List<NGram> Bigrams { get; set; }

        public List<NGram> Trigrams { get; set; }

        public List<NGram> Quadgrams { get; set; }

        public List<NGram> Quintgrams { get; set; }

        public List<NGram> Words { get; set; }

        public FitnessFunction(string text, List<NGram> bigrams, List<NGram> trigrams, List<NGram> quadgrams, List<NGram> quintgrams, List<NGram> words)
        {
            Text = text;
            Bigrams = bigrams;
            Trigrams = trigrams;
            Quadgrams = quadgrams;
            Quintgrams = quintgrams;
            Words = words;
        }

        public double Evaluate(IChromosome chromosome)
        {
            string result = SubstitutionCipher.Decode(Text, ((Chromosome)chromosome).Key);

            double bigramDifference = 0;
            int bigramCount = 0;
            foreach (NGram bigram in Bigrams)
            {
                if (result.Contains(bigram.Value))
                {
                    NGram nGram = NGram.FromString(result, bigram.Value);
                    bigramDifference += Math.Abs(bigram.Frequencies - nGram.Frequencies);
                    bigramCount++;
                }
            }

            double trigramDifference = 0;
            int trigramsCount = 0;
            foreach (NGram trigram in Trigrams)
            {
                if (result.Contains(trigram.Value))
                {
                    NGram nGram = NGram.FromString(result, trigram.Value);
                    trigramDifference += Math.Abs(trigram.Frequencies - nGram.Frequencies);
                    trigramsCount++;
                }
            }

            return (bigramDifference / bigramCount + trigramDifference / trigramsCount) * (bigramCount + trigramsCount);
        }
    }
}
