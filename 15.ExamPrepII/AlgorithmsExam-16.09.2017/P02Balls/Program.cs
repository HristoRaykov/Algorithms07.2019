using System;
using System.Collections.Generic;

namespace P02Balls
{
    internal class Program
    {
        private static int[] pockets;

        private static int balls;

        private static int maxBallsCount;

        private static List<string> combinations;


        public static void Main(string[] args)
        {
            ReadInput();

            for (int i = 0; i < pockets.Length; i++)
            {
                pockets[i] = 1;
            }

            var ballCount = balls - pockets.Length;
            GenerateCombinations(0, ballCount);

            Console.WriteLine(string.Join(Environment.NewLine, combinations));
        }

        private static void GenerateCombinations(int idx, int ballCount)
        {
            if (ballCount == 0)
            {
                combinations.Add(string.Join(", ", pockets));
                return;
            }

            if (idx >= pockets.Length)
            {
                return;
            }

            for (int i = maxBallsCount-1; i >= 0; i--)
            {
                if (ballCount - i >= 0)
                {
                    pockets[idx] += i;
                    GenerateCombinations(idx + 1, ballCount - i);
                    pockets[idx] -= i;
                }
            }
        }

        private static void ReadInput()
        {
            var pocketsCount = int.Parse(Console.ReadLine());
            balls = int.Parse(Console.ReadLine());
            maxBallsCount = int.Parse(Console.ReadLine());

            pockets = new int[pocketsCount];
            combinations = new List<string>();
        }
    }
}