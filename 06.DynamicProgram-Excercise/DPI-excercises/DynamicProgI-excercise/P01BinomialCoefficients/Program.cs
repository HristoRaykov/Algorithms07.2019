using System;
using System.Collections.Generic;

namespace P01BinomialCoefficients
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            var memorized = new Dictionary<string, decimal>();

            var binomCoef = NChooseK(n, k, memorized);

            Console.WriteLine(binomCoef);
        }

        private static decimal NChooseK(int n, int k, IDictionary<string, decimal> memorized)
        {
            if (k > n)
            {
                return 0;
            }

            if (k == 0 || k == n)
            {
                return 1;
            }

            var key = $"{n} {k}";
            if (memorized.ContainsKey(key))
            {
                return memorized[key];
            }

            var binomCoef = NChooseK(n - 1, k - 1, memorized) + NChooseK(n - 1, k, memorized);
           
            memorized[key] = binomCoef;
            
            return binomCoef;
        }
    }
}