using System;
using System.Collections.Generic;
using System.Linq;

namespace P04SumWithUnlimitedAmountOfCoins
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            var nums = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var targetSum = int.Parse(Console.ReadLine());

            var sumCounts = CalcAllPossibleSums(nums, targetSum);

            Console.WriteLine(sumCounts[targetSum]);
        }


        private static int[] CalcAllPossibleSums(int[] nums, int targetSum)
        {
            var sumCounts = new int[targetSum + 1];
            sumCounts[0] = 1;

            for (int numIdx = 1; numIdx <= nums.Length; numIdx++)
            {
                var currNum = nums[numIdx-1];
                for (int sum = 1; sum < targetSum + 1; sum++)
                {
                    if (sum >= currNum)
                    {
                        sumCounts[sum] += sumCounts[sum - currNum];
                    }
                }
            }


            return sumCounts;
        }
    }
}
