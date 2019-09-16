using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace ExerIIP01Shelter
{
    internal class Program
    {
        private static List<Soldier> soldiers;

        private static List<Shelter> shelters;

        private static int shelterCapacity;

        public static void Main(string[] args)
        {
            ReadInput();

            foreach (var soldier in soldiers)
            {
                soldier.CalculateShelterDistances(shelters);
            }

            HideSoldiers();

            var maxDist = decimal.MinValue;
            foreach (var shelter in shelters)
            {
                var dist = shelter.GetMaxDistToHide();
                if (dist > maxDist)
                {
                    maxDist = dist;
                }
            }

            Console.WriteLine($"{maxDist:F6}");
        }

        private static void HideSoldiers()
        {
            foreach (var soldier in soldiers)
            {
                FindShelter(soldier);
            }
        }

        private static void FindShelter(Soldier soldier)
        {
            if (soldier.Shelter == null)
            {

                var closestShelter = soldier.GetClosestShelter();
                if (closestShelter.hasFreeSpace())
                {
                    soldier.Shelter= closestShelter;
                    soldier.VisitedShelters.Add(closestShelter.Id);
                    closestShelter.AddSoldier(soldier);
                }
                else
                {
                    var alterShelterPath = soldier.GetAlternativeShelterPath();
                    if (alterShelterPath != null)
                    {
                        var soldierToRedirect = closestShelter.FindSoldierToRedirect(alterShelterPath.Distance);
                        if (soldierToRedirect != null)
                        {
                            soldier.Shelter= closestShelter;
                            soldier.VisitedShelters.Add(closestShelter.Id);
                            closestShelter.AddSoldier(soldier);
                            
                            var shelter = soldierToRedirect.Shelter;
                            shelter.RemoveSoldier(soldierToRedirect);
                            
                            soldierToRedirect.Shelter = null;
                            
                            FindShelter(soldierToRedirect);
                        }
                    }
                }
            }
        }

        private static void ReadInput()
        {
            var line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            var soldiersCount = line[0];
            var sheltersCount = line[1];
            shelterCapacity = line[2];

            soldiers = new List<Soldier>();
            shelters = new List<Shelter>();

            for (int i = 0; i < soldiersCount; i++)
            {
                line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                var position = new Position(line[0], line[1]);
                var soldier = new Soldier(i, position);
                soldiers.Add(soldier);
            }

            for (int i = 0; i < sheltersCount; i++)
            {
                line = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                var position = new Position(line[0], line[1]);
                shelters.Add(new Shelter(i, position, shelterCapacity));
            }
        }
    }

    internal class Soldier
    {
        public int Id { get; }
        public Position Position { get; }
        public OrderedBag<Path> SheltersPaths { get; private set; }

        public Shelter Shelter { get; set; }

        public HashSet<int> VisitedShelters { get; set; }


        public Soldier(int id, Position position)
        {
            Id = id;
            Position = position;
            SheltersPaths = new OrderedBag<Path>();
            VisitedShelters = new HashSet<int>();
        }

        public void CalculateShelterDistances(List<Shelter> shelters)
        {
            foreach (var shelt in shelters)
            {
                decimal side1 = Math.Abs(this.Position.X - shelt.Position.X);
                decimal side2 = Math.Abs(this.Position.Y - shelt.Position.Y);
                decimal distance = (decimal) Math.Sqrt((double) (side1 * side1 + side2 * side2));
                var path = new Path(shelt, distance);
                SheltersPaths.Add(path);
            }
        }

        public Path GetAlternativeShelterPath()
        {
            var bestShelter = GetClosestShelter();

            for (int i = 0; i < SheltersPaths.Count-1; i++)
            {
                var currShelter = SheltersPaths[i].Shelter;
                if (currShelter.Id == bestShelter.Id)
                {
                    return SheltersPaths[i + 1];
                }
            }

            return null;
        }

        public Path GetClosestShelterPath()
        {
            foreach (var sheltersPath in SheltersPaths)
            {
                if (!VisitedShelters.Contains(sheltersPath.Shelter.Id))
                {
                    return sheltersPath;
                }
            }

            return null;
        }

        public Shelter GetClosestShelter()
        {
            var closestShelterPath = GetClosestShelterPath();
            if (closestShelterPath != null)
            {
                return closestShelterPath.Shelter;
            }

            return null;
        }

        public decimal GetShelterDistence()
        {
            for (int i = 0; i < SheltersPaths.Count; i++)
            {
                var shelterPath = SheltersPaths[i];
                if (Shelter.Id == shelterPath.Shelter.Id)
                {
                    return SheltersPaths[i].Distance;
                }
            }

            return 0;
        }
    }

    internal class Path : IComparable<Path>
    { 
        public Shelter Shelter { get; }

        public decimal Distance { get; set; }

        public Path(Shelter shelter, decimal distance)
        {
            Shelter = shelter;
            Distance = distance;
        }

        public int CompareTo(Path other)
        {
            return this.Distance.CompareTo(other.Distance);
        }
    }

    internal class Position
    {
        public int X { get; }

        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal class Shelter
    {
        public int Id { get; }
        public Position Position { get; }

        public int Capacity { get; }
        public List<Soldier> Soldiers { get; private set; }

        public Shelter(int id, Position position, int capacity)
        {
            Id = id;
            Position = position;
            Capacity = capacity;
            Soldiers = new List<Soldier>();
        }

        public void AddSoldier(Soldier soldier)
        {
            Soldiers.Add(soldier);
        }

        public bool hasFreeSpace()
        {
            return Soldiers.Count < Capacity;
        }

        public Soldier FindSoldierToRedirect(decimal distance)
        {
            var soldierWithMinRedirectDist = FindSoldierWithMinAlterPath();
            if (soldierWithMinRedirectDist!=null && soldierWithMinRedirectDist.GetAlternativeShelterPath().Distance < distance)
            {
                return soldierWithMinRedirectDist;
            }

            return null;
        }

        private Soldier FindSoldierWithMinAlterPath()
        {
            var minRedirectDist = decimal.MaxValue;
            Soldier bestSoldier = null;
            foreach (var soldier in Soldiers)
            {
                var altPath = soldier.GetClosestShelterPath();
                if (altPath!=null && altPath.Distance < minRedirectDist)
                {
                    minRedirectDist = altPath.Distance;
                    bestSoldier = soldier;
                }
            }

            return bestSoldier;
        }

        public void RemoveSoldier(Soldier soldierToRedirect)
        {
            for (int i = 0; i < Soldiers.Count; i++)
            {
                if (Soldiers[i].Id==soldierToRedirect.Id)
                {
                    Soldiers.RemoveAt(i);
                }
            }
        }

        public decimal GetMaxDistToHide()
        {
            var maxDist = decimal.MinValue;
            foreach (var soldier in Soldiers)
            {
                var dist = soldier.GetShelterDistence();
                if (dist > maxDist)
                {
                    maxDist = dist;
                }
            }

            return maxDist;
        }
    }
}
