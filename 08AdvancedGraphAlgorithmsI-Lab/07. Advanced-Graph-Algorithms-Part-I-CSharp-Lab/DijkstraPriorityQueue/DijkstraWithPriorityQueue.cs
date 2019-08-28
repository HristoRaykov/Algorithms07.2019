using Dijkstra;

namespace DijkstraPriorityQueue
{
    using System;
    using System.Collections.Generic;

    public static class DijkstraWithPriorityQueue
    {
        public static List<int> DijkstraAlgorithm(Dictionary<Node, Dictionary<Node, int>> graph, Node sourceNode,
            Node destinationNode)
        {
            ResetDistanceFromStart(graph);

            var previous = new int?[graph.Count];
            var visited = new bool[graph.Count];


            var priorityQueue = new PriorityQueue<Node>();
            sourceNode.DistanceFromStart = 0;
            priorityQueue.Enqueue(sourceNode);
            visited[sourceNode.Id] = true;

            while (priorityQueue.Count > 0)
            {
                var currNode = priorityQueue.ExtractMin();

                var adjacents = graph[currNode];
                foreach (var kvp in adjacents)
                {
                    var adjacent = kvp.Key;
                    var distance = kvp.Value;
                    if (!visited[adjacent.Id])
                    {
                        priorityQueue.Enqueue(adjacent);
                        visited[adjacent.Id] = true;
                    }

                    // recalculate min dist to adjacent node
                    if (currNode.DistanceFromStart + distance < adjacent.DistanceFromStart)
                    {
                        adjacent.DistanceFromStart = currNode.DistanceFromStart + distance;
                        previous[adjacent.Id] = currNode.Id;
                        priorityQueue.DecreaseKey(adjacent);
                    }
                }
            }


            var path = ReconstructShortestPath(graph, destinationNode, previous);
            return path;
        }

        private static List<int> ReconstructShortestPath(Dictionary<Node, Dictionary<Node, int>> graph,
            Node destinationNode, int?[] previous)
        {
            if (double.IsInfinity(destinationNode.DistanceFromStart))
            {
                return null;
            }

            var path = new List<int>();
            int? currNodeId = destinationNode.Id;
            while (currNodeId != null)
            {
                path.Add(currNodeId.Value);
                currNodeId = previous[currNodeId.Value];
            }

            path.Reverse();
            return path;
        }

        private static void ResetDistanceFromStart(Dictionary<Node, Dictionary<Node, int>> graph)
        {
            foreach (var kvp in graph)
            {
                var node = kvp.Key;
                node.DistanceFromStart = Double.PositiveInfinity;
            }
        }
    }
}