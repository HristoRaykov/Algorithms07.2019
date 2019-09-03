using System;
using System.Collections.Generic;
using System.Linq;

public class StronglyConnectedComponents
{
    private static List<int>[] graph;

    private static List<int>[] reverseGraph;

    private static bool[] visited;

    private static Stack<int> traverseStack;

    private static List<List<int>> stronglyConnectedComponents;

    public static List<List<int>> FindStronglyConnectedComponents(List<int>[] targetGraph)
    {
        graph = targetGraph;
        reverseGraph = new List<int>[graph.Length];
        for (int i = 0; i < reverseGraph.Length; i++)
        {
            reverseGraph[i] = new List<int>();
        }

        visited = new bool[graph.Length];
        traverseStack = new Stack<int>();
        stronglyConnectedComponents = new List<List<int>>();

        CalculateReverseGraph();
        
        // populating 'traverseStack'
        for (int node = 0; node < graph.Length; node++)
        {
            if (!visited[node])
            {
                Dfs(node);
            }
        }

        visited = new bool[graph.Length];
        
        // order in traverseStack (parents on top of children) guarantees that ReverseDfs() will traverse the graph(reversedGraph)
        // from nodes defined as 'parents' from Dfs() traversal so all find paths will be two-directional
        while (traverseStack.Count > 0)
        {
            var node = traverseStack.Pop();
            if (!visited[node])
            {
                stronglyConnectedComponents.Add(new List<int>());
                ReverseDfs(node);
            }
        }

        return stronglyConnectedComponents;
    }

    private static void Dfs(int node)
    {
        if (!visited[node])
        {
            visited[node] = true;
            var children = graph[node];
            foreach (var child in children)
            {
                Dfs(child);
            }

            traverseStack.Push(node);
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
                ReverseDfs(child);
            }

            stronglyConnectedComponents.Last().Add(node);
        }
    }

    private static void CalculateReverseGraph()
    {
        for (int parent = 0; parent < graph.Length; parent++)
        {
            var children = graph[parent];
            foreach (var child in children)
            {
                reverseGraph[child].Add(parent);
            }
        }
    }
}