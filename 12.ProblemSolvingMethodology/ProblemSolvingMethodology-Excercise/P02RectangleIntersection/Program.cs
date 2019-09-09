using System;
using System.Collections.Generic;
using System.Linq;

namespace P02RectangleIntersection
{
    internal class Program
    {
        private static List<Rectangle> rects;

        private static List<int> xCords;

        private static List<int> yCords;

        private static int[,] matrix;

        public static void Main(string[] args)
        {
            ReadInput();

            foreach (var rectangle in rects)
            {
                var minYIdx = yCords.IndexOf(rectangle.minY);
                var maxYIdx = yCords.IndexOf(rectangle.maxY);
                var minXIdx = xCords.IndexOf(rectangle.minX);
                var maxXIdx = xCords.IndexOf(rectangle.maxX);

                for (int row = minYIdx; row < maxYIdx; row++)
                {
                    for (int col = minXIdx; col < maxXIdx; col++)
                    {
                        matrix[row, col]++;
                    }
                }
            }

            var totalArea = 0;
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] > 1)
                    {
                        var minX = xCords[col];
                        var maxX = xCords[col + 1];
                        var minY = yCords[row];
                        var maxY = yCords[row + 1];
                        totalArea += (maxX - minX) * (maxY - minY);
                    }
                }
            }


            Console.WriteLine(totalArea);
        }

        private static void ReadInput()
        {
            var rectCount = int.Parse(Console.ReadLine());

            rects = new List<Rectangle>();
            xCords = new List<int>();
            yCords = new List<int>();


            for (int i = 0; i < rectCount; i++)
            {
                var coords = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();
                var rectangle = new Rectangle(coords[0], coords[1], coords[2], coords[3]);
                rects.Add(rectangle);
                xCords.Add(coords[0]);
                xCords.Add(coords[1]);
                yCords.Add(coords[2]);
                yCords.Add(coords[3]);
            }

            xCords = xCords.Distinct().OrderBy(x => x).ToList();
            yCords = yCords.Distinct().OrderBy(y => y).ToList();

            matrix = new int[yCords.Count - 1, xCords.Count - 1];
        }
    }

    internal class Rectangle
    {
        public int minX { get; }

        public int maxX { get; }

        public int minY { get; }

        public int maxY { get; }

        public Rectangle(int minX, int maxX, int minY, int maxY)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
        }
    }
}