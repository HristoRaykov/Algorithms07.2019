using System;
using System.Collections.Generic;
using System.Linq;

namespace LabIIP02Guitar
{
    internal class Program
    {
        private static int[] volChanges;

        private static int startVol;

        private static int maxVol;

        private static HashSet<int> volumes;

        public static void Main(string[] args)
        {
            ReadInput();

            if (GenerateAllFinalVolumes())
            {
                Console.WriteLine(volumes.OrderBy(x=>x).Last());
            }
            else
            {
                Console.WriteLine(-1);
            }
        }

        private static bool GenerateAllFinalVolumes()
        {
            foreach (var volChange in volChanges)
            {
                var lastVolumes = new HashSet<int>();
                foreach (var volume in volumes)
                {
                    if (volume - volChange >= 0)
                    {
                        lastVolumes.Add(volume - volChange);
                    }

                    if (volume + volChange <= maxVol)
                    {
                        lastVolumes.Add(volume + volChange);
                    }
                }

                if (lastVolumes.Count == 0)
                {
                    return false;
                }

                volumes = lastVolumes;
            }

            return true;
        }

        private static void ReadInput()
        {
            volChanges = Console.ReadLine()
                .Split(", ".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            startVol = int.Parse(Console.ReadLine());
            maxVol = int.Parse(Console.ReadLine());

            volumes = new HashSet<int>();
            volumes.Add(startVol);
        }
    }
}