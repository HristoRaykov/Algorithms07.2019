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

            Console.WriteLine();
        }

        private static int GetAlansSum(IDictionary<int, List<int>> possibleSums, double targetSum)
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

        private static IDictionary<int, List<int>> CalcAllPossibleSums(int[] prices)
        {
            var possibleSums = new Dictionary<int, List<int>>();
            possibleSums[0] = new List<int>() {0};


            for (int i = 0; i < prices.Length; i++)
            {
                var currPrice = prices[i];
                foreach (var sum in possibleSums.Keys.ToList())
                {
                    var newSum = sum + currPrice;
                    if (possibleSums.ContainsKey(newSum))
                    {
                        var prevNums = possibleSums[newSum];
                        prevNums.Add(currPrice);
                        possibleSums[newSum] = prevNums;
                    }
                    else
                    {
                        possibleSums[newSum] = new List<int>() {currPrice};
                    }
                }
            }


            return possibleSums;
        }
    }
}