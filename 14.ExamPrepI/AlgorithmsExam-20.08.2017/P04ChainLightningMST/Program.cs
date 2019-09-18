using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace P04ChainLightningMST
{
    internal class Program
    {
        private static List<Edge>[] nhoods;

        private static int lightningsCount;

//        private static Dictionary<int, int> lightnings;

        private static int[] nhoodsDamage;

        private static List<int>[] msf;

//        private static OrderedBag<Edge> priorQueue;

        private static bool[] visited;


        public static void Main(string[] args)
        {
            ReadInput();

            msf = new List<int>[nhoods.Length];
            for (int i = 0; i < msf.Length; i++)
            {
                msf[i] = new List<int>();
            }

            visited = new bool[nhoods.Length];


            for (int nhood = 0; nhood < nhoods.Length; nhood++)
            {
                if (!visited[nhood])
                {
                    Prim(nhood);
                }
            }

            for (int i = 0; i < lightningsCount; i++)
            {
                var line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                var nhood = line[0];
                var damage = line[1];
                
                Dfs(nhood, nhood, damage);
            }


            Console.WriteLine(nhoodsDamage.Max());
        }

        private static void Dfs(int nhood, int previos, int damage)
        {
            nhoodsDamage[nhood] += damage;

            var children = msf[nhood];
            foreach (var child in children)
            {
                if (child != previos)
                {
                    Dfs(child, nhood, damage / 2);
                }
            }
        }

        private static void Prim(int nhood)
        {
            visited[nhood] = true;
            var priorQueue = new OrderedBag<Edge>();
            priorQueue.AddMany(nhoods[nhood]);

            while (priorQueue.Count > 0)
            {
                var currEdge = priorQueue.RemoveFirst();
                int source;
                int dest;

                if (visited[currEdge.FirstNhood] && !visited[currEdge.SecondNhood])
                {
                    source = currEdge.FirstNhood;
                    dest = currEdge.SecondNhood;
                }
                else if (!visited[currEdge.FirstNhood] && visited[currEdge.SecondNhood])
                {
                    source = currEdge.SecondNhood;
                    dest = currEdge.FirstNhood;
                }
                else
                {
                    continue;
                }

                visited[dest] = true;

                msf[source].Add(dest);
                msf[dest].Add(source);

                priorQueue.AddMany(nhoods[dest]);
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