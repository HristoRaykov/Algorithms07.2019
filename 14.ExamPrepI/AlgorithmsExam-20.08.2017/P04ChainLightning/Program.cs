using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace P04ChainLightning
{
    internal class Program
    {
        private static List<Edge>[] nhoods;

//        private static Dictionary<int, int> lightnings;
        private static int lightningsCount;

        private static int[] nhoodsDamage;


        public static void Main(string[] args)
        {
            ReadInput();

//            foreach (var nhood in lightnings.Keys)
//            {
//                var initialDamage = lightnings[nhood];
//                PerformChainLightning(nhood, initialDamage);
//            }

            
            for (int i = 0; i < lightningsCount; i++)
            {
                var line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                PerformChainLightning(line[0], line[1]);
            }

            Console.WriteLine(nhoodsDamage.Max());
        }

        private static void PerformChainLightning(int start, int initialDamage)
        {
            var priorQueue = new OrderedBag<Edge>();
            priorQueue.AddMany(nhoods[start]);

            var visited = new bool[nhoods.Length];
            var chainFactors = new int[nhoods.Length];

            visited[start] = true;
            chainFactors[start] = 1;
            nhoodsDamage[start] += initialDamage / chainFactors[start];

            while (priorQueue.Count > 0)
            {
                var currEdge = priorQueue.RemoveFirst();

                var source = visited[currEdge.FirstNhood] ? currEdge.FirstNhood : currEdge.SecondNhood;

                var destination = currEdge.GetDestination(source);
                if (!visited[destination])
                {
                    visited[destination] = true;
                    chainFactors[destination] = chainFactors[source] * 2;
                    nhoodsDamage[destination] += initialDamage / chainFactors[destination];

                    var destEdges = nhoods[destination];

                    foreach (var destEdge in destEdges)
                    {
                        var destNhood = destEdge.GetDestination(destination);
                        if (!visited[destNhood])
                        {
                            priorQueue.Add(destEdge);
                        }
                    }
                }
            }
        }

        private static void ReadInput()
        {
            var nhoodsCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());
            lightningsCount = int.Parse(Console.ReadLine());

            nhoods = new List<Edge>[nhoodsCount];
//            lightnings = new Dictionary<int, int>();
            nhoodsDamage = new int[nhoodsCount];


            for (int i = 0; i < nhoodsCount; i++)
            {
                nhoods[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                var edge = new Edge(line[0], line[1], line[2]);
                nhoods[line[0]].Add(edge);
                nhoods[line[1]].Add(edge);
            }

//            for (int i = 0; i < lightningsCount; i++)
//            {
//                var line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
//
//                lightnings[line[0]] = line[1];
//            }
        }
    }

    internal class Edge : IComparable<Edge>
    {
        public int FirstNhood { get; }
        public int SecondNhood { get; }
        public int Distance { get; }

        public Edge(int firstNhood, int secondNhood, int distance)
        {
            FirstNhood = firstNhood;
            SecondNhood = secondNhood;
            Distance = distance;
        }

        public int GetDestination(int source)
        {
            if (FirstNhood == source)
            {
                return SecondNhood;
            }

            if (SecondNhood == source)
            {
                return FirstNhood;
            }

            return -1;
        }

        public int CompareTo(Edge other)
        {
            return Distance.CompareTo(other.Distance);
        }
    }
}