package P03_CombinationsWithRepetition;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.stream.Collectors;

public class CombinationsWithRepetition {

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        int n = Integer.parseInt(reader.readLine().trim());
        int k = Integer.parseInt(reader.readLine().trim());

        int[] Narr = new int[n];
        for (int i = 0; i < n; i++) {
            Narr[i]=i+1;
        }
        int[] Karr = new int[k];

        combinationsWithRepetition(Narr, Karr, 0, 0);

        System.out.println();
    }

    private static void combinationsWithRepetition(int[] arrN, int[] arrK, int idxK, int idxN) {
        if (idxK > arrK.length-1) {
            printList(arrK);
        } else {
            for (int i = idxN; i < arrN.length; i++) {
                arrK[idxK] = arrN[i];
                combinationsWithRepetition(arrN, arrK, idxK+1, i);
            }
        }
    }

    private static void printList(int[] arr) {
        System.out.println(Arrays.stream(arr)
                .mapToObj(Integer::toString)
                .collect(Collectors.joining(" ")));
    }

}
