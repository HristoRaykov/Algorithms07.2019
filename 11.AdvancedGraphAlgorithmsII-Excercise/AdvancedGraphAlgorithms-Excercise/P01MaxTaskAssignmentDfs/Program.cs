using System;
using System.Collections.Generic;

namespace P01MaxTaskAssignmentDfs
{
    internal class Program
    {
        // rows are tasks, cols are people
        private static int[][] graph;

        private static char[] peopleNames;

        private static bool[] visited;

        private static int[] assignedTasks;

        public static void Main(string[] args)
        {
            ReadInput();

//            for (int person = 0; person < graph[0].Length; person++)
            for (int person = graph[0].Length - 1; person >= 0; person--)
            {
                visited = new bool[graph.Length];
                FindPath(person);
            }

            var result = new List<string>();
            for (int i = 0; i < assignedTasks.Length; i++)
            {
                var personToRmove = assignedTasks[i];
                if (personToRmove != -1)
                {
                    var personName = peopleNames[personToRmove];
                    result.Add($"{personName}-{i + 1}");
                }
            }

            result.Sort();
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }

        private static bool FindPath(int person)
        {
            for (int task = 0; task < graph.Length; task++)
//            for (int task = graph.Length - 1; task >= 0; task--)
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

            for (int person = 0; person < peopleCount; person++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int task = 0; task < tasksCount; task++)
                {
                    var capacity = line[task] == 'Y' ? 1 : 0;

                    if (capacity == 1)
                    {
                        graph[task][person] = 1;
                    }
                }
            }
        }
    }
}