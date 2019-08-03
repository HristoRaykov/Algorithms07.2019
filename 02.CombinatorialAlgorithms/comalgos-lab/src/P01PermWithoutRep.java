import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;


public class P01PermWithoutRep {


    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        String[] perm = reader.readLine().split("\\s+");

        permute(perm, 0);

    }

    private static void permute(String[] perm, int idx) {
        if (idx == perm.length){
            printArr(perm);
            return;
        }

        permute(perm, idx+1);

        for (int i = idx+1; i < perm.length; i++) {
            swap(perm, idx, i);
            permute(perm, idx+1);
            swap(perm, idx, i);
        }
    }

    private static void swap(String[] perm, int idx, int i) {
        String temp = perm[idx];
        perm[idx] = perm[i];
        perm[i] = temp;
    }

    private static void printArr(String[] arr) {
        System.out.println(String.join(" ", arr));
    }


}
