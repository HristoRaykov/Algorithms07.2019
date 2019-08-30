using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace P05BreakCycles
{
    internal class Program
    {
        private static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

        private static List<Edge> edges = new List<Edge>();

        private static List<Edge> removed = new List<Edge>();
        
        private static HashSet<string> cyclic;

        public static void Main(string[] args)
        {
            ReadGraph();

            InitEdges();

            HandleDuplicateEdges();

            foreach (var edge in edges)
            {
                var closesCycle = DoesClosesCycle(edge);
                if (closesCycle)
                {
                    removed.Add(edge);
                }
            }

            Console.WriteLine("Edges to remove: " + removed.Count);
            foreach (var edge in removed)
            {
                Console.WriteLine(edge);
            }
            
            
        }

        private static bool DoesClosesCycle(Edge edge)
        {
            var lexSmaller = edge.GetLexSmallerNode();
            var lexBigger = edge.GetLexBiggerNode();

            graph[lexSmaller].Remove(lexBigger);
            graph[lexBigger].Remove(lexSmaller);
            
            cyclic = new HashSet<string>();
            bool hasPath = SearchPath(lexSmaller, cyclic, lexBigger);
            if (hasPath)
            {
                return true;
            }

            graph[lexSmaller].Add(lexBigger);
            graph[lexBigger].Add(lexSmaller);

            return false;
        }

        private static bool SearchPath(string node, HashSet<string> cyclic, string targetNode)
        {
            if (node == targetNode)
            {
                return true;
            }

            cyclic.Add(node);
            var adjacents = graph[node];
            foreach (var adjacent in adjacents)
            {
                if (!cyclic.Contains(adjacent))
                {
                    var hasPath = SearchPath(adjacent, cyclic, targetNode);
                    if (hasPath)
                    {
                        return true;
                    }
                }
            }

            cyclic.Remove(node);

            return false;
        }


        private static void HandleDuplicateEdges()
        {
            var duplicates = edges
                .GroupBy(e => e.ToString())
                .Where(g => g.Count() > 1)
                .ToDictionary(x => x.Key, y => y.Count());

            foreach (var duplicate in duplicates.Keys)
            {
                for (int i = 0; i < duplicates[duplicate]-1; i++)
                {
                   var edgesStr = edges.Select(e => e.ToString()).ToList();
                   var idx = edgesStr.IndexOf(duplicate);
                   var edgeToRemove = edges[idx];
                   graph[edgeToRemove.FirstNode].Remove(edgeToRemove.SecondNode);
                   graph[edgeToRemove.SecondNode].Remove(edgeToRemove.FirstNode);
                   removed.Add(edgeToRemove);
                   edges.RemoveAt(idx);
                }
            }
        }
        
        private static void InitEdges()
        {
            var notToAdd = new List<string>();
            foreach (var parent in graph.Keys)
            {
                foreach (var child in graph[parent])
                {
                    if (!notToAdd.Contains($"{parent} - {child}"))
                    {
                        var edge = new Edge(parent, child);
                        edges.Add(edge);
                        notToAdd.Add($"{child} - {parent}");
                    }
                }
            }
            
            edges.Sort((e1,e2) => e1.ToString().CompareTo(e2.ToString()));
        }

        private static void ReadGraph()
        {
            string line;
            while (true)
            {
                line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                var nodes = line.Split("->".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var parent = nodes[0].Trim();
                var children = nodes[1].Trim().Split(' ').ToList();

                if (!graph.ContainsKey(parent))
                {
                    graph[parent] = new List<string>();
                }

                graph[parent] = children;
            }
        }
    }

    internal class Edge : IComparable<Edge>
    {
        public string FirstNode { get; }
        public string SecondNode { get; }

        public Edge(string firstNode, string secondNode)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
        }

        public string GetLexSmallerNode()
        {
            if (FirstNode.CompareTo(SecondNode) <= 0)
            {
                return FirstNode;
            }
            else
            {
                return SecondNode;
            }
        }

        public string GetLexBiggerNode()
        {
            if (FirstNode.CompareTo(SecondNode) <= 0)
            {
                return SecondNode;
            }
            else
            {
                return FirstNode;
            }
        }


        public int CompareTo(Edge other)
        {
            return this.ToString().CompareTo(other.ToString());
        }

        public override string ToString()
        {
            var nodes = new List<string>() {FirstNode, SecondNode};
            nodes.Sort();
            return $"{nodes[0]} - {nodes[1]}";
        }
    }
}