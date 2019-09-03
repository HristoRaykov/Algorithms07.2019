using System;
using System.Collections.Generic;
using System.Linq;

namespace P01CableNetwork
{
    internal class Program
    {
        private static int budget;

        private static bool[] connNodes;

        private static List<Edge> connEdges = new List<Edge>();

        private static List<Edge> notConnEdges = new List<Edge>();

        public static void Main(string[] args)
        {
            ReadInput();

            var budgetLeft = budget;
            while (true)
            {
                var minEdge = FindMinEdge();
                if (minEdge == null || budgetLeft - minEdge.Weight < 0)
                {
                    break;
                }

                budgetLeft -= minEdge.Weight;
                
            }

            Console.WriteLine("Budget used: "+(budget-budgetLeft));
        }

        private static Edge FindMinEdge()
        {
            // finds min edge with one connected and one not connected node modified Prim algorithm
            for (int i = 0; i < notConnEdges.Count; i++)
            {
                var edge = notConnEdges[i];
                if ((connNodes[edge.FirstNode] && !connNodes[edge.SecondNode]) ||
                    (!connNodes[edge.FirstNode] && connNodes[edge.SecondNode]))
                {
                    notConnEdges.RemoveAt(i);
                    connEdges.Add(edge);
                    connNodes[edge.FirstNode] = true;
                    connNodes[edge.SecondNode] = true;
                    return edge;
                }
            }

            return null;
        }

        private static void ReadInput()
        {
            budget = int.Parse(Console.ReadLine().Split(' ')[1]);
            var nodeCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            var edgeCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            connNodes = new bool[nodeCount];

            for (int i = 0; i < edgeCount; i++)
            {
                var line = Console.ReadLine().Split(' ');

                if (line.Length == 3)
                {
                    var nodeParams = line.Select(int.Parse).ToArray();
                    var edge = new Edge(nodeParams[0], nodeParams[1], nodeParams[2], false);
                    notConnEdges.Add(edge);
                }
                else
                {
                    var nodeParams = line.Take(3).Select(int.Parse).ToArray();
                    var edge = new Edge(nodeParams[0], nodeParams[1], nodeParams[2], true);
                    connNodes[edge.FirstNode] = true;
                    connNodes[edge.SecondNode] = true;
                    connEdges.Add(edge);
                }
            }

            notConnEdges.Sort();
        }
    }

    class Edge : IComparable<Edge>
    {
        public int FirstNode { get; }

        public int SecondNode { get; }

        public int Weight { get; }

        public bool IsConnected { get; set; }

        public Edge(int firstNode, int secondNode, int weight, bool isConnected)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
            Weight = weight;
            IsConnected = isConnected;
        }

        public int CompareTo(Edge other)
        {
            return this.Weight.CompareTo(other.Weight);
        }
    }
}