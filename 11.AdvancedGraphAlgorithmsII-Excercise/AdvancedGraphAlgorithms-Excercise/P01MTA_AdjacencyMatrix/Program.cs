using System;
using System.Collections.Generic;

namespace P01MTA_AdjacencyMatrix
{
    internal class Program
    {
        private static int peopleCount;

        private static int tasksCount;

        private static string[] nodes;

        private static int[][] graph;

        private static int[] parents;


        public static void Main(string[] args)
        {
            ReadInput();

            var startNode = 0;
            var endNode = graph.Length - 1;

            while (Bfs(startNode, endNode))
            {
                var currNode = endNode;
                while (currNode != startNode)
                {
                    var parent = parents[currNode];
                    // decrease capacity 
                    graph[parent][currNode] = 0;
                    // increase flow
                    graph[currNode][parent] = 1;

                    currNode = parent;
                }
            }

            var assaignedTasks = new List<string>();


            for (int person = 1; person <= peopleCount; person++)
            {
                for (int task = 1; task <= tasksCount; task++)
                {
                    var taskId = peopleCount + task;
                    var flow = graph[taskId][person];
                    if (flow > 0)
                    {
                        assaignedTasks.Add($"{nodes[person]}-{nodes[taskId]}");
                    }
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, assaignedTasks));
        }

        private static bool Bfs(int startNode, int endNode)
        {
            var visited = new bool[graph.Length];

            var queue = new Queue<int>();
            queue.Enqueue(startNode);
            visited[startNode] = true;
            while (queue.Count > 0)
            {
                var currNode = queue.Dequeue();

                for (int child = 0; child < graph.Length; child++)
                {
                    if (graph[currNode][child] > 0 && !visited[child])
                    {
                        parents[child] = currNode;
                        visited[child] = true;
                        queue.Enqueue(child);
                    }
                }
            }

            return visited[endNode];
        }

        private static void ReadInput()
        {
            peopleCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            tasksCount = int.Parse(Console.ReadLine().Split(' ')[1]);

            nodes = new string[peopleCount + tasksCount + 2];

            graph = new int[nodes.Length][];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new int[graph.Length];
            }

            parents = new int[nodes.Length];
            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = -1;
            }

            nodes[0] = "start";
            nodes[nodes.Length - 1] = "end";
            var startChar = 'A';

            for (int personNodeId = 1; personNodeId <= peopleCount; personNodeId++)
            {
                var nodeName = startChar.ToString();
                nodes[personNodeId] = nodeName;
                startChar++;

                graph[0][personNodeId] = 1;
            }

            for (int idx = 1; idx <= tasksCount; idx++)
            {
                var taskNodeId = peopleCount + idx;
                nodes[taskNodeId] = idx.ToString();
                graph[taskNodeId][nodes.Length - 1] = 1;

                var line = Console.ReadLine().ToCharArray();
                for (int personNodeId = 1; personNodeId <= line.Length; personNodeId++)
                {
                    var capacity = line[personNodeId - 1] == 'Y' ? 1 : 0;
                    if (capacity == 1)
                    {
                        graph[personNodeId][taskNodeId] = 1;
                    }
                }
            }
        }
    }
}