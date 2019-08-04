import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;


public class P03VarWithoutRep {

    static String[] elements;
    static boolean[] used;
    static String[] variations;
    static int k;


    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        elements = reader.readLine().split("\\s+");
        used = new boolean[elements.length];
        k = Integer.parseInt(reader.readLine());
        variations = new String[k];

        variate(0);

    }

    private static void variate(int idxK) {
        if (idxK == k){
            printArr(variations);
            return;
        }

        for (int i = 0; i <elements.length; i++) {
            if (!used[i]) {
                variations[idxK] = elements[i];
                used[i] = true;
                variate(idxK + 1);
                used[i] = false;
            }
        }
    }

    private static void printArr(String[] arr) {
        System.out.println(String.join(" ", arr));
    }


}
