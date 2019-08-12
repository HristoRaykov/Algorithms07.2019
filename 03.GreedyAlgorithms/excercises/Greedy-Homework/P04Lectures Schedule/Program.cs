using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace P04Lectures_Schedule
{
    internal class Program
    {
        private static IList<Lecture> lectures = new List<Lecture>();
        
        public static void Main(string[] args)
        {
            int lecCount = int.Parse(Console.ReadLine().Substring(10));
            for (int i = 0; i < lecCount; i++)
            {
                var input = Console.ReadLine().Split(':');
                var name = input[0].Trim();
                var times = input[1].Trim().Split(" - ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var lecture = new Lecture(name, int.Parse(times[0]), int.Parse(times[1]));
                lectures.Add(lecture);
            }

            var bestLecs = GetBestLectures();

            PrintLectures(bestLecs);
        }

        private static IList<Lecture> GetBestLectures()
        {
            lectures = lectures.OrderBy(l => l.Finish).ToList();
            var bestLecs = new List<Lecture>();
            
            while (lectures.Count>0)
            {
                var lecture = lectures[0];
                bestLecs.Add(lecture);
                lectures = lectures.Where(l => l.Start >= lecture.Finish).ToList();
            }

            return bestLecs;
        }
        
        private static void PrintLectures(IList<Lecture> bestLecs)
        {
            Console.WriteLine($"Lectures ({bestLecs.Count}):");
            foreach (var lec in bestLecs)
            {
                Console.WriteLine(lec);
            }
        }
    }

    class Lecture
    {
        public string Name { get; }

        public int Start { get; }

        public int Finish { get; }

        public Lecture(string name, int start, int finish)
        {
            Name = name;
            Start = start;
            Finish = finish;
        }

        public override string ToString()
        {
           return $"{this.Start}-{this.Finish} -> {this.Name}";
        }
    }
}