using System;
using System.Collections.Generic;
using System.Linq;

namespace P01DistanceBetweenVertices
{
    internal class Program
    {
        private static int verticesCount;

        private static int pairsCount;

        private static Dictionary<int, List<int>> graph;

        private static Dictionary<int, int> pairs;

        private static Dictionary<int, Dictionary<int, int>> verticesDist;
        

        public static void Main(string[] args)
        {
            ReadInput();
            verticesDist = new Dictionary<int, Dictionary<int, int>>();

            var distance = CalculateDistanceBetweenVerices(1, 2);


            Console.WriteLine();
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
            throw new NotImplementedException();
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
                var values = kvp[1] == "" ? new List<int>(){-1} : kvp[1].Split(' ').Select(int.Parse).ToList();
                graph[key] = values;
            }

            pairs = new Dictionary<int, int>();
            for (int i = 0; i < pairsCount; i++)
            {
                line = Console.ReadLine();
                var kvp = line.Split('-');
                var key = int.Parse(kvp[0]);
                var value = int.Parse(kvp[1]);
                pairs[key] = value;
            }
        }
    }
}