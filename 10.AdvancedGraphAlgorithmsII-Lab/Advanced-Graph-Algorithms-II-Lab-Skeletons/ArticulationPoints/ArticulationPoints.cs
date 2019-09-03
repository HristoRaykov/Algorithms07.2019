using System;
using System.Collections.Generic;

public class ArticulationPoints
{
    private static List<int>[] graph;

    private static bool[] visited;

    private static int[] parents;

    private static int[] lowpoints;

    private static int[] depths;

    private static List<int> articulationPoints;


    public static List<int> FindArticulationPoints(List<int>[] targetGraph)
    {
        graph = targetGraph;
        visited = new bool[graph.Length];
        parents = new int[graph.Length];
        for (int i = 0; i < parents.Length; i++)
        {
            parents[i] = -1;
        }

        lowpoints = new int[graph.Length];
        depths = new int[graph.Length];
        articulationPoints = new List<int>();

        for (int node = 0; node < graph.Length; node++)
        {
            if (!visited[node])
            {
                FAP(node, 1);
            }
        }

        return articulationPoints;
    }

    public static void FAP(int node, int depth)
    {
        if (!visited[node])
        {
            visited[node] = true;
            depths[node] = depth;
            lowpoints[node] = depth;

            int childCount = 0;
            bool isArticulation = false;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    parents[child] = node;
                    FAP(child, depth + 1);
                    childCount++;

                    if (lowpoints[child] >= depths[node])
                    {
                        isArticulation = true;
                    }

                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[child]);
                }
                else if (child != parents[node])
                {
                    lowpoints[node] = Math.Min(lowpoints[node], depths[child]);
                }
            }

            if ((isArticulation && parents[node] != -1) || (parents[node] == -1 && childCount > 1))
            {
                articulationPoints.Add(node);
            }
        }
    }
}