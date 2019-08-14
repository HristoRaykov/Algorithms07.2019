using System;
using System.Collections.Generic;
using System.Linq;

namespace P02LongestIncreasSubSeq
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var seq = Console.ReadLine()
                .Split(" ".ToCharArray())
                .Select(int.Parse)
                .ToArray();

            var lis = new int[seq.Length];
            var prev = new int[seq.Length];

            var idxWithMaxLen = CalculateLis(seq, lis, prev);

            var longestSeq = ReconstructLIS(seq, prev, idxWithMaxLen);

            PrintLIS(longestSeq);
        }

        private static int CalculateLis(int[] seq, int[] lis, int[] prev)
        {
            var idxWithMaxLen = 1;

            for (int currIdx = 0; currIdx < seq.Length; currIdx++)
            {
                var currElement = seq[currIdx];
                var currLIS = 1;
                var prevIdx = -1;
                for (int i = 0; i < currIdx; i++)
                {
                    var iLIS = lis[i];
                    var iElement = seq[i];
                    if (iElement < currElement && iLIS >= currLIS)
                    {
                        currLIS = iLIS + 1;
                        prevIdx = i;
                    }
                }

                lis[currIdx] = currLIS;
                prev[currIdx] = prevIdx;

                if (lis[currIdx] > lis[idxWithMaxLen])
                {
                    idxWithMaxLen = currIdx;
                }
            }

            return idxWithMaxLen;
        }

        private static IList<int> ReconstructLIS(int[] seq, int[] prev, int idxWithMaxLen)
        {
            var longestSeq = new List<int>();
            
            var prevIdx = idxWithMaxLen;
            while (prevIdx != -1)
            {
                longestSeq.Add(seq[prevIdx]);
                prevIdx = prev[prevIdx];
            }

            longestSeq.Reverse();

            return longestSeq;
        }


        private static void PrintLIS(IList<int> longestSeq)
        {
            Console.WriteLine(string.Join(" ", longestSeq));
        }
    }
}