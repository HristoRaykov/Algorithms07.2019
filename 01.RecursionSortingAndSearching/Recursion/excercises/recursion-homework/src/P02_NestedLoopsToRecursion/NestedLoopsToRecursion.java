package P02_NestedLoopsToRecursion;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class NestedLoopsToRecursion {

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        int n = Integer.parseInt(reader.readLine().trim());
        List<Integer> arr = new ArrayList<>();
        nestedLoopsToRecursion(arr, n, 1);

        System.out.println();
    }

    private static void nestedLoopsToRecursion(List<Integer> arr, int n, int k) {
        if (k > n) {
            printList(arr);
        } else {
            for (int i = 1; i <= n; i++) {
                arr.add(i);
                nestedLoopsToRecursion(arr, n, k + 1);
                arr.remove(arr.size() - 1);
            }
        }
    }

    private static void printList(List<Integer> arr) {
        System.out.println(arr.stream()
                .map(String::valueOf)
                .collect(Collectors.joining(" ")));
    }

}
