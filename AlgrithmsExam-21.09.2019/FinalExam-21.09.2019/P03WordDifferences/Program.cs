using System;
using System.Collections.Generic;
using System.Linq;

namespace P03WordDifferences
{
    class Program
    {
        private static char[] firstStr;

        private static char[] secondStr;


        static void Main(string[] args)
        {
            firstStr = Console.ReadLine().ToCharArray();
            secondStr = Console.ReadLine().ToCharArray();
            

            var firstStrStripped = StripFirstString();

            var lcs = CalculateLCS(firstStrStripped, secondStr);

            var lcsLenght = lcs[lcs.GetLength(0) - 1, lcs.GetLength(1) - 1];

            var deletionsAndInsertions = (firstStr.Length - lcsLenght) * 2;

            Console.WriteLine("Deletions and Insertions: " + deletionsAndInsertions);
        }

        private static char[] StripFirstString()
        {
            var firstStrStripped = new List<char>();
            var secondString = secondStr.ToString();
            for (int i = 0; i < firstStr.Length; i++)
            {
                var ch = firstStr[i];
                if (secondStr.Contains(ch))
                {
                    firstStrStripped.Add(ch);
                }
            }

            return firstStrStripped.ToArray();
        }

        private static int[,] CalculateLCS(char[] firstStr, char[] secondStr)
        {
            var firstStrLen = firstStr.Length;
            var secondStrLen = secondStr.Length;
            var lcs = new int[firstStrLen + 1, secondStrLen + 1];

            for (int firstStrIdx = 0; firstStrIdx < firstStrLen + 1; firstStrIdx++)
            {
                for (int secondStrIdx = 0; secondStrIdx < secondStrLen + 1; secondStrIdx++)
                {
                    if (firstStrIdx == 0 || secondStrIdx == 0)
                    {
                        lcs[firstStrIdx, secondStrIdx] = 0;
                        continue;
                    }

                    var currFirstStrChar = firstStr[firstStrIdx - 1];
                    var currSecondStrChar = secondStr[secondStrIdx - 1];

                    var firstStrPrevBest = lcs[firstStrIdx, secondStrIdx - 1];
                    var secondStrPrevBest = lcs[firstStrIdx - 1, secondStrIdx];

                    var currBest = Math.Max(firstStrPrevBest, secondStrPrevBest);
                    if (currFirstStrChar == currSecondStrChar)
                    {
                        var x = lcs[firstStrIdx - 1, secondStrIdx - 1];
                        currBest = Math.Max(1 + lcs[firstStrIdx - 1, secondStrIdx - 1], currBest);
                    }

                    lcs[firstStrIdx, secondStrIdx] = currBest;
                }
            }

            return lcs;
        }
    }
}