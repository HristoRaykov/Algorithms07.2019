package P05Needles;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;

public class P05Needles {

    private static int arrLen;
    private static int needlesLen;
    private static int[] arr;
    private static int[] needles;

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        String[] leghts = reader.readLine().split(" ");
        arrLen = Integer.parseInt(leghts[0]);
        needlesLen = Integer.parseInt(leghts[1]);

        arr = Arrays.stream(reader.readLine().split(" "))
                .mapToInt(Integer::valueOf)
                .toArray();
        needles = Arrays.stream(reader.readLine().split(" "))
                .mapToInt(Integer::valueOf)
                .toArray();

        int[] needleIdxs = calculateIndexes(arr, needles);

        System.out.println(String.join(" ", Arrays.stream(needleIdxs).mapToObj(String::valueOf).toArray(String[]::new)));
    }

    private static int[] calculateIndexes(int[] arr, int[] needles) {
        int[] needleIdxs = new int[needlesLen];

        for (int i = 0; i < needles.length; i++) {
            int needle = needles[i];
            int needleIdx = calculateNeedleIndex(arr, needle);
            needleIdxs[i] = needleIdx;
        }

        return needleIdxs;
    }

    private static int calculateNeedleIndex(int[] arr, int needle) {
        int idx = getLeftMostIndex(arr, needle);

        idx = backwardZeros(arr, idx);

        return idx;
    }

    private static int backwardZeros(int[] arr, int idx) {
        if (idx==0){
            return idx;
        }

        while (arr[idx - 1] == 0) {
            idx--;
            if (idx==0){
                return idx;
            }
        }
        return idx;
    }

    private static int getLeftMostIndex(int[] arr, int needle) {
        for (int i = 0; i < arr.length; i++) {
            if (needle <= arr[i]) {
                return i;
            }
        }
        return arr.length;
    }

}
