using System;
using System.Collections.Generic;
using System.Linq;

namespace P01Blocks
{
    internal class Program
    {
        private static char[] letters;

        private static char[] blocks;

        private static bool[] used;

        private static List<string> generatedBlocks;


        public static void Main(string[] args)
        {
            ReadInput();

            GenerateBlocks(0);

            Console.WriteLine("Number of blocks: "+ generatedBlocks.Count);
            Console.WriteLine(string.Join(Environment.NewLine, generatedBlocks));
        }

        private static void GenerateBlocks(int blockIdx)
        {
            if (blockIdx == blocks.Length)
            {
                generatedBlocks.Add($"{blocks[0]}{blocks[1]}{blocks[2]}{blocks[3]}");
                return;
            }

            var firstChar = blocks[0]=='0' ? letters[0] : blocks[0];
            var firstCharLetterIdx = Array.IndexOf(letters, firstChar);
            
            // handling rotations
            if (firstCharLetterIdx == letters.Length - 1 - (blocks.Length - 1) + 1)
            {
                return;
            }

            for (int letterIdx = firstCharLetterIdx + 1; letterIdx < letters.Length; letterIdx++)
            {
                if (!used[letterIdx])
                {
                    used[letterIdx] = true;
                    var ch = letters[letterIdx];
                    blocks[blockIdx] = ch;
                    GenerateBlocks(blockIdx + 1);
                    used[letterIdx] = false;
                }
            }
        }

        private static void ReadInput()
        {
            var n = int.Parse(Console.ReadLine());

            letters = new char[n];
            for (int i = 0; i < n; i++)
            {
                letters[i] = (char) ('A' + i);
            }

            blocks = new char[4];

            used = new bool[n];
            generatedBlocks = new List<string>();
        }
    }
}