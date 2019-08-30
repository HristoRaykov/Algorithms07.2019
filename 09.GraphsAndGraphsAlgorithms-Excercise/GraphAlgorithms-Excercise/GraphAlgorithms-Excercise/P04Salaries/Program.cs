using System;
using System.Collections.Generic;
using System.Linq;

namespace P04Salaries
{
    internal class Program
    {
        private static int[,] adjMtrx;

        private static List<Employee> employees;


        public static void Main(string[] args)
        {
            ReadInput();

            InitializeGraph();

            var bosses = employees.Where(e => e.HasManagers == false).ToList();
            foreach (var boss in bosses)
            {
                var salary = CalcSalary(boss);
            }

            var totalSalary = employees.Sum(e => e.Salary);
            Console.WriteLine(totalSalary);
        }

        private static long CalcSalary(Employee employee)
        {
            if (employee.IsVisited)
            {
                return employee.Salary;
            }

            foreach (var subordinateId in employee.Subordinates)
            {
                var subordinate = employees.Where(e => e.Id == subordinateId).First();
                employee.Salary += CalcSalary(subordinate);
            }

            employee.IsVisited = true;

            return employee.Salary;
        }


        private static void InitializeGraph()
        {
            employees = new List<Employee>();
            for (int row = 0; row < adjMtrx.GetLength(0); row++)
            {
                var subordinates = new List<int>();
                var managers = new List<int>();
                for (int col = 0; col < adjMtrx.GetLength(1); col++)
                {
                    if (adjMtrx[row, col] == 1)
                    {
                        subordinates.Add(col);
                    }

                    if (adjMtrx[col, row]==1)
                    {
                        managers.Add(col);
                    }
                }

                var employee = new Employee(row, managers, subordinates);
                employees.Add(employee);
            }
        }

        private static void ReadInput()
        {
            int n = int.Parse(Console.ReadLine());
            adjMtrx = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                var line = Console.ReadLine().ToCharArray();
                for (int j = 0; j < line.Length; j++)
                {
                    adjMtrx[i, j] = line[j] == 'N' ? 0 : 1;
                }
            }
        }
    }

    internal class Employee
    {
        public int Id { get; }

        public List<int> Managers { get; }

        public List<int> Subordinates { get; }
        
        public long Salary { get; set; }

        public bool IsRegular { get; }

        public bool HasManagers { get; }
        
        public bool IsVisited { get; set; }

        public Employee(int id, List<int> managers, List<int> subordinates)
        {
            this.Id = id;
            this.Managers = managers;
            this.Subordinates = subordinates;
            this.HasManagers = this.Managers.Count > 0;
            this.IsRegular = this.Subordinates.Count == 0;
            if (IsRegular)
            {
                this.Salary = 1;
                this.IsVisited = true;
            }
        }
    }
}