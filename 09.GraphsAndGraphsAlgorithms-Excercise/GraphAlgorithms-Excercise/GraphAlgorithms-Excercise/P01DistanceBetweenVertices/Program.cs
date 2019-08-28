using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace P01DistanceBetweenVertices
{
    internal class Program
    {
        private static int verticesCount;

        private static int pairsCount;

        private static Dictionary<int, List<int>> graph;

        private static List<KeyValuePair<int, int>> pairs;

        private static Dictionary<int, Dictionary<int, int>> verticesDist;


        public static void Main(string[] args)
        {
            ReadInput();
            verticesDist = new Dictionary<int, Dictionary<int, int>>();

            foreach (var pair in pairs)
            {
                var startVertice = pair.Key;
                var targetVertice = pair.Value;
                var distance = CalculateDistanceBetweenVerices(startVertice, targetVertice);

                Console.WriteLine($"{{{startVertice}, {targetVertice}}} -> {distance}");
            }
        }


        private static int CalculateDistanceBetweenVerices(int startVertice, int endVertice)
        {
            if (verticesDist.ContainsKey(startVertice))
            {
                return verticesDist[startVertice][endVertice];
            }

            CalcAllDistancesForVerice(startVertice);

            return verticesDist[startVertice][endVertice];
        }

        private static void CalcAllDistancesForVerice(int startVertice)
        {
            var distances = new Dictionary<int, int>();
            foreach (var vertice in graph.Keys)
            {
                if (vertice != startVertice)
                {
                    distances[vertice] = -1;
                }
            }

            var count = 1;
            var visited = new HashSet<int>();
            DFS(startVertice, distances, visited, count);

            verticesDist[startVertice] = distances;
        }

        private static void DFS(int currVertice, Dictionary<int, int> distances, HashSet<int> visited,int count)
        {
            var descendants = graph[currVertice];
            if (descendants.Count == 0)
            {
                return;
            }

            visited.Add(currVertice);
            foreach (var descendant in descendants)
            {
                if (visited.Contains(descendant))
                {
                    continue;
                }

                var currDist = distances[descendant];
                if (currDist == -1 || count < currDist)
                {
                    distances[descendant] = count;
                }

                DFS(descendant, distances, visited, count + 1);
            }

            visited.Remove(currVertice);
        }

        private static void ReadInput()
        {
            verticesCount = int.Parse(Console.ReadLine());
            pairsCount = int.Parse(Console.ReadLine());

            graph = new Dictionary<int, List<int>>();
            string line;
            for (int i = 0; i < verticesCount; i++)
            {
                line = Console.ReadLine();
                var kvp = line.Split(':');
                var key = int.Parse(kvp[0]);
                var values = kvp[1] == "" ? new List<int>() : kvp[1].Split(' ').Select(int.Parse).ToList();
                graph[key] = values;
            }

            pairs = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < pairsCount; i++)
            {
                line = Console.ReadLine();
                var kvp = line.Split('-');
                var key = int.Parse(kvp[0]);
                var value = int.Parse(kvp[1]);
                var pair = new KeyValuePair<int, int>(key, value);
                pairs.Add(pair);
            }
        }
    }
}