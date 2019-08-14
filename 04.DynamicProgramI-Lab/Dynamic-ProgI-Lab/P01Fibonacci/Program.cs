using System;

namespace P01Fibonacci
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var f1 = 1;
            var f2 = 1;
            int fn;
            var counter = 2;
            while (counter <= n)
            {
                fn = f1 + f2;
                f1 = f2;
                f2 = fn;
                counter++;
            }

            Console.WriteLine(f1);
        }
    }
}