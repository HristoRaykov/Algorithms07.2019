using System;
using System.Collections.Generic;
using System.Text;

namespace P03MoveDownRightSum
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var matrix = ReadInput();

            var maxSumMtrx = CalculateMaxSumMatrix(matrix);

            var maxSumPath = ReconstructMaxSumPath(maxSumMtrx);

            PrintPath(maxSumPath);
        }

        private static IList<int[]> ReconstructMaxSumPath(int[,] maxSumMtrx)
        {
            var path = new List<int[]>();

            var row = maxSumMtrx.GetLength(0) - 1;
            var col = maxSumMtrx.GetLength(1) - 1;
            while (row != 0 || col != 0)
            {
                path.Add(new int[2] {row, col});
                if (row == 0)
                {
                    col--;
                }
                else if (col == 0)
                {
                    row--;
                }
                else
                {
                    var leftCell = maxSumMtrx[row, col - 1];
                    var topCell = maxSumMtrx[row - 1, col];
                    if (leftCell >= topCell)
                    {
                        col--;
                    }
                    else
                    {
                        row--;
                    }
                }
            }

            path.Add(new int[2] {row, col});
            path.Reverse();

            return path;
        }

        private static int[,] CalculateMaxSumMatrix(int[,] matrix)
        {
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(0);
            var maxSumMtrx = new int[n, m];

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < m; col++)
                {
                    if (row == 0 && col == 0)
                    {
                        maxSumMtrx[row, col] = matrix[row, col];
                    }
                    else if (row == 0 && col != 0)
                    {
                        maxSumMtrx[row, col] = maxSumMtrx[row, col - 1] + matrix[row, col];
                    }
                    else if (row != 0 && col == 0)
                    {
                        maxSumMtrx[row, col] = maxSumMtrx[row - 1, col] + matrix[row, col];
                    }
                    else
                    {
                        maxSumMtrx[row, col] = maxSumMtrx[row, col - 1] >= maxSumMtrx[row - 1, col]
                            ? maxSumMtrx[row, col - 1] + matrix[row, col]
                            : maxSumMtrx[row - 1, col] + matrix[row, col];
                    }
                }
            }


            return maxSumMtrx;
        }

        private static void PrintPath(IList<int[]> maxSumPath)
        {
            StringBuilder path = new StringBuilder();

            for (int i = 0; i < maxSumPath.Count; i++)
            {
                path.Append($"[{maxSumPath[i][0]}, {maxSumPath[i][1]}]");
                if (i != maxSumPath.Count - 1)
                {
                    path.Append(" ");
                }
            }

            Console.WriteLine(path);
        }

        private static int[,] ReadInput()
        {
            var n = int.Parse(Console.ReadLine());
            var m = int.Parse(Console.ReadLine());
            var matrix = new int[n, m];

            for (int row = 0; row < n; row++)
            {
                var input = Console.ReadLine().Split(' ');
                for (int col = 0; col < m; col++)
                {
                    matrix[row, col] = int.Parse(input[col]);
                }
            }

            return matrix;
        }
    }
}