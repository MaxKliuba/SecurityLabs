using System;
using System.Collections.Generic;

namespace Lab4
{
    class PasswordGenerator
    {
        private static readonly string NUMBERS = "1234567890";
        private static readonly string[] KEYBOARD = {
            "!@#$%&*()",
            "qwertyuiop",
            "asdfghjkl",
            "zxcvbnm",
            "abcdefghijklmnopqrstuvwxyz",
        };
        private static readonly List<string> NAMES = DocumentReader.ReadList("..\\..\\..\\res\\names.txt", false);
        private static readonly List<string> NOUNS = DocumentReader.ReadList("..\\..\\..\\res\\nouns.txt", false);

        private static readonly Random RANDOM = new Random();

        public static string GenerateLikeHuman()
        {
            string password;

            int v = RANDOM.Next(0, 100);
            if (v >= 0 && v < 35) password = Numbers(RANDOM.Next(4, 10));
            else if (v >= 35 && v < 50) password = DateOfBirth();
            else if (v >= 50 && v < 70) password = SequentialLettersAndNumbers();
            else if (v >= 70 && v < 90) password = NameAndNumbers();
            else if (v >= 90 && v < 99) password = NounsAndNumbers();
            else if (v >= 99 && v < 100) password = Random();
            else password = "password";

            int m1 = RANDOM.Next(0, 100);
            if (m1 >= 0 && m1 < 1) password = AddSpecialSharacter(password);
            int m2 = RANDOM.Next(0, 100);
            if (m2 >= 0 && m2 < 2) password = ReplaceCharacters(password);

            return password;
        }

        public static string Numbers(int count = 4)
        {
            return (RANDOM.Next(0, 2) == 1 ? RepeatingNumber(count) : SequentialNumbers(count));
        }

        public static string RepeatingNumber(int count = 4)
        {
            return new string((char)(RANDOM.Next(0, 10) + '0'), count);
        }

        public static string SequentialNumbers(int count = 4)
        {
            char[] password = NUMBERS.Substring(0, count).ToCharArray();
            if (RANDOM.Next(0, 2) == 1)
            {
                Array.Reverse(password);
            }

            return new string(password);
        }

        public static string DateOfBirth()
        {
            return DigitToString(RANDOM.Next(1, 32)) + DigitToString(RANDOM.Next(1, 13)) + DigitToString(RANDOM.Next(1940, 2021));
        }

        public static string SequentialLettersAndNumbers()
        {
            string str = KEYBOARD[RANDOM.Next(1, KEYBOARD.Length)];
            char[] word = str.Substring(0, RANDOM.Next(4, 8)).ToCharArray();
            if (RANDOM.Next(0, 2) == 1)
            {
                Array.Reverse(word);
            }

            return ChangeLetterCase(new string(word) + Numbers(RANDOM.Next(0, Math.Abs(12 - word.Length) % 10)));
        }

        public static string NameAndNumbers()
        {
            string name = NAMES[RANDOM.Next(0, NAMES.Count)];

            return ChangeLetterCase(name + Numbers(RANDOM.Next(0, Math.Abs(12 - name.Length) % 10)));
        }

        public static string NounsAndNumbers()
        {
            string noun = NOUNS[RANDOM.Next(0, NOUNS.Count)];

            return ChangeLetterCase(noun + Numbers(RANDOM.Next(0, Math.Abs(12 - noun.Length) % 10)));
        }

        public static string Random()
        {
            string password = "";

            int length = RANDOM.Next(4, 20);
            string buf = NUMBERS + KEYBOARD[0] + KEYBOARD[4];

            for (int i = 0; i < length; i++)
            {
                password += buf[RANDOM.Next(0, buf.Length)];
            }

            return ChangeLetterCase(password);
        }

        private static string ChangeLetterCase(string input)
        {
            string output = "";

            int v = RANDOM.Next(0, 100);

            if (v >= 0 && v < 40) output = input.ToLower();
            else if (v >= 40 && v < 50) output = input.ToUpper();
            else if (v >= 50 && v < 90)
            {
                output += input[0].ToString().ToUpper();
                output += input.Substring(1);
            }
            else if (v >= 90 && v < 100)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    output += RANDOM.Next(0, 2) == 0 ? input[i].ToString().ToLower() : input[i].ToString().ToUpper();
                }
            }
            else output = input;

            return output;
        }

        private static string AddSpecialSharacter(string password)
        {
            return password + KEYBOARD[0][RANDOM.Next(0, KEYBOARD[0].Length)];
        }

        private static string ReplaceCharacters(string password)
        {
            int v = RANDOM.Next(0, 100);

            if (v >= 0 && v < 40) return password.Replace('i', '1').Replace('I', '1');
            else if (v >= 40 && v < 80) return password.Replace('o', '0').Replace('O', '0');
            else if (v >= 80 && v < 100) return password.Replace('B', '8');
            else return password;
        }


        private static string DigitToString(int digit)
        {
            string str = digit + "";

            return (str.Length == 1 ? "0" + str : str);
        }
    }
}
