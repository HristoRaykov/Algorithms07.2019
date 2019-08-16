using System;
using System.Collections.Generic;
using System.Data.Common;

namespace P02LCS
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var firstStr = Console.ReadLine();
            var secondStr = Console.ReadLine();

            var lcs = CalculateLCS(firstStr, secondStr);

            var commonChars = ReconstructLCS(lcs, firstStr, secondStr);

            Console.WriteLine(commonChars.Count);
        }

        private static int[,] CalculateLCS(string firstStr, string secondStr)
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

                    var currFirstStrChar = firstStr.ToCharArray()[firstStrIdx - 1];
                    var currSecondStrChar = secondStr.ToCharArray()[secondStrIdx - 1];

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

        private static IList<char> ReconstructLCS(int[,] lcs, string firstStr, string secondStr)
        {
            var seq = new List<char>();

            var row = lcs.GetLength(0)-1;
            var col = lcs.GetLength(1)-1;

            char currFirstStrChar;
            char currSecondStrChar;
            int currBest;

            while (row > 0 && col > 0)
            {
                currFirstStrChar = firstStr.ToCharArray()[row-1];
                currSecondStrChar = secondStr.ToCharArray()[col-1];

                currBest = lcs[row, col];

                if (currFirstStrChar == currSecondStrChar && currBest == lcs[row - 1, col - 1] + 1)
                {
                    seq.Add(currFirstStrChar);
                    row--;
                    col--;
                }
                else
                {
                    var firstStrPrevBest = lcs[row, col - 1];
                    var secondStrPrevBest = lcs[row - 1, col];

                    if (currBest != secondStrPrevBest && currBest == firstStrPrevBest)
                    {
                        col--;
                    }
                    else
                    {
                        row--;
                    }
                }
            }

            seq.Reverse();
            return seq;
        }
    }
}