using System.Collections.Generic;
using System.Linq;

namespace Kurskal
{
    public class KruskalAlgorithm
    {
        public static List<Edge> Kruskal(int numberOfVertices, List<Edge> edges)
        {
            var spanTree = new List<Edge>();

            var parents = new int[numberOfVertices + 1];
            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = i;
            }

            var sortedEdges = edges.OrderBy(e => e.Weight);

            foreach (var edge in sortedEdges)
            {
                var startNode = edge.StartNode;
                var endNode = edge.EndNode;

                var startNodeRoot = FindRoot(startNode, parents);
                var endNodeRoot = FindRoot(endNode, parents);

                if (startNodeRoot != endNodeRoot)
                {
                    spanTree.Add(edge);
                    parents[startNodeRoot] = endNodeRoot;
                }
            }

            return spanTree;
        }

        public static int FindRoot(int node, int[] parent)
        {
            // finding root Node
            var root = node;
            while (parent[root] != root)
            {
                root = parent[root];
            }

            // path compression setting founded root to be parent of all node we move through
            while (root != node)
            {
                var oldParent = parent[node];
                parent[node] = root;
                node = oldParent;
            }

            return root;
        }
    }
}