using System;
using System.Collections.Generic;
using System.Linq;

namespace P03DividingPresentsDP
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var prices = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var possibleSums = CalcAllPossibleSums(prices);

            var targetSum = prices.Sum() / 2.0;

            var alansSum = GetAlansSum(possibleSums, targetSum);
            var totalSum = prices.Sum();
            var bobsSum = totalSum - alansSum;

            var alansPresents = ReconstructAlansPresents(possibleSums, alansSum);

            Console.WriteLine($"Difference: {bobsSum - alansSum}");
            Console.WriteLine($"Alan:{alansSum} Bob:{bobsSum}");
            Console.WriteLine($"Alan takes: {string.Join(" ",alansPresents.Select(x=>x.ToString()))}");
            Console.WriteLine("Bob takes the rest.");
        }

        private static int GetAlansSum(IDictionary<int, int> possibleSums, double targetSum)
        {
            var minDiff = targetSum;
            var minDiffSum = targetSum;
            foreach (var sum in possibleSums.Keys)
            {
                var currDiff = targetSum - sum;
                if (currDiff < minDiff && currDiff >= 0)
                {
                    minDiff = currDiff;
                    minDiffSum = sum;
                }
            }

            return (int) minDiffSum;
        }

        private static IDictionary<int, int> CalcAllPossibleSums(int[] prices)
        {
            var possibleSums = new Dictionary<int, int>();
            possibleSums[0] = 0;


            for (int i = 0; i < prices.Length; i++)
            {
                var currPrice = prices[i];
                foreach (var sum in possibleSums.Keys.ToList())
                {
                    var newSum = sum + currPrice;
                    if (!possibleSums.ContainsKey(newSum))
                    {
                        possibleSums[newSum] = currPrice;
                    }
                }
            }

            return possibleSums;
        }


        private static IList<int> ReconstructAlansPresents(IDictionary<int, int> possibleSums, int targetSum)
        {
            var presents = new List<int>();
            while (targetSum != 0)
            {
                var present = possibleSums[targetSum];
                presents.Add(present);
                targetSum -= present;
            }

            return presents;
        }
    }
}