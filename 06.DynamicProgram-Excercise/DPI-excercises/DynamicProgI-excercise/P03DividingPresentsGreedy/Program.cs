using System;
using System.Collections.Generic;
using System.Linq;

namespace P03DividingPresentsGreedy
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var prices = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var firstTwinIdxs = DividePresents(prices);
            var secondTwinIdxs = Enumerable.Range(0,prices.Length)
                .Except(firstTwinIdxs)
                .OrderByDescending(i=>i)
                .ToList();

            var firstTwinPrices = firstTwinIdxs.Select(i => prices[i]).ToList();
            var secondTwinPrices = secondTwinIdxs.Select(i => prices[i]).ToList();

            var firstTwinSum = firstTwinPrices.Sum();
            var secondTwinSum = secondTwinPrices.Sum();

            List<int> AlanPresents;
            int AlanSum;
            int BobSum;
            if (firstTwinSum <= secondTwinSum)
            {
                AlanPresents = firstTwinPrices;
                AlanSum = firstTwinSum;
                BobSum = secondTwinSum;
            }
            else
            {
                AlanPresents = secondTwinPrices;
                AlanSum = secondTwinSum;
                BobSum = firstTwinSum;
            }

            var AlanPresentsStr = string.Join(" ", AlanPresents.Select(x => x.ToString()));
            Console.WriteLine($"Difference: {BobSum - AlanSum}");
            Console.WriteLine($"Alan:{AlanSum} Bob:{BobSum}");
            Console.WriteLine($"Alan takes: {AlanPresentsStr}");
            Console.WriteLine("Bob takes the rest.");
        }

        private static IList<int> DividePresents(int[] prices)
        {
            var firstTwinIdxs = new List<int>();
            var pricesCopy = new int[prices.Length];
            Array.Copy(prices, pricesCopy, prices.Length);

            var orderedPrices = prices.OrderBy(i => i).ToArray();

            var firstTotalValue = 0;
            var secondTotalValue = 0;
            int diff;

            for (int i = orderedPrices.Length-1; i >= 0 ; i--)
            {
                var currPrice = orderedPrices[i];
                diff = firstTotalValue - secondTotalValue;
                if (diff >=0)
                {
                    secondTotalValue += currPrice;
                }
                else
                {
                    firstTotalValue += currPrice;
                    var upperIdx = Array.LastIndexOf(pricesCopy, currPrice);
                    pricesCopy[upperIdx] = -1;
                    firstTwinIdxs.Add(upperIdx);
                }
            }

            firstTwinIdxs = firstTwinIdxs.OrderByDescending(i=>i).ToList();
            return firstTwinIdxs;
        }
    }
}