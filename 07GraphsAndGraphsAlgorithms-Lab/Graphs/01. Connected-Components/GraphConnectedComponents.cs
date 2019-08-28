using System;
using System.Collections.Generic;
using System.Linq;

public class GraphConnectedComponents
{
    private static List<int>[] graph;

    private static bool[] visited;

    public static void Main()
    {
        graph = ReadGraph();
        visited = new bool[graph.Length];
        FindGraphConnectedComponents();
    }

    private static List<int>[] ReadGraph()
    {
        int n = int.Parse(Console.ReadLine());
        var graph = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            graph[i] = Console.ReadLine()
                .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToList();
        }

        return graph;
    }

    private static void FindGraphConnectedComponents()
    {
        for (int node = 0; node < graph.Length; node++)
        {
            if (!visited[node])
            {
                Console.Write("Connected component:");
                DFS(node);
                Console.WriteLine();
            }
           
        }
    }

    private static void DFS(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;

            var children = graph[node];
            foreach (var child in children)
            {
                DFS(child);
            }

            Console.Write(" " + node);
        }
    }
}