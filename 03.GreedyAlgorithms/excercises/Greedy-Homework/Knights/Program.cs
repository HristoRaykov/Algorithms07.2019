using System;
using System.Collections.Generic;
using System.Linq;
 
namespace Knights
{
    public class Program
    {
        private static int turns = 1;
        private static int[][] board;
 
        private static Point currentPoint = new Point(0, 0);
        private static List<Point> candidates = new List<Point>();
 
        public static void Main()
        {
            ReadInput();
            MakeTour();
            PrintBoard();
        }
 
        private static void ReadInput()
        {
            int n = int.Parse(Console.ReadLine());
            board = new int[n][];
 
            for (int i = 0; i < n; i++)
            {
                board[i] = new int[n];
            }
        }
 
        private static void MakeTour()
        {
            candidates = FindPotentialMoves(currentPoint);
            board[currentPoint.Row][currentPoint.Col] = turns++;
 
            while (candidates.Count > 0)
            {
                int minNextMoves = int.MaxValue;
                Point bestCandidate = null;
 
                foreach (var candidate in candidates)
                {
                    int candidateMoves = FindPotentialMoves(candidate).Count;
                    if (candidateMoves < minNextMoves)
                    {
                        minNextMoves = candidateMoves;
                        bestCandidate = candidate;
                    }
                }
 
                currentPoint = bestCandidate;
                board[currentPoint.Row][currentPoint.Col] = turns++;
                candidates = FindPotentialMoves(currentPoint);
            }
        }
 
        private static void PrintBoard()
        {
            foreach (var row in board)
            {
                Console.WriteLine(string.Join(" ", row.Select(r => r.ToString().PadLeft(3))));
            }
        }
 
        private static List<Point> FindPotentialMoves(Point point)
        {
            List<Point> result = new List<Point>
            {
                new Point(point.Row + 1, point.Col + 2),
                new Point(point.Row - 1, point.Col + 2),
                new Point(point.Row + 1, point.Col - 2),
                new Point(point.Row - 1, point.Col - 2),
                new Point(point.Row + 2, point.Col + 1),
                new Point(point.Row + 2, point.Col - 1),
                new Point(point.Row - 2, point.Col + 1),
                new Point(point.Row - 2, point.Col - 1)
            };
 
            return result.Where(p => IsInBoard(p.Row, p.Col) && board[p.Row][p.Col] == 0).ToList();
        }
 
        private static bool IsInBoard(int row, int col)
        {
            return 0 <= row && row < board.Length && 0 <= col && col < board[row].Length;
        }
    }
 
    public class Point
    {
        public Point(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
 
        public int Row { get; set; }
 
        public int Col { get; set; }
    }
}