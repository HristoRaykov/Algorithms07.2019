using System;
using System.Linq;

namespace P03_IterPermWithRep
{
    class IterPermWithRep
    {
        private static string[] arr;

        static void Main(string[] args)
        {
            arr = Console.ReadLine().Split(" ").ToArray();

            string[] perm = new string[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                perm[i] = arr[0];
            }

            Permute(perm);
        }

        private static void Permute(string[] perm)
        {
           
        }

        private static void PrintArray(string[] perm)
        {
            Console.WriteLine(string.Join(" ", perm));
        }
    }
}