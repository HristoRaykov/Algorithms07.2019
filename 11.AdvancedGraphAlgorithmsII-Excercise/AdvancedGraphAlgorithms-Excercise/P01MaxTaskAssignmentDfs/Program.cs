﻿﻿using System;
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
                FindPath(person, -1);
            }

            Console.WriteLine();
        }

        private static bool FindPath(int person, int taskToAvoid)
        {
            for (int task = 0; task < graph.Length; task++)
            {
                if (graph[task][person] > 0 && task!=taskToAvoid)
                {
                    // if task is not assigned or the assigned person can take another task
                    var personAssignedToTask = assignedTasks[task];
                    if (assignedTasks[task] < 0 || FindPath(assignedTasks[task], task))
                    {
                        assignedTasks[task] = person;
                        return true;
                    }
                    
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