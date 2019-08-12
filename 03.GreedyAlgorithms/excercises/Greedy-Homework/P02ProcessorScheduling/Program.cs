using System;
using System.Collections.Generic;
using System.Linq;

namespace P02ProcessorScheduling
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var taskNumber = int.Parse(Console.ReadLine().Substring(7));
            var allTasks = new List<Task>();
            for (int i = 0; i < taskNumber; i++)
            {
                var input = Console.ReadLine().Split(" - ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                allTasks.Add(new Task(int.Parse(input[0]), int.Parse(input[1]), i + 1));
            }

            List<Task> schedTasks = CalculateTasks(allTasks);
            schedTasks.Sort((t1, t2) =>
            {
                if (t1.Deadline == t2.Deadline)
                {
                    return t2.Value - t1.Value;
                }
                else
                {
                    return t1.Deadline - t2.Deadline;
                }
            });
            var totalValue = schedTasks.Select(t => t.Value).Sum();
            Console.WriteLine($"Optimal schedule: {string.Join(" -> ", schedTasks.Select(t => t.TaskNumber))}\n" +
                              $"Total value: {totalValue}");
        }

        private static List<Task> CalculateTasks(List<Task> allTasks)
        {
            var schedTasks = new List<Task>();
            allTasks.Sort((t1, t2) =>
            {
                if (t1.Value == t2.Value)
                {
                    return t1.Deadline - t2.Deadline;
                }
                else
                {
                    return t2.Value - t1.Value;
                }
            });

            var taskNumber = 1;
            var maxTasks = allTasks.Select(t => t.Deadline).Max();

            while (taskNumber <= maxTasks)
            {
                var idx = 0;
                while (idx < allTasks.Count)
                {
                    var currTask = allTasks[idx];
                    if (currTask.Deadline > maxTasks)
                    {
                        idx++;
                    }
                    else
                    {
                        schedTasks.Add(currTask);
                        allTasks.Remove(currTask);
                        break;
                    }
                }

                taskNumber++;
            }

            return schedTasks;
        }
    }

    class Task
    {
        public int Value { get; }

        public int Deadline { get; }

        public int TaskNumber { get; }

        public Task(int value, int deadline, int taskNumber)
        {
            Value = value;
            Deadline = deadline;
            TaskNumber = taskNumber;
        }
    }
}