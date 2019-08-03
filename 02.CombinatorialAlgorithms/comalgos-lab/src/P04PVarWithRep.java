import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;


public class P04PVarWithRep {

    static String[] elements;
    static String[] variations;
    static int k;


    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        elements = reader.readLine().split("\\s+");
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
                variations[idxK] = elements[i];
                variate(idxK + 1);
        }
    }

    private static void printArr(String[] arr) {
        System.out.println(String.join(" ", arr));
    }


}
