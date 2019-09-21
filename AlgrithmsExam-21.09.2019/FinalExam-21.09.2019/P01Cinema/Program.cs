using System;
using System.Collections.Generic;
using System.Linq;

namespace P01Cinema
{
    class Program
    {
        private static List<string> friends;

        private static Dictionary<int, string> reservedSeats;

        private static string[] cinema;

        private static List<string> friendsWithNoSeat;

        private static List<string> combinations;

        static void Main(string[] args)
        {
            ReadInput();

            friendsWithNoSeat = friends.Except(reservedSeats.Values).ToList();

            FillReservedSeats();

            GenerateCombinations(0);

            Console.WriteLine(string.Join(Environment.NewLine, combinations));
        }

        private static void GenerateCombinations(int idx)
        {
            var seatNum = idx + 1;
            if (reservedSeats.ContainsKey(seatNum))
            {
                GenerateCombinations(idx + 1);
            }
            else
            {
                if (idx == cinema.Length)
                {
                    combinations.Add(string.Join(" ", cinema));
                    return;
                }
                else
                {
                    GenerateCombinations(idx + 1);
                    for (int i = idx + 1; i < cinema.Length; i++)
                    {
                        var seat = i + 1;
                        if (!reservedSeats.ContainsKey(seat))
                        {
                            Swap(idx, i);
                            GenerateCombinations(idx + 1);
                            Swap(idx, i);
                        }
                    }
                }
            }
        }

        private static void Swap(int i1, int i2)
        {
            var temp = cinema[i1];
            cinema[i1] = cinema[i2];
            cinema[i2] = temp;
        }

        private static void FillReservedSeats()
        {
            foreach (var reservedSeat in reservedSeats.Keys)
            {
                cinema[reservedSeat - 1] = reservedSeats[reservedSeat];
            }

            var idx = 0;
            for (int i = 0; i < cinema.Length; i++)
            {
                if (cinema[i] == null)
                {
                    cinema[i] = friendsWithNoSeat[idx];
                    idx++;
                }
            }
        }

        private static void ReadInput()
        {
            friends = Console.ReadLine().Split(", ").ToList();

            reservedSeats = new Dictionary<int, string>();

            cinema = new string[friends.Count];

            combinations = new List<string>();

            while (true)
            {
                var line = Console.ReadLine();
                if (line == "generate")
                {
                    break;
                }

                var lineParams = line.Split(" - ").ToArray();
                reservedSeats[int.Parse(lineParams[1])] = lineParams[0];
            }
        }
    }
}