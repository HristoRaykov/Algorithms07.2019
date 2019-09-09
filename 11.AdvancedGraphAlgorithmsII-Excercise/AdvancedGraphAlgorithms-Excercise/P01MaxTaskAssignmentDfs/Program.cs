using System;
using System.Collections.Generic;

namespace P01MaxTaskAssignmentDfs
{
    internal class Program
    {
        private static int[][] graph;

        private static char[] peopleNames;

        private static bool[] visited;

        private static int[] assignedTasks;

        public static void Main(string[] args)
        {
            ReadInput();

            for (int person = 0; person < graph[0].Length; person++)
            {
                visited = new bool[graph.Length];
                FindPath(person);
            }

            var result = new List<string>();
            for (int i = 0; i < assignedTasks.Length; i++)
            {
                result.Add($"{peopleNames[assignedTasks[i]]}-{i+1}");
            }
            result.Sort();
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static bool FindPath(int person)
        {
            for (int task = 0; task < graph.Length; task++)
            {
                if (graph[task][person] > 0 && !visited[task])
                {
                    visited[task] = true;
                    // if task is not assigned or the assigned person can take another task
                    if (assignedTasks[task] < 0)
                    {
                        assignedTasks[task] = person;

                        return true;
                    }

                    var hasPath = FindPath(assignedTasks[task]);
                    if (hasPath)
                    {
                        assignedTasks[task] = person;

                        return true;
                    }

                    Console.WriteLine();
                }
            }

            return false;
        }


        private static void ReadInput()
        {
            var peopleCount = int.Parse(Console.ReadLine().Split(' ')[1]);
            var tasksCount = int.Parse(Console.ReadLine().Split(' ')[1]);

            graph = new int[tasksCount][];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new int[peopleCount];
            }

            peopleNames = new char[peopleCount];

            visited = new bool[tasksCount];

            assignedTasks = new int[tasksCount];

            for (int i = 0; i < tasksCount; i++)
            {
                assignedTasks[i] = -1;
            }

            var startChar = 'A';

            for (int personNodeId = 0; personNodeId < peopleCount; personNodeId++)
            {
                peopleNames[personNodeId] = startChar;
                startChar++;
            }

            for (int task = 0; task < tasksCount; task++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int person = 0; person < line.Length; person++)
                {
                    var capacity = line[person] == 'Y' ? 1 : 0;

                    if (capacity == 1)
                    {
                        graph[task][person] = 1;
                    }
                }
            }
        }
    }
}