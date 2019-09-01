using System;
using System.Collections.Generic;
using System.Linq;

namespace P03MostReliablePath
{
    internal class Program
    {
        private static List<int>[] graph;

        private static List<Edge> edges;

        private static int startNode;

        private static int targetNode;

        private static double[] reliablity;

        private static bool[] visited;

        private static int[] previous;


        public static void Main(string[] args)
        {
            ReadInput();

            Dijkstra();

            var path = ReconstructPath();

            Console.WriteLine($"Most reliable path reliability: {reliablity[targetNode] * 100:F}%");
            Console.WriteLine(string.Join(" -> ", path));
        }


        private static void Dijkstra()
        {
            reliablity[startNode] = 1;
            previous[startNode] = -1;

            while (true)
            {
                var safestNodeParams = GetSafestUnvisitedNode();
                var safestNode = safestNodeParams.Key;
                var safestNodeReliability = safestNodeParams.Value;
                if (safestNode == 0 && safestNodeReliability == 0.00)
                {
                    break;
                }

                visited[safestNode] = true;

                var adjacentNodes = graph[safestNode];

                foreach (var adjacentNode in adjacentNodes)
                {
                    if (visited[adjacentNode])
                    {
                        continue;
                    }

                    var adjacentNodeReliability = reliablity[adjacentNode];
                    var betweenNodesEdge =
                        edges.Find(e => (e.FirstNode == safestNode && e.SecondNode == adjacentNode) ||
                                        (e.FirstNode == adjacentNode && e.SecondNode == safestNode));
                    if (adjacentNodeReliability < safestNodeReliability + betweenNodesEdge.Reliability)
                    {
                        reliablity[adjacentNode] = safestNodeReliability * betweenNodesEdge.Reliability;
                        previous[adjacentNode] = safestNode;
                    }
                }
            }
        }

        private static KeyValuePair<int, double> GetSafestUnvisitedNode()
        {
            int safestNode = 0;
            double maxReliability = 0.00;
            for (int currNode = 0; currNode < graph.Length; currNode++)
            {
                if (!visited[currNode])
                {
                    var currReliability = reliablity[currNode];
                    if (currReliability > maxReliability)
                    {
                        safestNode = currNode;
                        maxReliability = currReliability;
                    }
                }
            }

            return new KeyValuePair<int, double>(safestNode, maxReliability);
        }
        
        private static List<int> ReconstructPath()
        {
            var path = new List<int>();

            var prev = targetNode;
            while (prev != -1)
            {
                path.Add(prev);
                prev = previous[prev];
            }

            path.Reverse();
            return path;
        }


        private static void ReadInput()
        {
            var nodeCount = int.Parse(Console.ReadLine().Split( ' ')[1]);

            graph = new List<int>[nodeCount];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            edges = new List<Edge>();
            reliablity = new double[nodeCount];

            visited = new bool[nodeCount];
            previous = new int[nodeCount];

            var path = Console.ReadLine().Split(' ').ToArray();

            startNode = int.Parse(path[1]);
            targetNode = int.Parse(path[3]);

            var edgeCount = int.Parse(Console.ReadLine().Split( ' ')[1]);

            for (int i = 0; i < edgeCount; i++)
            {
                var input = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                var edge = new Edge(input[0], input[1], input[2] / 100.0);
                edges.Add(edge);
                graph[edge.FirstNode].Add(edge.SecondNode);
                graph[edge.SecondNode].Add(edge.FirstNode);
            }
        }
    }

    internal class Edge : IComparable<Edge>
    {
        public int FirstNode { get; set; }

        public int SecondNode { get; set; }

        public double Reliability { get; set; }

        public Edge(int firstNode, int secondNode, double reliability)
        {
            this.FirstNode = firstNode;
            this.SecondNode = secondNode;
            this.Reliability = reliability;
        }

        public int CompareTo(Edge other)
        {
            int weightCompared = this.Reliability.CompareTo(other.Reliability);
            return weightCompared;
        }

        public override string ToString()
        {
            return $"({this.FirstNode} {this.SecondNode}) -> {this.Reliability}";
        }
    }
}