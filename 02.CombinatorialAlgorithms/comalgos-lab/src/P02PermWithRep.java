import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.HashSet;
import java.util.Set;


public class P02PermWithRep {

    static String[] arr;


    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        arr = reader.readLine().split("\\s+");

        permute( 0);

    }

    private static void permute(int idx) {
        if (idx == arr.length){
            printArr(arr);
            return;
        }

        permute(idx+1);
        Set<String> swapped = new HashSet<>();
        swapped.add(arr[idx]);
        for (int i = idx+1; i < arr.length; i++) {
            if (!swapped.contains(arr[i])) {
                swap(idx, i);
                permute(idx + 1);
                swap(idx, i);
                swapped.add(arr[i]);
            }
        }
    }

    private static void swap(int idx, int i) {
        String temp = arr[idx];
        arr[idx] = arr[i];
        arr[i] = temp;
    }

    private static void printArr(String[] arr) {
        System.out.println(String.join(" ", arr));
    }


}
