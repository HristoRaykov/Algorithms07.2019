using System;
using System.Collections.Generic;

namespace P04_Snakes
{
    class Snakes
    {
        private static IList<Direction[]> snakes = new List<Direction[]>();

        private static ISet<string> visitedBlocks = new HashSet<string>();

        private static ISet<string> generatedSnakes = new HashSet<string>();

        private static int numOfBlocks;

        static void Main(string[] args)
        {
            numOfBlocks = int.Parse(Console.ReadLine());
            
            GenerateSnakes(new Direction[numOfBlocks], 0, 0, 0, Direction.S);

            PrintSnakes(snakes);
        }

        private static void GenerateSnakes(Direction[] snake, int idx, int row, int col, Direction direction)
        {
            if (idx == numOfBlocks)
            {
                var snakeStr = GetSnakeString(snake);
                if (!generatedSnakes.Contains(snakeStr))
                {
                    snakes.Add(snake);
                    ISet<string> identicalSnakes = GetAllIdenticalSnakes(snake);
                    generatedSnakes.UnionWith(identicalSnakes);
                }

                return;
            }

            var blockPosition = $"{row} {col}";
            if (!visitedBlocks.Contains(blockPosition))
            {
                visitedBlocks.Add(blockPosition);
                snake[idx] = direction;

                GenerateSnakes(DeepArrayCopy(snake), idx + 1, row, col + 1, Direction.R);
                GenerateSnakes(DeepArrayCopy(snake), idx + 1, row - 1, col, Direction.D);
                GenerateSnakes(DeepArrayCopy(snake), idx + 1, row, col - 1, Direction.L);
                GenerateSnakes(DeepArrayCopy(snake), idx + 1, row + 1, col, Direction.U);

                visitedBlocks.Remove(blockPosition);
            }
        }

        private static ISet<string> GetAllIdenticalSnakes(Direction[] snake)
        {
            var originSnake = DeepArrayCopy(snake);
            ISet<string> identicalSnakes = new HashSet<string>();
            
            identicalSnakes.Add(GetSnakeString(originSnake));

            var flippedSnake = FlipSnake(originSnake);
            identicalSnakes.Add(GetSnakeString(flippedSnake));

            var reversedSnake = ReverseSnake(originSnake);
            identicalSnakes.Add(GetSnakeString(reversedSnake));
            
            var flippedReversedSnake = ReverseSnake(flippedSnake);
            identicalSnakes.Add(GetSnakeString(flippedReversedSnake));
            
            for (var i = 0; i < 3; i++)
            {
                RotateSnake(originSnake);
                identicalSnakes.Add(GetSnakeString(originSnake));
                RotateSnake(flippedSnake);
                identicalSnakes.Add(GetSnakeString(flippedSnake));
                RotateSnake(reversedSnake);
                identicalSnakes.Add(GetSnakeString(reversedSnake));
                RotateSnake(flippedReversedSnake);
                identicalSnakes.Add(GetSnakeString(flippedReversedSnake));
            }

            return identicalSnakes;
        }

        private static Direction[] ReverseSnake(Direction[] snake)
        {
            var reversedSnake = DeepArrayCopy(snake);

            for (int i = 1; i < snake.Length; i++)
            {
                reversedSnake[i] = snake[snake.Length - i];
            }

            return reversedSnake;
        }

        private static void RotateSnake(Direction[] snake)
        {
            for (int i = 1; i < snake.Length; i++)
            {
                if (snake[i] == Direction.U)
                {
                    snake[i] = Direction.R;
                }
                else
                {
                    snake[i]++;
                }
            }
        }

        private static Direction[] FlipSnake(Direction[] snake)
        {
            var flippedSnake = DeepArrayCopy(snake);

            for (int i = 0; i < flippedSnake.Length; i++)
            {
                switch (flippedSnake[i])
                {
                    case Direction.D:
                        flippedSnake[i] = Direction.U;
                        break;
                    case Direction.U:
                        flippedSnake[i] = Direction.D;
                        break;
                }
            }

            return flippedSnake;
        }

        private static void PrintSnakes(IList<Direction[]> directions)
        {
            foreach (Direction[] direction in directions)
            {
                var dirStr = GetSnakeString(direction);
                Console.WriteLine(dirStr);
            }

            Console.WriteLine($"Snakes count = {directions.Count}");
        }

        private static string GetSnakeString(Direction[] direction)
        {
            return string.Join("", direction);
        }

        private static Direction[] DeepArrayCopy(Direction[] directions)
        {
            Direction[] newArray = new Direction[directions.Length];
            Array.Copy(directions, newArray, directions.Length);
            return newArray;
        }
    }

    enum Direction
    {
        S,
        R,
        D,
        L,
        U
    }
}

