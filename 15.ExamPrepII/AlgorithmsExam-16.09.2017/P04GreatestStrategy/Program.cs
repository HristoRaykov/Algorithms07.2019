using System;
using System.Collections.Generic;
using System.Linq;

namespace P04GreatestStrategy
{
    internal class Program
    {
        private static Dictionary<int, HashSet<int>> areas;

        private static int startArea;

        private static List<HashSet<int>> sections;

        public static void Main(string[] args)
        {
            ReadInput();

            FindSections(startArea, startArea);

            var sectionsAreas = sections.SelectMany(s=>s);
            var lastSection = new HashSet<int>(areas.Keys);
            lastSection.ExceptWith(sectionsAreas);
            sections.Add(lastSection);

            var maxSum = sections.Select(s => s.Sum()).Max();

            Console.WriteLine(maxSum);
        }

        private static HashSet<int> FindSections(int area, int prevArea)
        {
            var children = areas[area];
            var section = new HashSet<int>();
            foreach (var child in children)
            {
                if (child == prevArea)
                {
                    continue;
                }

                var childSection = FindSections(child, area);
                
                    
                if (childSection.Count % 2 == 0)//&& childSection.Count != 0
                {
                    sections.Add(new HashSet<int>(childSection));
                    childSection.Clear();
                }
                
                section.UnionWith(childSection);
            }

            section.Add(area);
            return section;
        }

        private static void ReadInput()
        {
            var line = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var areasCount = line[0];
            var connectionsCount = line[1];
            startArea = line[2];

            areas = new Dictionary<int, HashSet<int>>();
            for (int i = 1; i <= areasCount; i++)
            {
                areas[i] = new HashSet<int>();
            }

            for (int i = 0; i < connectionsCount; i++)
            {
                line = Console.ReadLine().Split().Select(int.Parse).ToArray();

                areas[line[0]].Add(line[1]);
                areas[line[1]].Add(line[0]);
            }

            sections = new List<HashSet<int>>();
        }
    }
}