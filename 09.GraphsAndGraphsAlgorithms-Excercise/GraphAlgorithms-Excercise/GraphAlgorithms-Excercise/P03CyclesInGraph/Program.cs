using System;
using System.Collections.Generic;

namespace P03CyclesInGraph
{
    internal class Program
    {
        private static List<string> nodeNames = new List<string>();

        private static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

        private static bool[] visited;
        
        private static HashSet<int> cyclic = new HashSet<int>();

        public static void Main(string[] args)
        {
            ReadGraph();

            for (int node = 0; node < nodeNames.Count; node++)
            {
                if (!visited[node])
                {
                    try
                    {
                        DFS(node, -1);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                }
            }

            Console.WriteLine("Acyclic: Yes");
        }

        private static void DFS(int node, int parent)
        {
            if (cyclic.Contains(node))
            {
                throw new Exception("Acyclic: No");
            }
            
            if (visited[node])
            {
                return;
            }

            visited[node] = true;
            cyclic.Add(node);
            
            var children = graph[node];
            children.Remove(parent);
            foreach (var child in children)
            {
                DFS(child, node);
            }

            cyclic.Remove(node);

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

                var nodes = line.Split('–');
                var firstNode = nodes[0];
                var secondNode = nodes[1];
                if (!nodeNames.Contains(firstNode))
                {
                    nodeNames.Add(firstNode);
                }

                if (!nodeNames.Contains(secondNode))
                {
                    nodeNames.Add(secondNode);
                }

                var firstNodeNum = nodeNames.IndexOf(firstNode);
                var secondNodeNum = nodeNames.IndexOf(secondNode);

                if (!graph.ContainsKey(firstNodeNum))
                {
                    graph[firstNodeNum] = new List<int>();
                }

                graph[firstNodeNum].Add(secondNodeNum);
                if (!graph.ContainsKey(secondNodeNum))
                {
                    graph[secondNodeNum] = new List<int>();
                }

                graph[secondNodeNum].Add(firstNodeNum);
            }

            visited = new bool[nodeNames.Count];
        }
    }
}