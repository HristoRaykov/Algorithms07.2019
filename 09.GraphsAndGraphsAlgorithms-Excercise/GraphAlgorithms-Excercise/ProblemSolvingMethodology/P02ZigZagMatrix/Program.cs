using System;
using System.Collections.Generic;
using System.Linq;

namespace P02ZigZagMatrix
{
    internal class Program
    {
        private static int[,] mtrx;

        private static int[,] sums;

        private static int[,] prevRowIdx;


        public static void Main(string[] args)
        {
            ReadInput();

            CalcMaxSumsMatrix();

            var maxSumIdx = RetrieveMaxSum();
            var maxSum = sums[maxSumIdx, sums.GetLength(1)-1];
            var maxPath = ReconstructMaxPath(maxSumIdx);
            
            Console.WriteLine($"{maxSum} = {string.Join(" + ", maxPath)}");
        }

        private static List<int> ReconstructMaxPath(int maxSumIdx)
        {
            var path = new List<int>();
            var idx = maxSumIdx;
            for (int col = sums.GetLength(1)-1; col >= 0; col--)
            {
                path.Add(mtrx[idx, col]);
                idx = prevRowIdx[idx, col];
            }

            path.Reverse();
            return path;
        }

        private static int RetrieveMaxSum()
        {
            var lastColIdx = sums.GetLength(1)-1;
            var maxSumIdx = -1;
            var maxSum = 0;
            for (int row = 0; row < sums.GetLength(0); row++)
            {
                if (sums[row, lastColIdx] > maxSum)
                {
                    maxSum = sums[row, lastColIdx];
                    maxSumIdx = row;
                }
            }

            return maxSumIdx;
        }

        private static void CalcMaxSumsMatrix()
        {
            for (int col = 0; col < mtrx.GetLength(1); col++)
            {
                for (int row = 0; row < mtrx.GetLength(0); row++)
                {
                    if (col == 0)
                    {
                        sums[row, col] = mtrx[row, col];
                        continue;
                    }

                    var bestPrevIdx = -1;
                    var bestPrevSum = 0;

                    if (col % 2 != 0)
                    {
                        for (int i = row + 1; i < mtrx.GetLength(0); i++)
                        {
                            var sum = sums[i, col - 1];
                            if (sums[i, col - 1] > bestPrevSum)
                            {
                                bestPrevSum = sums[i, col - 1];
                                bestPrevIdx = i;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < row; i++)
                        {
                            var sum = sums[i, col - 1];
                            if (sums[i, col - 1] > bestPrevSum)
                            {
                                bestPrevSum = sums[i, col - 1];
                                bestPrevIdx = i;
                            }
                        }
                    }
                    
                    prevRowIdx[row, col] = bestPrevIdx;
                    sums[row, col] = bestPrevSum + mtrx[row, col];
                }
            }
        }

        private static void ReadInput()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            mtrx = new int[rows, cols];
            sums = new int[rows, cols];
            prevRowIdx = new int[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                var lineParams = Console.ReadLine()
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
                for (int col = 0; col < cols; col++)
                {
                    mtrx[row, col] = lineParams[col];
                    prevRowIdx[row, col] = -1;
                }
            }
        }
    }
}