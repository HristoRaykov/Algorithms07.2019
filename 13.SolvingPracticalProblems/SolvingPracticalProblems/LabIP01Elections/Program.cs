using System;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using Mono.Math;

namespace LabIP01Elections
{
    internal class Program
    {
        private static int K;

        private static int N;

        private static int[] partiesSeats;


        private static BigInteger[] seatsSumsCount;

        public static void Main(string[] args)
        {
            ReadInput();

            var maxSum = 0;
            for (var i = 0; i < partiesSeats.Length; i++)
            {
                maxSum += partiesSeats[i];
            }

            seatsSumsCount = new BigInteger[maxSum + 1];


            // using subset sum algorithm
            CalculateSeatsSums();
            var combinations = new BigInteger(0);
            for (var seatsSum = K; seatsSum < seatsSumsCount.Length; seatsSum++)
            {
                combinations += seatsSumsCount[seatsSum];
            }

            Console.WriteLine(combinations);
        }

        private static void CalculateSeatsSums()
        {
            seatsSumsCount[0] = 1;
//            for (int partyIdx = 0; partyIdx < partiesSeats.Length; partyIdx++)
            foreach (var currPartySeats in partiesSeats)
            {
//                var currPartySeats = partiesSeats[partyIdx];
                for (int seatsSum = seatsSumsCount.Length-1; seatsSum >= 0; seatsSum--)
                {
                    if (seatsSumsCount[seatsSum] > 0) //check if there is calculated valid sum
                    {
                        var newSeatsSum = currPartySeats + seatsSum;
                        seatsSumsCount[newSeatsSum] += seatsSumsCount[seatsSum];
                    }
                }
            }
        }

        private static void ReadInput()
        {
            K = int.Parse(Console.ReadLine());
            N = int.Parse(Console.ReadLine());

            partiesSeats = new int[N];

            for (int i = 0; i < N; i++)
            {
                partiesSeats[i] = int.Parse(Console.ReadLine());
            }
        }
    }
}