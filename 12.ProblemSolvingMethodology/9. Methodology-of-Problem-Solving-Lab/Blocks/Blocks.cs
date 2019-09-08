namespace Blocks
{
    using System;
    using System.Collections.Generic;

    public class Blocks
    {
        public static void Main(string[] args)
        {
            // TODO
        }

        public static HashSet<string> FindBlocks(int numberOfLetters)
        {
            // TODO
            throw new NotImplementedException();
        }

        private static void FillLetters(int numberOfLetters, char[] letters)
        {
            // TODO
        }

        private static void GenerateVariations(char[] letters, char[] currentCombination, bool[] used, HashSet<string> results, int index = 0)
        {
            // TODO
        }

        private static void AddResult(char[] result, HashSet<string> results)
        {
           // TODO
        }

        private static void PrintBlocks(HashSet<string> results)
        {
            Console.WriteLine("Number of blocks: {0}", results.Count);
            foreach (var combination in results)
            {
                Console.WriteLine(combination);
            }
        }
    }
}