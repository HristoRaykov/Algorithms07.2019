using System;
using System.Collections.Generic;

namespace P02MinEditDistance
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var replaceCost =
                int.Parse(Console.ReadLine().Split(" = ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1]);
            var insertCost =
                int.Parse(Console.ReadLine().Split(" = ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1]);
            var deleteCost =
                int.Parse(Console.ReadLine().Split(" = ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1]);
            var s1 = Console.ReadLine().Split('=')[1];
            var s2 = Console.ReadLine().Split('=')[1];
            
            var costs = CalculateCosts(s1.ToCharArray(), s2.ToCharArray(), replaceCost, insertCost, deleteCost);

//            for (int i = 0; i < costs.GetLength(0); i++)
//            {
//                for (int j = 0; j < costs.GetLength(1); j++)
//                {
//                    Console.Write(costs[i, j] + " ");
//                }
//
//                Console.WriteLine();
//            }
            
            var minDist = costs[s1.Length, s2.Length];

            var operations = ReconstructMinDistance(costs, s1.ToCharArray(), s2.ToCharArray(), replaceCost, insertCost,
                deleteCost);

            Console.WriteLine("Minimum edit distance: " + minDist);
            foreach (var operation in operations)
            {
                Console.WriteLine(operation);
            }
        }

        private static int[,] CalculateCosts(char[] s1, char[] s2, int replaceCost, int insertCost, int deleteCost)
        {
            var costs = new int[s1.Length + 1, s2.Length + 1];

            for (int row = 0; row < costs.GetLength(0); row++)
            {
                for (int col = 0; col < costs.GetLength(1); col++)
                {
                    if (row == 0)
                    {
                        costs[row, col] = col * insertCost;
                        continue;
                    }

                    if (col == 0)
                    {
                        costs[row, col] = row * deleteCost;
                        continue;
                    }

                    var s1Char = s1[row - 1];
                    var s2Char = s2[col - 1];

                    if (s1Char == s2Char)
                    {
                        costs[row, col] = costs[row - 1, col - 1];
                    }
                    else
                    {
                        var replace = costs[row - 1, col - 1] + replaceCost;
                        var insert = costs[row, col - 1] + insertCost;
                        var delete = costs[row - 1, col] + deleteCost;
                        costs[row, col] = Math.Min(replace, Math.Min(insert, delete));
                    }
                }
            }


            return costs;
        }


        private static IList<string> ReconstructMinDistance(int[,] costs, char[] s1, char[] s2, int replaceCost,
            int insertCost, int deleteCost)
        {
            var operations = new List<string>();
            var row = costs.GetLength(0) - 1;
            var col = costs.GetLength(1) - 1;

            var totalCost = costs[row, col];

            while (totalCost > 0)
            {
                if (row == 0)
                {
                    operations.Add($"INSERT(0,{s2[col - 1]})");
                    break;
                }

                if (col == 0)
                {
                    operations.Add($"DELETE(0)");
                    break;
                }

                var s1Char = s1[row - 1];
                var s2Char = s2[col - 1];
                if (s1Char == s2Char)
                {
                    row--;
                    col--;
                    continue;
                }

                var replace = costs[row - 1, col - 1] + replaceCost;
                var insert = costs[row, col - 1] + insertCost;
                var delete = costs[row - 1, col] + deleteCost;

                if (totalCost == replace)
                {
                    row--;
                    col--;
                    operations.Add($"REPLACE({row-1},{s2Char})");
                    totalCost -= replaceCost;
                }
                else if (totalCost == insert)
                {
                    col--;
                    operations.Add($"INSERT({row-1},{s2Char})");
                    totalCost -= insertCost;
                }
                else
                {
                    row--;
                    operations.Add($"DELETE({row-1})");
                    totalCost -= deleteCost;
                }
            }

            operations.Reverse();
            return operations;
        }
    }
}