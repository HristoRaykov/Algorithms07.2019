using System;
using System.Collections.Generic;
using System.Linq;

namespace LabIP02SumTo13
{
    class Program
    {
        private static int[] nums;

        private static List<int> possibleSums;
        
        private static void Main (string[] args)
        {
            nums = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            possibleSums = new List<int>();
                
            CalculateSums(-1, 0);

            if (possibleSums.Contains(13))
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }

        }
    
        private static void CalculateSums(int idx, int sum)
        {
            idx++;
        
            if (idx==3)
            {
                possibleSums.Add(sum);
                return;
            }
        
            int num = nums[idx];
        
            CalculateSums(idx, sum+num);
            CalculateSums(idx, sum-num);
        }

    }
}