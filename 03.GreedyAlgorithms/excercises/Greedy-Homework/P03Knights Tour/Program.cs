using System;
using System.Collections.Generic;
using System.Linq;

namespace P03Knights_Tour
{
    internal class Program
    {
        private static int[,] board;

        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            board = new int[n, n];

            RunKnightsTour(new BoardCell(0, 0));

            PrintBoard();
        }

        private static void RunKnightsTour(BoardCell currCell)
        {
            var usedCells = new HashSet<BoardCell>(BoardCell.RowColComparer);

            var nextCell = currCell;
            var idx = 1;
            while (true)
            {
                board[nextCell.Row, nextCell.Col] = idx;
                idx++;
                usedCells.Add(nextCell);

                var possibleMoves = GetAllPossibleMoves(nextCell, usedCells);

                nextCell = GetNextCell(possibleMoves, usedCells);

                if (nextCell == null)
                {
                    
                    return;
                }
            }
        }

        private static BoardCell GetNextCell(ISet<BoardCell> possibleMoves, HashSet<BoardCell> usedCells)
        {
            BoardCell nextCell = null;
            var minSize = int.MaxValue;
            foreach (var cell in possibleMoves)
            {
                var moves = GetAllPossibleMoves(cell, usedCells);
                if (moves.Count > 0 && moves.Count < minSize)
                {
                    minSize = moves.Count;
                    nextCell = cell;
                }
            }

            if (nextCell==null && possibleMoves.Count==1)
            {
                return new List<BoardCell>(possibleMoves)[0];
            }

            return nextCell;
        }

        private static ISet<BoardCell> GetAllPossibleMoves(BoardCell currCell, HashSet<BoardCell> usedCells)
        {
            var allMoves = GetAllMoves(currCell);

            var possibleMoves = new HashSet<BoardCell>(BoardCell.RowColComparer);
            possibleMoves.UnionWith(allMoves
                .Where(c => IsValidBoardCell(c))
                .Where(c => !usedCells.Contains(c)));

            return possibleMoves;
        }

        private static ISet<BoardCell> GetAllMoves(BoardCell currCell)
        {
            var allMoves = new HashSet<BoardCell>(BoardCell.RowColComparer);

            var currRow = currCell.Row;
            var currCol = currCell.Col;

            allMoves.Add(new BoardCell(currRow + 1, currCol + 2));
            allMoves.Add(new BoardCell(currRow - 1, currCol + 2));
            allMoves.Add(new BoardCell(currRow + 1, currCol - 2));
            allMoves.Add(new BoardCell(currRow - 1, currCol - 2));
            allMoves.Add(new BoardCell(currRow + 2, currCol + 1));
            allMoves.Add(new BoardCell(currRow + 2, currCol - 1));
            allMoves.Add(new BoardCell(currRow - 2, currCol + 1));
            allMoves.Add(new BoardCell(currRow - 2, currCol - 1));

            return allMoves;
        }

        private static bool IsValidBoardCell(BoardCell cell)
        {
            return cell.Row >= 0 && cell.Row < board.GetLength(0) && cell.Col >= 0 && cell.Col < board.GetLength(1);
        }
        
        private static void PrintBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(" " + board[i,j].ToString().PadLeft(3));
                }

                Console.WriteLine();
            }
        }
    }

    class BoardCell
    {
        private sealed class RowColEqualityComparer : IEqualityComparer<BoardCell>
        {
            public bool Equals(BoardCell x, BoardCell y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Row == y.Row && x.Col == y.Col;
            }

            public int GetHashCode(BoardCell obj)
            {
                unchecked
                {
                    return (obj.Row * 397) ^ obj.Col;
                }
            }
        }

        public static IEqualityComparer<BoardCell> RowColComparer { get; } = new RowColEqualityComparer();

        public int Row { get; }

        public int Col { get; }

        public BoardCell(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}