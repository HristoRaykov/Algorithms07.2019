using System;
using System.Collections.Generic;
using System.Linq;

namespace P02BiConnectedComponents
{
    internal class Program
    {
        private static List<int>[] graph;

        private static bool[] visited;

        private static int[] depths;

        private static int[] lowpoints;

        private static int[] parents;

        private static List<int> articPoints;

        private static List<HashSet<int>> biConnComponents;

        private static Stack<string> edgeStack;


        public static void Main(string[] args)
        {
            ReadInput();

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    Dfs(node, 1);
                }
            }


            Console.WriteLine("Number of bi-connected components: " + biConnComponents.Count);
        }

        private static void Dfs(int node, int depth)
        {
            depths[node] = depth;
            lowpoints[node] = depth;
            visited[node] = true;

            var childCount = 0;
            var isArticulation = false;

            var children = graph[node];
            foreach (var child in children)
            {
                var currEdge = $"{node}-{child}";
                if (!visited[child])
                {
                    edgeStack.Push(currEdge);
                    parents[child] = node;
                    Dfs(child, depth + 1);
                    childCount++;

                    if (depths[node] <= lowpoints[child])
                    {
                        isArticulation = true;
                        GetBiConnCompNodes(currEdge);
                    }

                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[child]);
                }
                else if (parents[node] != child)
                {
                    lowpoints[node] = Math.Min(lowpoints[node], depths[child]);
                }
            }

            if ((parents[node] == -1 && childCount > 1) || (parents[node] != -1 && isArticulation))
            {
                articPoints.Add(node);
            }
        }

        private static void GetBiConnCompNodes(string currEdge)
        {
            var component = new HashSet<int>();
            while (true)
            {
                var edge = edgeStack.Pop();

                var nodes = edge.Split('-').Select(int.Parse).ToArray();
                var firstNode = nodes[0];
                var secondNode = nodes[1];
                component.Add(firstNode);
                component.Add(secondNode);
                if (edge == currEdge)
                {
                    break;
                }
            }

            biConnComponents.Add(component);
        }

        private static void ReadInput()
        {
            var nodesCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            var edgesCount = int.Parse(Console.ReadLine().Split(' ')[1]);

            graph = new List<int>[nodesCount];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                var firstNode = line[0];
                var secondNode = line[1];
                graph[firstNode].Add(secondNode);
                graph[secondNode].Add(firstNode);
            }

            visited = new bool[nodesCount];
            depths = new int[nodesCount];
            lowpoints = new int[nodesCount];
            parents = new int[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                parents[i] = -1;
            }

            articPoints = new List<int>();
            biConnComponents = new List<HashSet<int>>();
            edgeStack = new Stack<string>();
        }
    }
}