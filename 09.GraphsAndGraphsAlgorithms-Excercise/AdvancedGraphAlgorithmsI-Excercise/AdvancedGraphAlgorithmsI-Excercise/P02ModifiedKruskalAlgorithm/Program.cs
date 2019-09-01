using System;
using System.Collections.Generic;
using System.Linq;

namespace P02ModifiedKruskalAlgorithm
{
    internal class Program
    {
        private static List<Edge> edges = new List<Edge>();


        public static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            int edgesCount = int.Parse(Console.ReadLine().Split(' ')[1]);

            for (int i = 0; i < edgesCount; i++)
            {
                var line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                var edge = new Edge(line[0], line[1], line[2]);
                edges.Add(edge);
            }


            var minimumSpanningForest = KruskalAlgorithm.Kruskal(nodesCount, edges);

            Console.WriteLine("Minimum spanning forest weight: " +
                              minimumSpanningForest.Sum(e => e.Weight));

//            foreach (var edge in minimumSpanningForest)
//            {
//                Console.WriteLine(edge);
//            }
        }
    }

    internal class KruskalAlgorithm
    {
        public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
        {
            var spanTree = new List<Edge>();

            var parents = new int[numberOfVertices + 1];
            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = i;
            }

            var sortedEdges = edges.OrderBy(e => e.Weight);

            foreach (var edge in sortedEdges)
            {
                var startNode = edge.StartNode;
                var endNode = edge.EndNode;

                var startNodeRoot = FindRoot(startNode, parents);
                var endNodeRoot = FindRoot(endNode, parents);

                if (startNodeRoot != endNodeRoot)
                {
                    spanTree.Add(edge);
                    parents[startNodeRoot] = endNodeRoot;
                }
            }

            return spanTree;
        }

        public static int FindRoot(int node, int[] parent)
        {
            // finding root Node
            var root = node;
            while (parent[root] != root)
            {
                root = parent[root];
            }

            // path compression setting founded root to be parent of all node we move through
            while (root != node)
            {
                var oldParent = parent[node];
                parent[node] = root;
                node = oldParent;
            }

            return root;
        }
    }

    internal class Edge : IComparable<Edge>
    {
        public Edge(int startNode, int endNode, int weight)
        {
            this.StartNode = startNode;
            this.EndNode = endNode;
            this.Weight = weight;
        }

        public int StartNode { get; set; }

        public int EndNode { get; set; }

        public int Weight { get; set; }

        public int CompareTo(Edge other)
        {
            int weightCompared = this.Weight.CompareTo(other.Weight);
            return weightCompared;
        }

        public override string ToString()
        {
            return $"({this.StartNode} {this.EndNode}) -> {this.Weight}";
        }
    }
}