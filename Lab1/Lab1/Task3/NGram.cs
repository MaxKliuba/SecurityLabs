using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Lab1
{
    class NGram : IComparable<NGram>
    {
        public string Value { get; set; }

        public double Frequencies { get; set; }

        public NGram(string value, double frequencies)
        {
            Value = value;
            Frequencies = frequencies;
        }

        public static List<NGram> Parse(List<string> inputs)
        {
            long sum = 0;
            List<NGram> nGrams = new List<NGram>();
            for (int i = 0; i < inputs.Count; i++)
            {
                string[] line = inputs[i].Split(" ");
                int number = Convert.ToInt32(line[1]);
                nGrams.Add(new NGram(line[0], number));
                sum += number;
            }

            for (int i = 0; i < nGrams.Count; i++)
            {
                nGrams[i].Frequencies /= sum;
            }

            nGrams.Sort();
            nGrams.Reverse();

            return nGrams;
        }

        public static List<NGram> CreateList(string input)
        {
            List<NGram> nGrams = new List<NGram>();

            for (int i = 0; i < input.Length; i++)
            {
                int frequencies = 0;
                if (!nGrams.Exists(e => e.Value.Equals(input[i].ToString())))
                {
                    for (int j = i; j < input.Length; j++)
                    {
                        if (input[i].Equals(input[j]))
                        {
                            frequencies++;
                        }
                    }

                    nGrams.Add(new NGram(input[i].ToString(), (double)frequencies / input.Length));
                }
            }

            nGrams.Sort();
            nGrams.Reverse();

            return nGrams;
        }

        public static NGram FromString(string input, string value)
        {
            int number = 0;

            for (int i = 0; i < input.Length - value.Length; i++)
            {
                if (input.Substring(i, value.Length).Contains(value))
                {
                    number++;
                }
            }

            return new NGram(value, (double)number / input.Length);
        }

        public int CompareTo([AllowNull] NGram other)
        {
            return Frequencies.CompareTo(other.Frequencies);
        }

        public override string ToString()
        {
            return $"{Value} -> {Frequencies}";
        }
    }
}
