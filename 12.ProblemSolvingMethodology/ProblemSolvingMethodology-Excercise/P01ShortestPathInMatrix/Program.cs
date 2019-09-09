using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace P01ShortestPathInMatrix
{
    internal class Program
    {
        private static int[][] matrix;

        // access nodeId by node coordinates in matrix
        private static int[][] nodeIds;

        //access nodeValues by nodeId to avoid searching in matrix every time
        private static int[] nodeValues;

        private static List<Edge>[] graph;

        private static bool[] visited;

        private static int[] distances;

        private static int[] prev;


        public static void Main(string[] args)
        {
            ReadInput();

            InitGraph();

            Dijkstra(0);

            var path = ReconstructPath(nodeValues.Length - 1);
            Console.WriteLine("Length: " + distances[nodeValues.Length - 1]);
            Console.WriteLine("Path: " + string.Join(" ", path));
        }

        private static List<int> ReconstructPath(int endNode)
        {
            var path = new List<int>();

            var currNode = endNode;
            while (currNode != -1)
            {
                path.Add(nodeValues[currNode]);
                
                currNode = prev[currNode];
            }

            path.Reverse();
            return path;
        }

        private static void Dijkstra(int startNode)
        {
            visited = new bool[graph.Length];
            distances = new int[graph.Length];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            distances[startNode] = nodeValues[startNode];
            prev = new int[graph.Length];
            for (int i = 0; i < prev.Length; i++)
            {
                prev[i] = -1;
            }

            var priorQueue = new OrderedBag<int>(Comparer<int>.Create((x, y) => distances[x] - distances[y]));

            priorQueue.Add(startNode);

            while (priorQueue.Count > 0)
            {
                var node = priorQueue.RemoveFirst();

                if (distances[node] == int.MaxValue)
                {
                    break;
                }

                var childEdges = graph[node];
                foreach (var edge in childEdges)
                {
                    var child = edge.Child;
                    if (!visited[child])
                    {
                        priorQueue.Add(child);
                        visited[child] = true;
                    }

                    var newDistance = distances[node] + edge.Distance;
                    if (newDistance <= distances[child])
                    {
                        prev[child] = node;
                        distances[child] = newDistance;
                        priorQueue = new OrderedBag<int>(priorQueue, (x, y) => distances[x] - distances[y]);
                    }
                }
            }
        }

        private static void InitGraph()
        {
            graph = new List<Edge>[nodeValues.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<Edge>();
            }

            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[0].Length; col++)
                {
                    var currNode = nodeIds[row][col];

                    if (IsInMatrix(row, col + 1))
                    {
                        var child = nodeIds[row][col + 1];
                        var distance = matrix[row][col + 1];
                        var edge = new Edge(currNode, child, distance);
                        graph[currNode].Add(edge);
                    }

                    if (IsInMatrix(row - 1, col))
                    {
                        var child = nodeIds[row - 1][col];
                        var distance = matrix[row - 1][col];
                        var edge = new Edge(currNode, child, distance);
                        graph[currNode].Add(edge);
                    }

                    if (IsInMatrix(row, col - 1))
                    {
                        var child = nodeIds[row][col - 1];
                        var distance = matrix[row][col - 1];
                        var edge = new Edge(currNode, child, distance);
                        graph[currNode].Add(edge);
                    }

                    if (IsInMatrix(row + 1, col))
                    {
                        var child = nodeIds[row + 1][col];
                        var distance = matrix[row + 1][col];
                        var edge = new Edge(currNode, child, distance);
                        graph[currNode].Add(edge);
                    }
                }
            }
        }

        private static bool IsInMatrix(int row, int col)
        {
            var rows = matrix.Length;
            var cols = matrix[1].Length;
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }

        private static void ReadInput()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            matrix = new int[rows][];
            nodeIds = new int[rows][];
            nodeValues = new int[rows * cols];

            var nodeId = 0;
            for (int row = 0; row < rows; row++)
            {
                var inputValues = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();
                matrix[row] = inputValues;
                nodeIds[row] = new int[cols];
                for (int col = 0; col < cols; col++)
                {
                    nodeValues[nodeId] = matrix[row][col];
                    nodeIds[row][col] = nodeId++;
                }
            }
        }
    }

    internal class Edge
    {
        public int Parent { get; }

        public int Child { get; }

        public int Distance { get; }

        public Edge(int parent, int child, int distance)
        {
            Parent = parent;
            Child = child;
            Distance = distance;
        }
    }
}