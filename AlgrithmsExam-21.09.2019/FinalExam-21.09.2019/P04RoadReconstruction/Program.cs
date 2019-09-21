using System;
using System.Collections.Generic;
using System.Linq;

namespace P04RoadReconstruction
{
    class Program
    {
        private static List<Edge> edges;

        private static List<int>[] graph;

        private static List<string> imortantEdges;

        static void Main(string[] args)
        {
            ReadInput();

            foreach (var edge in edges)
            {
                var firstNode = edge.FirstNode;
                var secondNode = edge.SecondNode;
                graph[firstNode].Remove(secondNode);
                graph[secondNode].Remove(firstNode);

                var visited = new bool[graph.Length];
                visited[firstNode] = true;
                var hasAlternativePath = FindPath(firstNode, firstNode, secondNode, visited);
                if (!hasAlternativePath)
                {
                    imortantEdges.Add($"{firstNode} {secondNode}");
                }

                graph[firstNode].Add(secondNode);
                graph[secondNode].Add(firstNode);
            }

            Console.WriteLine("Important streets:");
            if (imortantEdges.Count > 0)
            {
                Console.WriteLine(string.Join(Environment.NewLine, imortantEdges));
            }
        }

        private static bool FindPath(int node, int prevNode, int targetNode, bool[] visited)
        {
            if (node == targetNode)
            {
                return true;
            }

            var children = graph[node];
            foreach (var child in children)
            {
                if (!visited[child] && child != prevNode)
                {
                    visited[child] = true;
                    var hasPath = FindPath(child, node, targetNode, visited);
                    if (hasPath)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void ReadInput()
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            edges = new List<Edge>();
            graph = new List<int>[nodesCount];
            imortantEdges = new List<string>();

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var line = Console.ReadLine().Split(" - ").Select(int.Parse).ToArray();
                var edge = new Edge(line[0], line[1]);
                edges.Add(edge);

                graph[line[0]].Add(line[1]);
                graph[line[1]].Add(line[0]);
            }
        }
    }

    internal class Edge
    {
        public int FirstNode { get; }

        public int SecondNode { get; }

        public Edge(int firstNode, int secondNode)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
        }
    }
}