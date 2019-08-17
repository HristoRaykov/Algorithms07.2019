using System;
using System.Collections.Generic;
using System.Linq;

namespace P02LongestZigzagSubSeq
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var seq = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var lzs = new int[seq.Length];
            var bestPrevIdx = new int[seq.Length];

            var idxWithMaxLZS = CalculateAllLZS(seq, lzs, bestPrevIdx);

            var longestSeq = ReconstructLZS(seq, bestPrevIdx, idxWithMaxLZS);

            Console.WriteLine(string.Join(" ", longestSeq.Select(x => x.ToString())));
        }

        private static int CalculateAllLZS(int[] seq, int[] lzs, int[] bestPrevIdx)
        {
            var idxWithMaxLZS = 1;
            var isPrevIncrease = new bool[seq.Length];

            lzs[0] = 1;
            bestPrevIdx[0] = -1;
            for (int currIdx = 1; currIdx < seq.Length; currIdx++)
            {
                // handle first element as two elements always form lzs
                if (seq[currIdx] == seq[0])
                {
                    lzs[currIdx] = 1;
                    bestPrevIdx[currIdx] = -1;
                }
                else
                {
                    lzs[currIdx] = 2;
                    bestPrevIdx[currIdx] = 0;
                    if (seq[currIdx] > seq[0])
                    {
                        isPrevIncrease[currIdx] = true;
                    }
                    else
                    {
                        isPrevIncrease[currIdx] = false;
                    }
                }

                for (int i = 1; i < currIdx; i++)
                {
                    bool hasChange = false;
                    if (lzs[i] + 1 > lzs[currIdx])
                    {
                        if (!isPrevIncrease[i] && seq[currIdx] > seq[i])
                        {
                            hasChange = true;
                            isPrevIncrease[currIdx] = true;
                        }
                        else if (isPrevIncrease[i] && seq[currIdx] < seq[i])
                        {
                            hasChange = true;
                            isPrevIncrease[currIdx] = false;
                        }
                    }

                    if (hasChange)
                    {
                        bestPrevIdx[currIdx] = i;
                        lzs[currIdx] = lzs[i] + 1;
                        if (lzs[currIdx] > lzs[idxWithMaxLZS])
                        {
                            idxWithMaxLZS = currIdx;
                        }
                    }
                }
            }

            return idxWithMaxLZS;
        }

        private static IList<int> ReconstructLZS(int[] seq, int[] bestPrevIdx, int idxWithMaxLzs)
        {
            var longestSeq = new List<int>();

            while (bestPrevIdx[idxWithMaxLzs] != -1)
            {
                longestSeq.Add(seq[idxWithMaxLzs]);
                idxWithMaxLzs = bestPrevIdx[idxWithMaxLzs];
            }
            
            longestSeq.Add(seq[idxWithMaxLzs]);
            longestSeq.Reverse();
            return longestSeq;
        }
    }
}