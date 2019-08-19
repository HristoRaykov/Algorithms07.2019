using System;
using System.Collections.Generic;
using System.Linq;

namespace P05SumWithLimitedAmoutOfCoins
{
    internal class Program
    {
        static HashSet<string> sumVariations = new HashSet<string>();

        public static void Main(string[] args)
        {
            var nums = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var targetSum = int.Parse(Console.ReadLine());

            var possibleSums = CalcAllPossibleSums(nums);

            var sumNums = new List<int>();
            ReconstructSums(possibleSums, targetSum, sumNums);
//            ReconstructSumsNoRep(possibleSums, targetSum);

            Console.WriteLine(possibleSums[targetSum].Count);
        }


        private static IDictionary<int, List<int>> CalcAllPossibleSums(int[] nums)
        {
            var possibleSums = new Dictionary<int, List<int>>();
            possibleSums[0] = new List<int>() {0};


            for (int i = 0; i < nums.Length; i++)
            {
                var currNum = nums[i];
                foreach (var sum in possibleSums.Keys.ToList())
                {
                    var newSum = sum + currNum;
                    if (possibleSums.ContainsKey(newSum))
                    {
                        var lastNums = possibleSums[newSum];
                        lastNums.Add(currNum);
                        possibleSums[newSum] = lastNums;
                    }
                    else
                    {
                        possibleSums[newSum] = new List<int>() {currNum};
                    }
                }
            }

            return possibleSums;
        }


//        private static void ReconstructSumsNoRep(IDictionary<int, List<int>> possibleSums, int targetSum)
//        {
//            var lastNums = possibleSums[targetSum];
//
//            foreach (var num in lastNums)
//            {
//                var sumNums = new List<int>() {num};
//                var totalSum = targetSum - num;
//                while (totalSum != 0)
//                {
//                    var currNum = possibleSums[totalSum][0];
//                    sumNums.Add(currNum);
//                    totalSum -= currNum;
//                }
//
//                sumVariations.Add(string.Join(" ", sumNums));
//            }
//        }

        private static void ReconstructSums(IDictionary<int, List<int>> possibleSums, int targetSum,
            List<int> sumNums)
        {
            if (targetSum == 0)
            {
                sumVariations.Add(string.Join(" ", sumNums));
                return;
            }

            var lastNums = possibleSums[targetSum];

            foreach (var num in lastNums)
            {
                sumNums.Add(num);
                ReconstructSums(possibleSums, targetSum - num, sumNums);
                sumNums.Remove(num);
            }
        }
    }
}