using System;
using System.Collections.Generic;
using System.Linq;

namespace P02AreasInMatrix
{
    internal class Program
    {
        private static char[,] matrix;

        private static bool[,] visited;

        private static Dictionary<char, int> connArreas;

        public static void Main(string[] args)
        {
            ReadInput();
            connArreas = new Dictionary<char, int>();
            visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];

            CalculateConnectedAreas();

            PrintConnectedArreas();
        }


        private static void CalculateConnectedAreas()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (visited[row, col] == false)
                    {
                        var ch = matrix[row, col];
                        if (!connArreas.ContainsKey(ch))
                        {
                            connArreas[ch] = 0;
                        }

                        connArreas[ch]++;

                        DFS(row, col);
                    }
                }
            }
        }

        private static void DFS(int row, int col)
        {
            if (visited[row, col])
            {
                return;
            }

            visited[row, col] = true;
            var currChar = matrix[row, col];
            if (IsInsiteMatrix(row, col - 1) && currChar == matrix[row, col - 1])
            {
                DFS(row, col - 1);
            }

            if (IsInsiteMatrix(row, col + 1) && currChar == matrix[row, col + 1])
            {
                DFS(row, col + 1);
            }

            if (IsInsiteMatrix(row - 1, col) && currChar == matrix[row - 1, col])
            {
                DFS(row - 1, col);
            }

            if (IsInsiteMatrix(row + 1, col) && currChar == matrix[row + 1, col])
            {
                DFS(row + 1, col);
            }
        }

        private static bool IsInsiteMatrix(int row, int col)
        {
            return row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1);
        }

        private static void PrintConnectedArreas()
        {
            var totalArreasCount = connArreas.Select(kvp => kvp.Value).Sum();
            Console.WriteLine("Areas: " + totalArreasCount);
            foreach (var ch in connArreas.Keys.OrderBy(ch=>ch))
            {
                Console.WriteLine($"Letter '{ch}' -> {connArreas[ch]}");
            }
        }

        private static void ReadInput()
        {
            var rows = int.Parse(Console.ReadLine());
            for (int i = 0; i < rows; i++)
            {
                var line = Console.ReadLine();
                if (matrix == null)
                {
                    matrix = new char[rows, line.Length];
                }

                for (int j = 0; j < line.Length; j++)
                {
                    matrix[i, j] = line.ToCharArray()[j];
                }
            }
        }
    }
}