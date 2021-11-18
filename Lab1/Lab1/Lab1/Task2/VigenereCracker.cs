using System;
using System.Text;

namespace Lab1
{
    class VigenereCracker
    {
        public static void AnalyzeKeyLength(string input)
        {
            int[] indexOfCoincidence = new int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {

                for (int j = 0; j < input.Length; j++)
                {
                    if (i != 0 && input[j] == input[(j + i) % input.Length])
                    {
                        indexOfCoincidence[i]++;
                    }
                }
            }

            DrawGraph(indexOfCoincidence);
        }

        public static void DrawGraph(int[] array)
        {
            int minVal = array[0];
            int maxVal = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                minVal = Math.Min(minVal, array[i]);
                maxVal = Math.Max(maxVal, array[i]);
            }

            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"[{i}]\t| {array[i]}\t| {new string('█', Map(array[i], minVal, maxVal, 1, 30))}");
            }
        }

        private static int Map(int x, int inMin, int inMax, int outMin, int outMax)
        {
            return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        public static string DecodeXor(string input, string key)
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                output.Append((char)(input[i] ^ key[i % key.Length]));
            }

            return output.ToString();
        }

        public static Result DecodeXorBruteforce(string input, int keyLength)
        {
            string key = "";
            string[] blocksIn = DivideIntoBlocks(input, keyLength);
            string[] blockOut = new string[blocksIn.Length];

            for (int i = 0; i < blocksIn.Length; i++)
            {
                Result result = CaesarCracker.DecodeXorBruteforce(blocksIn[i]);
                blockOut[i] = result.Value;
                key += result.Key;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(result);
                Console.ResetColor();
            }

            return new Result(key, VigenereCracker.MargeBlocks(blockOut));
        }

        public static string[] DivideIntoBlocks(string input, int keyLength)
        {
            string[] blocks = new string[keyLength];

            for (int i = 0; i < input.Length; i += keyLength)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    if (blocks[j] == null)
                    {
                        blocks[j] = "";
                    }

                    blocks[j] += input[(i + j) % input.Length];
                }
            }

            return blocks;
        }

        public static string MargeBlocks(string[] blocks)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < blocks[0].Length; i++)
            {
                for (int j = 0; j < blocks.Length; j++)
                {
                    str.Append(blocks[j][i % blocks[j].Length]);
                }
            }

            return str.ToString();
        }
    }
}
