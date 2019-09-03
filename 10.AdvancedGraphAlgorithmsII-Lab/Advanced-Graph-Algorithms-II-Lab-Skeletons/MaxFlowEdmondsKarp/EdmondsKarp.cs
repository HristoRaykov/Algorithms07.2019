using System;
using System.Collections.Generic;
using System.IO;

public class EdmondsKarp
{
    private static int[][] graph;

    private static int[] parents;

    public static int FindMaxFlow(int[][] targetGraph)
    {
        graph = targetGraph;
        parents = new int[graph[0].Length];
        for (int i = 0; i < parents.Length; i++)
        {
            parents[i] = -1;
        }

        var startNode = 0;
        var endNode = graph[0].Length - 1;
        var maxFlow = 0;


        while (Bfs(startNode, endNode))
        {
            var minPathCapacity = CalculateMinPathCapacity(startNode, endNode);

            maxFlow += minPathCapacity;

            RecalculateCapacity(startNode, endNode, minPathCapacity);
        }

        return maxFlow;
    }

    private static bool Bfs(int startNode, int endNode)
    {
        var visited = new bool[graph.Length];
        
        var queue = new Queue<int>();
        queue.Enqueue(startNode);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            visited[node] = true;


            for (int child = 0; child < graph[0].Length; child++)
            {
                if (graph[node][child] != 0 && !visited[child])
                {
                    parents[child] = node;
                    queue.Enqueue(child);
                }
            }
        }

        return visited[endNode];
    }

    private static void RecalculateCapacity(int startNode, int endNode, int minPathCapacity)
    {
        var currNode = endNode;
        while (currNode != startNode)
        {
            var parent = parents[currNode];
            graph[parent][currNode] -= minPathCapacity;
            graph[currNode][parent] += minPathCapacity;

            currNode = parent;
        }
    }

    private static int CalculateMinPathCapacity(int startNode, int endNode)
    {
        var minPathFlow = int.MaxValue;
        int currNode = endNode;
        while (currNode != startNode)
        {
            int parent = parents[currNode];

            var currEdgeCapacity = graph[parent][currNode];
            if (currEdgeCapacity < minPathFlow)
            {
                minPathFlow = currEdgeCapacity;
            }


            currNode = parent;
        }

        return minPathFlow;
    }
}