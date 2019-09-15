using System;
using System.Collections.Generic;
using System.Linq;

namespace LabIIP01StarsInTheCube
{
    internal class Program
    {
        private static char[,,] cube;

        private static Dictionary<char, int> stars;

        public static void Main(string[] args)
        {
            ReadInput();

            if (cube.GetLength(0) < 3)
            {
                Console.WriteLine(0);
                return;
            }

            for (int height = 1; height < cube.GetLength(0) - 1; height++)
            {
                for (int row = 1; row < cube.GetLength(1) - 1; row++)
                {
                    for (int col = 1; col < cube.GetLength(2) - 1; col++)
                    {
                        var currChar = cube[height, row, col];
                        if (cube[height, row, col + 1] == currChar && cube[height, row, col - 1] == currChar &&
                            cube[height, row + 1, col] == currChar && cube[height, row - 1, col] == currChar &&
                            cube[height + 1, row, col] == currChar && cube[height - 1, row, col] == currChar)
                        {
                            if (!stars.ContainsKey(currChar))
                            {
                                stars[currChar] = 0;
                            }

                            stars[currChar]++;
                        }
                    }
                }
            }


            if (stars.Count == 0)
            {
                Console.WriteLine(0);
            }
            else
            {
                var totalCount = stars.Sum(kvp => kvp.Value);
                Console.WriteLine(totalCount);
                foreach (var star in stars.Keys.OrderBy(x=>x))
                {
                    Console.WriteLine($"{star} -> {stars[star]}");
                }
            }
        }

        private static void ReadInput()
        {
            var n = int.Parse(Console.ReadLine());

            cube = new char[n, n, n];
            stars = new Dictionary<char, int>();

            for (int row = 0; row < n; row++)
            {
                var line = Console.ReadLine().Split('|').ToArray();
                for (int height = 0; height < n; height++)
                {
                    var inputRow = line[height].Trim().Split(' ').Select(x => char.Parse(x)).ToArray();
                    for (int col = 0; col < n; col++)
                    {
                        cube[height, row, col] = inputRow[col];
                    }
                }
            }


            Console.WriteLine();
        }
    }
}