using System;
using System.Collections.Generic;
using System.Linq;

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

            var flow = int.MaxValue;

            while (Bfs(startNode, endNode))
            {
                flow--;

                var currNode = endNode;
                while (currNode != startNode)
                {
                    var parent = parents[currNode];

                    graph[parent][currNode] = 0;

                    if (currNode != endNode && parent != startNode)
                    {
                        for (int i = 0; i < graph.Length; i++)
                        {
                            if (graph[i][currNode] != 0)
                            {
                                graph[i][currNode] = flow;
                            }
                        }
                    }

                    graph[currNode][parent] = flow;

                    currNode = parent;
                }
            }

            var assaignedTasks = new List<string>();


            for (int person = 1; person <= peopleCount; person++)
            {
                var assignedTask = "";
                for (int task = 1; task <= tasksCount; task++)
                {
                    var taskId = peopleCount + task;
                    if (graph[taskId][person] > 0)
                    {
                        assignedTask = $"{nodes[person]}-{nodes[taskId]}";
                    }
                }

                if (assignedTask != "")
                {
                    assaignedTasks.Add(assignedTask);
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

                Dictionary<int, int> children = new Dictionary<int, int>();

                for (int child = 0; child < graph.Length; child++)
                {
                    if (graph[currNode][child] != 0 && !visited[child])
                    {
                        children.Add(child, graph[currNode][child]);
                    }
                }

                foreach (var childNode in children.OrderBy(x => x.Value))
                {
                    parents[childNode.Key] = currNode;
                    visited[childNode.Key] = true;
                    if (childNode.Key == endNode)
                    {
                        break;
                    }

                    queue.Enqueue(childNode.Key);
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

            for (int personNodeId = 1; personNodeId <= peopleCount; personNodeId++)
            {
                
                var line = Console.ReadLine().ToCharArray();
                for (int idx = 0; idx < tasksCount; idx++)
                {
                    var taskNodeId = peopleCount + idx +1;
                    nodes[taskNodeId] = (idx+1).ToString();
                    graph[taskNodeId][nodes.Length - 1] = 1;

                    
                    var capacity = line[idx] == 'Y' ? 1 : 0;
                    if (capacity == 1)
                    {
                        graph[personNodeId][taskNodeId] = 1;
                    }
                }
            }
        }
    }
}