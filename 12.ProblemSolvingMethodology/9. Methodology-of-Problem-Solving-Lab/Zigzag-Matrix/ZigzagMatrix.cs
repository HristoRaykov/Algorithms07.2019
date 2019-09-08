namespace Zigzag_Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ZigzagMatrix
    {
        public static void Main(string[] args)
        {
            int numberOfRows = int.Parse(Console.ReadLine());
            int numberOfColumns = int.Parse(Console.ReadLine());

            int[][] matrix = new int[numberOfRows][];
            ReadMatrix(numberOfRows, matrix);

            // TODO
        }

        private static void ReadMatrix(int numberOfRows, int[][] matrix)
        {
            for (int i = 0; i < numberOfRows; i++)
            {
                matrix[i] = Console.ReadLine()
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();
            }
        }

        private static int GetLastRowIndexOfPath(int[,] maxPaths, int numberOfColumns)
        {
            // TODO
            throw new NotImplementedException();
        }

        private static List<int> RecoverMaxPath(
            int numberOfColumns, 
            int[][] matrix, 
            int rowIndex, 
            int[,] previousRowIndex)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}