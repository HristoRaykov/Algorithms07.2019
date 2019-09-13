package src;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.math.BigInteger;

public class Main {

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));


        int K = Integer.parseInt(reader.readLine());
        int N = Integer.parseInt(reader.readLine());

        int[] partiesSeats = new int[N];

        for (int i = 0; i < N; i++) {
            partiesSeats[i] = Integer.parseInt(reader.readLine());
        }

        int maxSum = 0;
        for (int i = 0; i < partiesSeats.length; i++) {
            maxSum += partiesSeats[i];
        }

        BigInteger[] seatsSumsCount = new BigInteger[maxSum + 1];
        for (int i = 0; i < seatsSumsCount.length; i++) {
            seatsSumsCount[i] = BigInteger.ZERO;
        }
        seatsSumsCount[0] = BigInteger.ONE;

        for (var partySeats:partiesSeats) {
            for (int seatsSum = seatsSumsCount.length-1; seatsSum >=0; seatsSum--){
                if (seatsSumsCount[seatsSum].compareTo(BigInteger.ZERO) > 0) {
                    var newSeatSum = partySeats + seatsSum;
                    seatsSumsCount[newSeatSum] = seatsSumsCount[seatsSum].add(seatsSumsCount[newSeatSum]);
                }
            }
        }

        BigInteger combinations = BigInteger.ZERO;
        for (int i = K; i < seatsSumsCount.length; i++) {
            combinations = combinations.add(seatsSumsCount[i]);
        }

        System.out.println(combinations);

    }
}
