using System;
using System.Collections.Generic;
using System.Linq;

namespace P02TravellingPoliceman
{
    internal class Program
    {
        private static int fuel;

        private static Street[] streets;

        private static int[,] bestValues;

        private static bool[,] visitedStreets;


        public static void Main(string[] args)
        {
            ReadInput();

            // knapsack problem
            SelectBestStreets();

            var bestStrs = ReconstructBestStreets();

            
            Console.WriteLine(string.Join(" -> ", bestStrs.Select(s=>s.Name)));
            Console.WriteLine($"Total pokemons caught -> {bestStrs.Sum(s=>s.PokemonCount)}");
            Console.WriteLine($"Total car damage -> {bestStrs.Sum(s=>s.CarDamage)}");
            Console.WriteLine($"Fuel Left -> {fuel}");
            
        }

        private static List<Street> ReconstructBestStreets()
        {
            var bestStrs = new List<Street>();

            var maxVal = bestValues[bestValues.GetLength(0) - 1, bestValues.GetLength(1) - 1];
            for (int strIdx = bestValues.GetLength(0)-1; strIdx > 0; strIdx--)
            {
                if (!visitedStreets[strIdx, fuel])
                {
                    continue;
                }

                var street = streets[strIdx - 1];
                bestStrs.Add(street);
                fuel -= street.Lenght;

            }

            bestStrs.Reverse();
            return bestStrs;
        }

        private static void SelectBestStreets()
        {
            for (int strIdx = 1; strIdx < bestValues.GetLength(0); strIdx++)
            {
                for (int f = 1; f < bestValues.GetLength(1); f++)
                {
                    var street = streets[strIdx-1];
                    var fuelNeeded = street.Lenght;
                    var bestValNoVisit = bestValues[strIdx - 1, f];
                    if (f < fuelNeeded)
                    {
                        bestValues[strIdx, f] = bestValNoVisit;
                    }
                    else
                    {
                        var bestValVisit = street.Value + bestValues[strIdx - 1, f - fuelNeeded];
                        if (bestValNoVisit >= bestValVisit)
                        {
                            bestValues[strIdx, f] = bestValNoVisit;
                        }
                        else
                        {
                            bestValues[strIdx, f] = bestValVisit;
                            visitedStreets[strIdx, f] = true;
                        }
                    }
                }
            }
        }

        private static void ReadInput()
        {
            fuel = int.Parse(Console.ReadLine());

            var strs = new List<Street>();

            var id = 0;
            while (true)
            {
                var line = Console.ReadLine();
                if (line == "End")
                {
                    break;
                }

                var lineParams = line.Split(',').ToArray();
                var streetName = lineParams[0];
                var carDamage = int.Parse(lineParams[1]);
                var pokCount = int.Parse(lineParams[2]);
                var length = int.Parse(lineParams[3]);

                var street = new Street(id, streetName, carDamage, pokCount, length);
                strs.Add(street);
                id++;
            }

            streets = strs.ToArray();
            bestValues = new int[streets.Length + 1, fuel + 1];
            visitedStreets = new bool[streets.Length + 1, fuel + 1];
        }
    }

    internal class Street
    {
        public int Id { get; }
        public string Name { get; }
        public int CarDamage { get; }
        public int PokemonCount { get; }
        public int Lenght { get; }
        public int Value { get; private set; }

        public Street(int id, string name, int carDamage, int pokemonCount, int length)
        {
            Id = id;
            Name = name;
            CarDamage = carDamage;
            PokemonCount = pokemonCount;
            Lenght = length;
            Value = PokemonCount * 10 - CarDamage;
        }
    }
}