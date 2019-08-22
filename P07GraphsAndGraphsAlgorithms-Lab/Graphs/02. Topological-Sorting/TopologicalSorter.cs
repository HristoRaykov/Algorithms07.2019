using System;
using System.Collections.Generic;
using System.Linq;

public class TopologicalSorter
{
    private Dictionary<string, List<string>> graph;

    private Dictionary<string, int> parentCounts;

    public TopologicalSorter(Dictionary<string, List<string>> graph)
    {
        this.graph = graph;
        parentCounts = new Dictionary<string, int>();
    }

    public ICollection<string> TopSort()
    {
        var topSorted = new List<string>();

        CalculateParentsCount();

        while (true)
        {
            var nodeToRemove = parentCounts.Keys
                .FirstOrDefault(node => parentCounts[node] == 0);

            if (nodeToRemove == null)
            {
                break;
            }

            topSorted.Add(nodeToRemove);
            RecalculateParentsCount(nodeToRemove);
            graph.Remove(nodeToRemove);
        }

        if (parentCounts.Count > 0)
        {
            throw new InvalidOperationException();
        }

        return topSorted;
    }

    private void RecalculateParentsCount(string nodeToRemove)
    {
        foreach (var child in graph[nodeToRemove])
        {
            if (parentCounts.ContainsKey(child))
            {
                parentCounts[child]--;
            }
        }

        parentCounts.Remove(nodeToRemove);
    }

    private void CalculateParentsCount()
    {
        foreach (var parent in graph.Keys)
        {
            if (!parentCounts.ContainsKey(parent))
            {
                parentCounts[parent] = 0;
            }

            var children = graph[parent];
            foreach (var child in children)
            {
                if (!parentCounts.ContainsKey(child))
                {
                    parentCounts[child] = 0;
                }

                parentCounts[child]++;
            }
        }
    }
}