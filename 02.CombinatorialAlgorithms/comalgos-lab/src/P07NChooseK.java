import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;


public class P07NChooseK {

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        int n = Integer.parseInt(reader.readLine());
        int k = Integer.parseInt(reader.readLine());

        int count = nChooseK(n, k);

        System.out.println(count);
    }

    private static int nChooseK(int n, int k) {
        if (k==0 || k==n) {
            return 1;
        }

        return nChooseK(n-1, k-1) + nChooseK(n-1, k);
    }




}
