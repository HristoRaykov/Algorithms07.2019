using System;
using System.Collections.Generic;
using System.Linq;

namespace P01ShootingRange
{
    internal class Program
    {
        private static int[] targets;

        private static int targetScore;

        private static HashSet<string> scoreComb;

        private static bool[] visited;

        private static int[] comb;

        public static void Main(string[] args)
        {
            targets = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            targetScore = int.Parse(Console.ReadLine());

            scoreComb = new HashSet<string>();

            comb = new int[targets.Length];
            visited = new bool[targets.Length];

            FindScoreCombinations(0);

            Console.WriteLine(String.Join(Environment.NewLine, scoreComb));
        }

        private static void FindScoreCombinations(int idx)
        {
            var score = GetCurrScore(idx);
            if (score == targetScore)
            {
                var hitTargets = GetCurrTargets(idx);
                scoreComb.Add(string.Join(" ", hitTargets));
                return;
            }

            if (idx == targets.Length || score > targetScore)
            {
                return;
            }

            FindScoreCombinations(idx + 1);
            var swapped = new HashSet<int>();
            swapped.Add(targets[idx]);
            for (int i = idx + 1; i < targets.Length; i++)
            {
                if (!swapped.Contains(targets[i]))
                {
                    Swap(idx, i);
                    FindScoreCombinations(idx + 1);
                    Swap(idx, i);
                    swapped.Add(targets[i]);
                }
            }
        }

        private static int GetCurrScore(int idx)
        {
            var score = 0;
            for (int i = 0; i < idx; i++)
            {
                var target = targets[i];
                score += (i + 1) * target;
            }

            return score;
        }

        private static List<int> GetCurrTargets(int idx)
        {
            var currTargets = new List<int>();
            for (int i = 0; i < idx; i++)
            {
                var target = targets[i];
                currTargets.Add(target);
            }

            return currTargets;
        }

        private static void CheckScore()
        {
            var score = 0;
            var currTargets = new List<int>();
            for (int i = 0; i < targets.Length; i++)
            {
                var target = targets[i];
                currTargets.Add(target);
                score += (i + 1) * target;
                if (score == targetScore)
                {
                    scoreComb.Add(string.Join(" ", currTargets));
                    return;
                }

                if (score > targetScore)
                {
                    return;
                }
            }
        }

        private static void Swap(int idx1, int idx2)
        {
            var temp = targets[idx1];
            targets[idx1] = targets[idx2];
            targets[idx2] = temp;
        }
    }
}