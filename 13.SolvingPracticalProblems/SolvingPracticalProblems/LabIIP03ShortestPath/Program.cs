using System;
using System.Collections.Generic;


namespace LabIIP03ShortestPath
{
    internal class Program
    {
        private static char[] path;

        private static List<int> emptyIdxes;

        private static char[] directions;

        private static List<string> allPaths;


        public static void Main(string[] args)
        {
            path = Console.ReadLine().ToCharArray();
            emptyIdxes = new List<int>();
            directions = new char[] {'L', 'R', 'S'};
            allPaths = new List<string>();

            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '*')
                {
                    emptyIdxes.Add(i);
                }
            }

            if (emptyIdxes.Count == 0)
            {
                Console.WriteLine(1);
                Console.WriteLine(new string(path));
            }

            FindAllPaths(0);

            Console.WriteLine(allPaths.Count);
            foreach (var path in allPaths)
            {
                Console.WriteLine(path);
            }
        }

        private static void FindAllPaths(int emptyIdx)
        {
            if (emptyIdx == emptyIdxes.Count)
            {
                allPaths.Add(new string(path));
                return;
            }

            var pathIdx = emptyIdxes[emptyIdx];
            foreach (var direction in directions)
            {
                path[pathIdx] = direction;
                FindAllPaths(emptyIdx + 1);
            }
        }
    }
}