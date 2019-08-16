using System;
using System.Collections.Generic;
using System.Linq;

namespace P04RodCutting
{
    internal class Program
    {
        private static int[] prices;

        private static int len;

        private static int[] bestPrices;
        private static int[] bestPrevIdx;
        
        
        public static void Main(string[] args)
        {
            prices = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            len = int.Parse(Console.ReadLine());

            bestPrices = new int[prices.Length];
            bestPrevIdx = new int[prices.Length];

            CalculateBestPrices(prices);
            
            var bestPrice = bestPrices[len];
            var cuts = ReconstructRodCuts();
            
            Console.WriteLine(bestPrice);
            Console.WriteLine(string.Join(" ", cuts));

        }

        private static IList<int> ReconstructRodCuts()
        {
            var cuts = new List<int>();

            var currIdx = len;
            var prevIdx = bestPrevIdx[currIdx];
            while (currIdx != prevIdx)
            {
                cuts.Add(prevIdx);

                currIdx = currIdx - prevIdx;
                prevIdx = bestPrevIdx[currIdx];
            }
            
            cuts.Add(prevIdx);
            return cuts;
        }

        private static void CalculateBestPrices(int[] prices)
        {
            for (int currIdx = 0; currIdx < prices.Length; currIdx++)
            {
                var currBestPrice = prices[currIdx];
                var bestPrev = currIdx;
                for (int i = 1; i < currIdx; i++)
                {
                    var bestLeft = bestPrices[i];
                    var bestRight = bestPrices[currIdx - i];
                    if (currBestPrice < bestLeft + bestRight)
                    {
                        currBestPrice = bestLeft + bestRight;
                        bestPrev = i;
                    }
                }

                bestPrices[currIdx] = currBestPrice;
                bestPrevIdx[currIdx] = bestPrev;
            }
        }
    }
}