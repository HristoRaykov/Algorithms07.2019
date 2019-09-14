using System;
using System.Collections.Generic;
using System.Linq;

namespace LabIP03Lumber
{
    internal class Program
    {
        private static LumberLog[] logs;

        private static List<int>[] graph;

        private static List<string> queries;

        private static List<List<int>> connComponents;

        private static int[] nodesComponents;

        private static bool[] visited;


        public static void Main(string[] args)
        {
            ReadInput();

            CalculateConnectedComponents();

            CalculateNodesComponents();

            foreach (var query in queries)
            {
                var nodes = query.Split(' ').Select(int.Parse).ToArray();
                if (nodesComponents[nodes[0]-1] == nodesComponents[nodes[1]-1])
                {
                    Console.WriteLine("YES");
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }
        }

        private static void CalculateNodesComponents()
        {
            for (int i = 0; i < connComponents.Count; i++)
            {
                var component = connComponents[i];
                foreach (var node in component)
                {
                    nodesComponents[node] = i;
                }
            }
        }

        private static void CalculateConnectedComponents()
        {
            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    connComponents.Add(new List<int>());
                    Dfs(node);
                }
            }
        }

        private static void Dfs(int node)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;
            connComponents.Last().Add(node);

            var children = graph[node];
            foreach (var child in children)
            {
                Dfs(child);
            }
        }

        private static void ReadInput()
        {
            var line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var numberOfLogs = line[0];
            var numberOfQueries = line[1];

            logs = new LumberLog[numberOfLogs];
            graph = new List<int>[numberOfLogs];

            for (int i = 0; i < numberOfLogs; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < numberOfLogs; i++)
            {
                var coordinates = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                var log = new LumberLog(coordinates[0], coordinates[2], coordinates[3], coordinates[1]);


                for (int j = 0; j < i; j++)
                {
                    if (log.DoesIntersect(logs[j]))
                    {
                        graph[i].Add(j);
                        graph[j].Add(i);
                    }
                }


                logs[i] = log;
            }

            queries = new List<string>();
            for (int i = 0; i < numberOfQueries; i++)
            {
                queries.Add(Console.ReadLine());
            }

            connComponents = new List<List<int>>();
            visited = new bool[numberOfLogs];
            nodesComponents = new int[numberOfLogs];
        }
    }

    internal class LumberLog
    {
        public int MinX { get; } // Ax
        public int MaxX { get; } // Bx
        public int MinY { get; } // By
        public int MaxY { get; } // Ay

        public LumberLog(int minX, int maxX, int minY, int maxY)
        {
            this.MinX = minX;
            this.MaxX = maxX;
            this.MinY = minY;
            this.MaxY = maxY;
        }

        public bool DoesIntersect(LumberLog other)
        {
            if (this.MinY <= other.MaxY && this.MaxY >= other.MinY &&
                this.MinX <= other.MaxX && this.MaxX >= other.MinX)
            {
                return true;
            }

            return false;
        }
    }
}