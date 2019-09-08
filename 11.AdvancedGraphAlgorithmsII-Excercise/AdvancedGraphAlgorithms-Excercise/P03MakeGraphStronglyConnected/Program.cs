using System;
using System.Collections.Generic;
using System.Linq;

namespace P03MakeGraphStronglyConnected
{
    internal class Program
    {
        private static List<int>[] graph;

        private static List<int>[] reverseGraph;

        private static bool[] visited;

        // strongly connected components - scc
        private static List<List<int>> scc;

        private static Stack<int> stack;


        public static void Main(string[] args)
        {
            ReadInput();

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    Dfs(node);
                }
            }

            ReverseGraph();

            visited = new bool[graph.Length];

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (!visited[node])
                {
                    scc.Add(new List<int>());
                    ReverseDfs(node);
                }
            }

            // representing one scc as one node in new graph
            var sccGraph = CreateSccGraph();

            var minEdgesToAdd = CalculateMinEdgesToAdd(sccGraph);

            Console.WriteLine("New edges needed: " + minEdgesToAdd);
        }

        private static int CalculateMinEdgesToAdd(HashSet<int>[] sccGraph)
        {
            var inDegree = new int[sccGraph.Length];
            var outDegree = new int[sccGraph.Length];

            for (int parent = 0; parent < sccGraph.Length; parent++)
            {
                var children = sccGraph[parent];
                outDegree[parent] += children.Count; 
                foreach (var child in children)
                {
                    inDegree[child]++;
                }
            }

            var noParentsNodeCount = inDegree.Count(i => i == 0);
            var noChildsNodeCount = outDegree.Count(i => i == 0);
            return Math.Max(noParentsNodeCount, noChildsNodeCount);
        }

        private static HashSet<int>[] CreateSccGraph()
        {
            var sccGraph = new HashSet<int>[scc.Count];

            for (int sccNode = 0; sccNode < scc.Count; sccNode++)
            {
                var nodes = scc[sccNode];
                var allChildren = new HashSet<int>();
                foreach (var node in nodes)
                {
                    var children = graph[node];
                    allChildren.UnionWith(children);
                }

                allChildren.ExceptWith(nodes);
                var sccNodeChildren = new HashSet<int>();
                foreach (var node in allChildren)
                {
                    var sccNodeId = GetSccNodeId(node);
                    sccNodeChildren.Add(sccNodeId);
                }

                sccGraph[sccNode] = sccNodeChildren;
            }

            return sccGraph;
        }

        private static int GetSccNodeId(int node)
        {
            for (int i = 0; i < scc.Count; i++)
            {
                var nodes = scc[i];
                if (nodes.Contains(node))
                {
                    return i;
                }
            }

            return -1;
        }

        private static void Dfs(int node)
        {
            if (!visited[node])
            {
                visited[node] = true;
                var children = graph[node];
                foreach (var child in children)
                {
                    if (!visited[child])
                    {
                        Dfs(child);
                    }
                }

                stack.Push(node);
            }
        }


        private static void ReverseDfs(int node)
        {
            if (!visited[node])
            {
                visited[node] = true;
                var children = reverseGraph[node];
                foreach (var child in children)
                {
                    if (!visited[child])
                    {
                        ReverseDfs(child);
                    }
                }

                scc.Last().Add(node);
            }
        }

        private static void ReverseGraph()
        {
            for (int parent = 0; parent < reverseGraph.Length; parent++)
            {
                foreach (var child in graph[parent])
                {
                    reverseGraph[child].Add(parent);
                }
            }
        }


        private static void ReadInput()
        {
            var nodesCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            var edgesCount = int.Parse(Console.ReadLine().Split(' ')[1]);

            graph = new List<int>[nodesCount];
            reverseGraph = new List<int>[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                graph[i] = new List<int>();
                reverseGraph[i] = new List<int>();
            }

            visited = new bool[nodesCount];
            scc = new List<List<int>>();
            stack = new Stack<int>();

            for (int edge = 0; edge < edgesCount; edge++)
            {
                var line = Console.ReadLine()
                    .Split(" -> ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                var parent = line[0];
                var child = line[1];
                graph[parent].Add(child);
            }
        }
    }
}