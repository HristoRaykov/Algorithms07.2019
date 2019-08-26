namespace Dijkstra
{
    using System;
    using System.Collections.Generic;

    public static class DijkstraWithoutQueue
    {
        public static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode)
        {
            var numberOfNodes = graph.GetLength(0);

            var visited = new bool[numberOfNodes];
            int?[] previous = new int?[numberOfNodes];

            var distances = new int[numberOfNodes];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            distances[sourceNode] = 0;

            while (true)
            {
                var nodeParams = GetNearestUnvisitedNode(numberOfNodes, distances, visited);
                var nearestNode = nodeParams[0];
                var minDist = nodeParams[1];

                if (minDist == int.MaxValue)
                {
                    break;
                }

                visited[nearestNode] = true;

                var adjacentNodes = GetAdjacentNodes(graph, nearestNode);

                foreach (var adjacentNode in adjacentNodes)
                {
                    var currDistance = distances[adjacentNode];
                    var newDistance = distances[nearestNode] + graph[nearestNode, adjacentNode];
                    if (newDistance < currDistance)
                    {
                        distances[adjacentNode] = newDistance;
                        previous[adjacentNode] = nearestNode;
                    }
                }
            }

            var path = ReconstructShortestPath(destinationNode, distances, previous);

            return path;
        }

        private static List<int> ReconstructShortestPath(int destinationNode,  int[] distances, int?[] previous)
        {
            var path = new List<int>();

            if (distances[destinationNode] == int.MaxValue)
            {
                return null;
            }
            
            int? currNode = destinationNode;
            while (currNode != null)
            {
                path.Add(currNode.Value);
                currNode = previous[currNode.Value];
            }
            
            path.Reverse();
            return path;
        }

        private static List<int> GetAdjacentNodes(int[,] graph, int node)
        {
            var adjacentNodes = new List<int>();

            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if (graph[node, i] > 0)
                {
                    adjacentNodes.Add(i);
                }
            }

            return adjacentNodes;
        }

        private static int[] GetNearestUnvisitedNode(int numberOfNodes, int[] distances, bool[] visited)
        {
            int nearestNode = 0;
            var minDist = int.MaxValue;

            for (int currNode = 0; currNode < numberOfNodes; currNode++)
            {
                if (!visited[currNode])
                {
                    var currNodeDist = distances[currNode];
                    if (currNodeDist < minDist)
                    {
                        nearestNode = currNode;
                        minDist = currNodeDist;
                    }
                }
            }


            return new int[] {nearestNode, minDist};
        }
    }
}