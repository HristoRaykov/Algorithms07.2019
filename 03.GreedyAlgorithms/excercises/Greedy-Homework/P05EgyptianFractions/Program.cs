using System;
using System.Collections.Generic;
using System.Linq;

namespace P05EgyptianFractions
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split('/');
            var p = int.Parse(input[0]);
            var q = int.Parse(input[1]);

            if (p>q)
            {
                Console.WriteLine("Error (fraction is equal to or greater than 1)");
                return;
            }

            var denominators = CalculateEgyptianFractions(p, q);

            PrintEgyptianFraction(p, q, denominators);
        }

        private static List<int> CalculateEgyptianFractions(int p, int q)
        {
            var denominators = new List<int>();
            var d = 2;
            var numerator = p;
            var denominator = q;
            while (true)
            {
                var residualNumerator = d * numerator - denominator;
                if (residualNumerator == 0)
                {
                    denominators.Add(d);
                    break;
                }

                if (residualNumerator < 0)
                {
                    d++;
                    continue;
                }

                numerator = residualNumerator;
                denominator = d * denominator;
                denominators.Add(d);
                d++;
            }


            return denominators;
        }

        private static void PrintEgyptianFraction(int p, int q, List<int> denominators)
        {
            var denomStr = denominators
                .Select(d => $"1/{d}")
                .ToList();
            var result = string.Join(" + ", denomStr);

            Console.Write($"{p}/{q} = {result}");
        }
    }
}