using System;
using System.Collections.Generic;
using System.Linq;

namespace P01MTA
{
    internal class Program
    {
        private static int[][] graph;
        private static int[] parents;

        public static void Main()
        {
            int personCount = int.Parse(Console.ReadLine().Split()[1]);
            int taskCount = int.Parse(Console.ReadLine().Split()[1]);

            int nodes = personCount + taskCount + 2;
            graph = new int[nodes][];
            BuildGraph(personCount, taskCount, nodes);

            parents = Enumerable.Repeat(-1, nodes).ToArray();
            int start = 0;
            int end = graph.Length - 1;
            TraverseUntilMaxFlow(start, end);

            List<string> result = ReconstructSolution(personCount);

            Console.WriteLine(string.Join(Environment.NewLine, result.OrderBy(s => s)));
        }

        private static List<string> ReconstructSolution(int personCount)
        {
            List<string> result = new List<string>();

            for (int j = personCount + 1; j < graph.Length - 1; j++)
            {
                string taskAssignment = string.Empty;

                for (int i = 1; i <= personCount; i++)
                {
                    char person = (char) ('A' + i - 1);

                    if (graph[j][i] > 0)
                    {
                        taskAssignment = $"{person}-{j - personCount}";
                    }
                }

                if (taskAssignment != string.Empty)
                {
                    result.Add(taskAssignment);
                }
            }

            return result;
        }

        private static void TraverseUntilMaxFlow(int start, int end)
        {
            int flow = int.MaxValue;

            while (Bfs(start, end))
            {
                flow--;

                int current = end;

                while (current != start)
                {
                    int previous = parents[current];

                    graph[previous][current] = 0;

                    if (current != end && previous != start)
                    {
                        for (int i = 0; i < graph.Length; i++)
                        {
                            if (graph[i][current] != 0)
                            {
                                graph[i][current] = flow;
                            }
                        }
                    }

                    graph[current][previous] = flow;
                    current = previous;
                }
            }
        }

        private static bool Bfs(int start, int end)
        {
            Queue<int> nodes = new Queue<int>();
            nodes.Enqueue(start);

            bool[] visited = new bool[graph.Length];
            visited[start] = true;

            while (nodes.Count > 0)
            {
                int node = nodes.Dequeue();
                Dictionary<int, int> children = new Dictionary<int, int>();

                for (int child = 0; child < graph.Length; child++)
                {
                    if (!visited[child] && graph[node][child] != 0)
                    {
                        children.Add(child, graph[node][child]);
                    }
                }

                foreach (var currentChild in children.OrderBy(kvp => kvp.Value))
                {
                    visited[currentChild.Key] = true;
                    parents[currentChild.Key] = node;

                    if (currentChild.Key == end)
                    {
                        break;
                    }

                    nodes.Enqueue(currentChild.Key);
                }
            }

            return visited[end];
        }

        private static void BuildGraph(int personCount, int taskCount, int nodes)
        {
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new int[nodes];
            }

            for (int i = 1; i <= personCount; i++)
            {
                graph[0][i] = 1;
            }

            for (int i = personCount + 1; i < nodes - 1; i++)
            {
                graph[i][graph.Length - 1] = 1;
            }

            for (int i = 1; i <= personCount; i++)
            {
                string taskInfo = Console.ReadLine();

                for (int j = 0; j < taskCount; j++)
                {
                    if (taskInfo[j] == 'Y')
                    {
                        graph[i][j + personCount + 1] = 1;
                    }
                }
            }
        }
    }
}