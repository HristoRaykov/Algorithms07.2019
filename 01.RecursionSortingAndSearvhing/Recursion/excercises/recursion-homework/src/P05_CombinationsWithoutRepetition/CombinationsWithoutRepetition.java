package P05_CombinationsWithoutRepetition;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.stream.Collectors;

public class CombinationsWithoutRepetition {


    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        int n = Integer.parseInt(reader.readLine());
        int k = Integer.parseInt(reader.readLine());
        int[] arrN = new int[n];
        int[] arrK = new int[k];
        for (int i = 1; i <= n; i++) {
            arrN[i - 1] = i;
        }

        combinationsWithoutRepetitions(arrN, arrK, 0, 0);

    }

    private static void combinationsWithoutRepetitions(int[] arrN, int[] arrK, int idxN, int idxK) {
        if (idxK > arrK.length - 1) {
            printArray(arrK);
        } else {
            for (int i = idxN; i < arrN.length; i++) {
                arrK[idxK] = arrN[i];
                combinationsWithoutRepetitions(arrN, arrK, i+1, idxK+1);
            }
        }


    }

    private static void printArray(int[] arr) {
        System.out.println(Arrays.stream(arr)
                .mapToObj(Integer::toString)
                .collect(Collectors.joining(" ")));
    }
}
