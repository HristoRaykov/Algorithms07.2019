using System;
using System.Collections.Generic;
using System.Linq;

namespace P01MaximumTasksAssignment
{
    internal class Program
    {
        private static int peopleCount;

        private static int tasksCount;

        private static string[] nodes;

        private static Dictionary<int, List<int>> graph;

        private static int[] parents;

        private static Dictionary<int, int> assaignedTasks;


        public static void Main(string[] args)
        {
            ReadInput();


            var startNode = 0;
            var endNode = nodes.Length - 1;

            while (Bfs(startNode, endNode))
            {
                var person = 0;
                var task = 0;

                var currNode = endNode;
                while (currNode != startNode)
                {
                    var parent = parents[currNode];

                    if (currNode == endNode)
                    {
                        task = parent;
                    }

                    if (currNode == task)
                    {
                        person = parent;
                    }


                    currNode = parent;
                }

                RemoveNodesFromGraph(person, task);

                assaignedTasks[person] = task;
            }

            foreach (var person in assaignedTasks.Keys.OrderBy(x => x))
            {
                Console.WriteLine($"{nodes[person]}-{nodes[assaignedTasks[person]]}");
            }
        }

        private static void RemoveNodesFromGraph(int person, int task)
        {
            graph.Remove(person);
            graph.Remove(task);
            foreach (var node in graph.Keys)
            {
                var children = graph[node];
                children.Remove(person);
                children.Remove(task);
            }
        }

        private static bool Bfs(int startNode, int endNode)
        {
            var visited = new bool[nodes.Length];

            var queue = new Queue<int>();
            queue.Enqueue(startNode);
            while (queue.Count > 0)
            {
                var currNode = queue.Dequeue();

                visited[currNode] = true;

                var children = graph[currNode];
                foreach (var child in children)
                {
                    if (!visited[child])
                    {
                        parents[child] = currNode;
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

            graph = new Dictionary<int, List<int>>();

            parents = new int[nodes.Length];
            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = -1;
            }

            assaignedTasks = new Dictionary<int, int>();

            for (int node = 0; node < nodes.Length; node++)
            {
                graph[node] = new List<int>();
            }

            nodes[0] = "start";
            nodes[nodes.Length - 1] = "end";
            var startChar = 'A';


            for (int personNodeId = 1; personNodeId <= peopleCount; personNodeId++)
            {
                graph[0].Add(personNodeId);
                var nodeName = startChar.ToString();
                nodes[personNodeId] = nodeName;
                startChar++;
            }

            for (int idx = 1; idx <= tasksCount; idx++)
            {
                var taskNodeId = peopleCount + idx;
                nodes[taskNodeId] = idx.ToString();
                graph[taskNodeId].Add(nodes.Length - 1);

                var line = Console.ReadLine().ToCharArray();
                for (int personNodeId = 1; personNodeId <= peopleCount; personNodeId++)
                {
                    var capacity = line[personNodeId - 1] == 'Y' ? 1 : 0;
                    if (capacity == 1)
                    {
                        graph[personNodeId].Add(taskNodeId);
                    }
                }
            }
        }
    }
}