using System;
using System.Collections.Generic;

namespace P01AbaspaBasapa
{
    internal class Program
    {
        private static char[] firstString;

        private static char[] secondString;

        private static List<char> maxSubSeq;


        public static void Main(string[] args)
        {
            firstString = Console.ReadLine().ToCharArray();
            secondString = Console.ReadLine().ToCharArray();

            maxSubSeq = new List<char>();

            for (int fsIdx = 0; fsIdx < firstString.Length; fsIdx++)
            {
                var fsChar = firstString[fsIdx];
                var fsMaxSubSeq = new List<char>();

                for (int ssIdx = 0; ssIdx < secondString.Length; ssIdx++)
                {
                    var ssChar = secondString[ssIdx];

                    if (fsChar == ssChar)
                    {
                        var ssMaxSubSeq = new List<char>();
                        var fsSearchIdx = fsIdx;
                        var ssSearchIdx = ssIdx;

                        while (fsSearchIdx < firstString.Length && ssSearchIdx < secondString.Length)
                        {
                            if (firstString[fsSearchIdx] != secondString[ssSearchIdx])
                            {
                                break;
                            }

                            ssMaxSubSeq.Add(firstString[fsSearchIdx]);
                            fsSearchIdx++;
                            ssSearchIdx++;
                        }

                        if (fsMaxSubSeq.Count < ssMaxSubSeq.Count)
                        {
                            fsMaxSubSeq = ssMaxSubSeq;
                        }
                    }
                }

                if (maxSubSeq.Count < fsMaxSubSeq.Count)
                {
                    maxSubSeq = fsMaxSubSeq;
                }
            }


            Console.WriteLine(new String(maxSubSeq.ToArray()));
        }
    }
}