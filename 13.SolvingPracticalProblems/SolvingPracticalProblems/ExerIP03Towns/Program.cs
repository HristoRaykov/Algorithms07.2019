using System;
using System.Linq;

namespace ExerIP03Towns
{
    internal class Program
    {
        private static string[] townsNames;

        private static int[] townsPopul;

        public static void Main(string[] args)
        {
            ReadInput();

            var lis = new int[townsNames.Length];
            var lds = new int[townsNames.Length];

            for (int i = 0; i < lis.Length; i++)
            {
                lis[i] = 1;
                lds[i] = 1;
            }

            FIllLongestSubsequence(townsPopul, lis);
            var reversed = townsPopul.Reverse().ToArray();
            FIllLongestSubsequence(reversed, lds);
            lds = lds.Reverse().ToArray();

            var maxSum = 0;
            for (int i = 0; i < lis.Length; i++)
            {
                if (lis[i]+lds[i] > maxSum)
                {
                    maxSum = lis[i] + lds[i];
                }
            }
            
            Console.WriteLine(maxSum-1);
        }

        private static void FIllLongestSubsequence(int[] population, int[] ls)
        {
            ls[0] = 1;
            for (int town = 1; town < population.Length; town++)
            {
                for (int prevTown = 0; prevTown < town; prevTown++)
                {
                    if (population[town] > population[prevTown] && ls[prevTown] + 1 > ls[town])
                    {
                        ls[town] = ls[prevTown] + 1;
                    }
                }
            }
        }

        private static void ReadInput()
        {
            var townsCount = int.Parse(Console.ReadLine());

            townsNames = new string[townsCount];
            townsPopul = new int[townsCount];

            for (int i = 0; i < townsCount; i++)
            {
                var line = Console.ReadLine().Split(' ').ToArray();
                townsNames[i] = line[1];
                townsPopul[i] = int.Parse(line[0]);
            }
        }
    }
}