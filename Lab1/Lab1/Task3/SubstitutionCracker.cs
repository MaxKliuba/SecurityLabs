using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    class SubstitutionCracker
    {
        public static Dictionary<char, float> GetEnglishLetters()
        {
            Dictionary<char, float> englishLetters = new Dictionary<char, float>();

            englishLetters.Add('A', 8.17f);
            englishLetters.Add('B', 1.49f);
            englishLetters.Add('C', 2.78f);
            englishLetters.Add('D', 4.25f);
            englishLetters.Add('E', 8.17f);
            englishLetters.Add('F', 8.17f);
            englishLetters.Add('G', 8.17f);
            englishLetters.Add('H', 8.17f);
            englishLetters.Add('A', 8.17f);
            englishLetters.Add('I', 8.17f);
            englishLetters.Add('J', 8.17f);
            englishLetters.Add('K', 8.17f);

            return englishLetters;
        }
    }
}
