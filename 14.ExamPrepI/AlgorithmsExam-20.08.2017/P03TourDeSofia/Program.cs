using System;
using System.Collections.Generic;
using System.Linq;

namespace P03TourDeSofia
{
    internal class Program
    {
        private static int start;

        private static List<int>[] tour;

        private static int[] dist;

        private static int[] prev;

        private static bool[] visited;

        public static void Main(string[] args)
        {
            if (!ReadInput())
            {
                return;
            }


            CalcAllRoutes(start);

            var shortestRoute = ReconstructRoute();

            Console.WriteLine(shortestRoute);
        }

        private static int ReconstructRoute()
        {
            if (prev[start] == -1)
            {
                return visited.Count(x => x);
            }

            return dist[start];
        }

        private static void CalcAllRoutes(int start)
        {
            var queue = new Queue<int>();
            
            queue.Enqueue(start);
            visited[start] = true;
            dist[start] = 0;
            
            while (queue.Count > 0)
            {
                var currJunct = queue.Dequeue();

                var destJuncts = tour[currJunct];
                foreach (var destJunct in destJuncts)
                {
                    if (!visited[destJunct])
                    {
                        queue.Enqueue(destJunct);
                        visited[destJunct] = true;
                    }

                    if (destJunct == start)
                    {
                        if (dist[start] == 0)
                        {
                            dist[destJunct] = 1 + dist[currJunct];
                            prev[destJunct] = currJunct;
                        }
                    }

                    if (1 + dist[currJunct] < dist[destJunct])
                    {
                        dist[destJunct] = 1 + dist[currJunct];
                        prev[destJunct] = currJunct;
                    }
                }
            }
        }

        private static bool ReadInput()
        {
            var juncCount = int.Parse(Console.ReadLine());
            var streetsCount = int.Parse(Console.ReadLine());

            if (juncCount == 0 || streetsCount == 0)
            {
                Console.WriteLine(0);
                return false;
            }

            start = int.Parse(Console.ReadLine());

            tour = new List<int>[juncCount];
            for (int i = 0; i < tour.Length; i++)
            {
                tour[i] = new List<int>();
            }

            for (int i = 0; i < streetsCount; i++)
            {
                var line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                tour[line[0]].Add(line[1]);
            }

            dist = new int[juncCount];
            prev = new int[juncCount];
            for (int i = 0; i < juncCount; i++)
            {
                dist[i] = int.MaxValue;
                prev[i] = -1;
            }

            visited = new bool[juncCount];

            return true;
        }
    }
}