using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Wintellect.PowerCollections;

namespace ExerIIP02FastAndFurious
{
    internal class Program
    {
        private static Dictionary<string, List<Road>> roadsNet;

        private static Dictionary<string, OrderedBag<Record>> records;

        private static Dictionary<string, Dictionary<string, TimeSpan>> fastestRoutes;

        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            
            ReadInput();

            foreach (var licenseNum in records.Keys)
            {
                var recs = records[licenseNum];
                var citiesPairArr = new string[2];
                var citiesPairs = new List<CitiesPair>();
                GetPossibleCombinations(citiesPairs, citiesPairArr, 0, recs, 0, DateTime.Now);

                foreach (var citiesPair in citiesPairs)
                {
                    var road = roadsNet[citiesPair.FirstCity]
                        .Where(r => r.GetDestination(citiesPair.FirstCity) == citiesPair.SecondCity)
                        .First();
                    if (road != null)
                    {
                        if (!fastestRoutes.ContainsKey(road.FirstCity))
                        {
                            FindFastestRoutes(road.FirstCity);
                        }
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void FindFastestRoutes(string city)
        {
            throw new NotImplementedException();
        }

        private static void GetPossibleCombinations(List<CitiesPair> citiesPairs,
            string[] citiesPair, int pairIdx, OrderedBag<Record> recs, int recIdx, DateTime startTime)
        {
            if (pairIdx == citiesPair.Length)
            {
                var lastTime = recs[recIdx - 1].Time;
                citiesPairs.Add(new CitiesPair(citiesPair[0], citiesPair[1], lastTime - startTime));
                return;
            }

            for (int i = recIdx; i < recs.Count; i++)
            {
                citiesPair[pairIdx] = recs[i].City;
                if (pairIdx == 0)
                {
                    startTime = recs[i].Time;
                }

                GetPossibleCombinations(citiesPairs, citiesPair, pairIdx + 1, recs, i + 1, startTime);
            }
        }


        private static void ReadInput()
        {
            roadsNet = new Dictionary<string, List<Road>>();

            records = new Dictionary<string, OrderedBag<Record>>();

            var line = Console.ReadLine();
            while (true)
            {
                line = Console.ReadLine();
                if (line == "Records:")
                {
                    break;
                }

                var tokens = line.Split(' ').ToArray();

                var firstCity = tokens[0];
                var secondCity = tokens[1];
                var distance = double.Parse(tokens[2]);
                var speedLimit = double.Parse(tokens[3]);

                var road = new Road(firstCity, secondCity, distance, speedLimit);

                if (!roadsNet.ContainsKey(firstCity))
                {
                    roadsNet[firstCity] = new List<Road>();
                }

                roadsNet[firstCity].Add(road);

                if (!roadsNet.ContainsKey(secondCity))
                {
                    roadsNet[secondCity] = new List<Road>();
                }

                roadsNet[secondCity].Add(road);
            }

            while (true)
            {
                line = Console.ReadLine();
                if (line == "End")
                {
                    break;
                }

                var tokens = line.Split(' ').ToArray();

                var city = tokens[0];
                var licenseNum = tokens[1];
                var time = DateTime.Parse("01/01/0001 " + tokens[2]);

                var record = new Record(city, time);

                if (!records.ContainsKey(licenseNum))
                {
                    records[licenseNum] = new OrderedBag<Record>();
                }

                records[licenseNum].Add(record);
            }
        }
    }

    internal class CitiesPair
    {
        public string FirstCity;

        public string SecondCity;

        public TimeSpan Time;

        public CitiesPair(string firstCity, string secondCity, TimeSpan time)
        {
            FirstCity = firstCity;
            SecondCity = secondCity;
            Time = time;
        }
    }

    internal class Record : IComparable<Record>
    {
        public string City { get; }

        public DateTime Time { get; }

        public Record(string city, DateTime time)
        {
            City = city;
            Time = time;
        }

        public int CompareTo(Record other)
        {
            return this.Time.CompareTo(other.Time);
        }
    }

    internal class Road
    {
        public string FirstCity { get; }

        public string SecondCity { get; }

        public double Distance { get; }

        public double SpeedLimit { get; }

        public TimeSpan MinAllowedTime { get; private set; }

        public Road(string firstCity, string secondCity, double distance, double speedLimit)
        {
            FirstCity = firstCity;
            SecondCity = secondCity;
            Distance = distance;
            SpeedLimit = speedLimit;
            MinAllowedTime = TimeSpan.FromHours(Distance / SpeedLimit);
        }

        public string GetDestination(string city)
        {
            if (FirstCity == city)
            {
                return FirstCity;
            }

            if (SecondCity == city)
            {
                return SecondCity;
            }

            return "";
        }
    }
}