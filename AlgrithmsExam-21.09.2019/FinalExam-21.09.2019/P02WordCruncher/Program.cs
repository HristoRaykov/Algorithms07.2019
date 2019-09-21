using System;
using System.Collections.Generic;
using System.Linq;

namespace P02WordCruncher
{
    class Program
    {
        private static Dictionary<string, int> strings;

        private static Dictionary<int, SortedSet<string>> stringsIdxes;

        private static string word;

        private static List<string> combinations;

        static void Main(string[] args)
        {
            ReadInput();

            GenerateWord(0, new List<string>());

            if (combinations.Count > 0)
            {
                Console.WriteLine(string.Join(Environment.NewLine, combinations));
            }
        }

        private static void GenerateWord(int idx, List<string> combStrings)
        {
            if (idx == word.Length)
            {
                combinations.Add(string.Join(" ", combStrings));
                return;
            }

            if (idx > word.Length)
            {
                return;
            }

            var possibleStrs = stringsIdxes[idx];
            foreach (var possibleStr in possibleStrs)
            {
                if (strings[possibleStr] > 0)
                {
                    strings[possibleStr]--;
                    combStrings.Add(possibleStr);
                    GenerateWord(idx + possibleStr.Length, combStrings);
                    strings[possibleStr]++;
                    combStrings.Remove(possibleStr);
                }
            }
        }

        private static void ReadInput()
        {
            var input = Console.ReadLine().Split(", ").ToList();

            word = Console.ReadLine();

            strings = new Dictionary<string, int>();
            combinations = new List<string>();

            stringsIdxes = new Dictionary<int, SortedSet<string>>();
            for (int i = 0; i < word.Length; i++)
            {
                stringsIdxes[i] =
                    new SortedSet<string>(Comparer<string>.Create((s1, s2) => s1.Length.CompareTo(s2.Length)));
            }

            foreach (var inputStr in input)
            {
                if (!strings.ContainsKey(inputStr))
                {
                    strings[inputStr] = 0;
                }

                strings[inputStr]++;
            }

            foreach (var s in strings.Keys)
            {
                var matchIdxes = FindMatchIndexes(word, s);
                foreach (var matchIdx in matchIdxes)
                {
                    stringsIdxes[matchIdx].Add(s);
                }
            }
        }

        private static List<int> FindMatchIndexes(string word, string s)
        {
            var matchIdxes = new List<int>();

            var idx = word.IndexOf(s);
            while (idx != -1)
            {
                matchIdxes.Add(idx);
                idx = word.IndexOf(s, idx + s.Length);
            }

            return matchIdxes;
        }
    }
}