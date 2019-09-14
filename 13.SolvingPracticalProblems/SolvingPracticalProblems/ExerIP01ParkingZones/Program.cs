using System;
using System.Collections.Generic;
using System.Linq;


namespace ExerIP01ParkingZones
{
    internal class Program
    {
        private static List<ParkZone> parkZones;

        public static void Main(string[] args)
        {
            ReadInput();

            var freeParkSpots = new SortedSet<Spot>();

            foreach (var parkZone in parkZones)
            {
                foreach (var parkSpot in parkZone.freeParkSpots)
                {
                    freeParkSpots.Add(parkSpot);
                }
            }

            var bestParkSpot = freeParkSpots.First();

            Console.WriteLine(
                $"Zone Type: {bestParkSpot.ParkZone.Name}; X: {bestParkSpot.X}; Y: {bestParkSpot.Y}; Price: {bestParkSpot.PriceForStay:F}");

            Console.WriteLine();
        }

        private static void ReadInput()
        {
            var numOfParkZones = int.Parse(Console.ReadLine());
            parkZones = new List<ParkZone>();
            var parkZoneParams = new List<string>();

            for (int i = 0; i < numOfParkZones; i++)
            {
                parkZoneParams.Add(Console.ReadLine());
            }

            var freeParkSpots = Console.ReadLine()
                .Split(';')
                .ToArray();

            var targetCoord = Console.ReadLine()
                .Split(", ".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var target = new Spot(targetCoord[0], targetCoord[1]);
            var timePerSpot = int.Parse(Console.ReadLine());

            for (int i = 0; i < parkZoneParams.Count; i++)
            {
                var line = parkZoneParams[i].Split(':').ToArray();

                var parkZoneName = line[0].Trim();

                var parameters = line[1]
                    .Trim()
                    .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var coordinates = parameters.Take(4).Select(int.Parse).ToArray();
                var price = decimal.Parse(parameters[4]);
                var parkZone = new ParkZone(parkZoneName, coordinates[0], coordinates[1], coordinates[2],
                    coordinates[3], price, target, timePerSpot);
                parkZones.Add(parkZone);
            }

            foreach (var parkSpotStr in freeParkSpots)
            {
                var parkSpotCoord = parkSpotStr
                    .Split(", ".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var x = parkSpotCoord[0];
                var y = parkSpotCoord[1];
                foreach (var parkZone in parkZones)
                {
                    if (parkZone.isInParkZone(x, y))
                    {
                        var parkSpot = new Spot(x, y, parkZone);
                        break;
                    }
                }
            }
        }
    }

    internal class ParkZone
    {
        public string Name { get; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Hight { get; }
        public decimal Price { get; }

        public int TimePerSpotInSec { get; }

        public Spot Target { get; }
        public List<Spot> freeParkSpots { get; }

        public ParkZone(string name, int x, int y, int width, int hight, decimal price, Spot target,
            int timePerSpotInSec)
        {
            Name = name;
            X = x;
            Y = y;
            Width = width;
            Hight = hight;
            Price = price;
            TimePerSpotInSec = timePerSpotInSec;
            Target = target;
            freeParkSpots = new List<Spot>();
        }

        public void AddFreeParkSpot(Spot spot)
        {
            this.freeParkSpots.Add(spot);
        }

        public bool isInParkZone(int x, int y)
        {
            if (x >= this.X && x <= this.X + this.Width &&
                y >= this.Y && y <= this.Y + this.Hight)
            {
                return true;
            }

            return false;
        }
    }

    internal class Spot : IComparable<Spot>
    {
        public ParkZone ParkZone { get; }
        public int X { get; }
        public int Y { get; }
        public int DistanceToTarget { get; set; }
        public int TimeToTargetInMin { get; set; }

        public decimal PriceForStay { get; private set; }

        public Spot(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Spot(int x, int y, ParkZone parkZone)
        {
            X = x;
            Y = y;
            ParkZone = parkZone;
            ParkZone.AddFreeParkSpot(this);
            CalcDistToTarget();
            CalcTimeToTarget();
            CalculatePriceForStay();
        }

        private void CalculatePriceForStay()
        {
            PriceForStay = TimeToTargetInMin * ParkZone.Price;
        }


        private void CalcDistToTarget()
        {
            this.DistanceToTarget = Math.Abs(this.X - ParkZone.Target.X) + Math.Abs(this.Y - ParkZone.Target.Y) - 1;
        }

        private void CalcTimeToTarget()
        {
            this.TimeToTargetInMin =
                (int) Math.Ceiling( 2* DistanceToTarget * ParkZone.TimePerSpotInSec / 60.0);
        }

        public int CompareTo(Spot other)
        {
            if (this.PriceForStay == other.PriceForStay)
            {
                return this.DistanceToTarget.CompareTo(other.DistanceToTarget);
            }

            return this.PriceForStay.CompareTo(other.PriceForStay);
        }
    }
}