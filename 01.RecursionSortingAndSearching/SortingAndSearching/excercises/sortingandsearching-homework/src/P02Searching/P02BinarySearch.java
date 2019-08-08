package P02Searching;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;


public class P02BinarySearch {

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        List<Integer> list = Arrays.stream(reader.readLine().split(" "))
                .map(Integer::valueOf)
                .collect(Collectors.toList());
        int num = Integer.parseInt(reader.readLine());

        int idx = binSearch(list, num, 0, list.size()-1);

        System.out.println(idx);
    }

    private static int binSearch(List<Integer> list, int num, int leftIdx, int rightIdx) {
        if (num<list.get(leftIdx) || num > list.get(rightIdx)){
            return -1;
        }

        if (leftIdx == rightIdx) {
            if (num == list.get(leftIdx)) {
                return leftIdx;
            } else {
                return -1;
            }
        }

        int midIdx = (leftIdx + rightIdx) / 2;
        int midNum = list.get(midIdx);

        if (num == midNum) {
            return midIdx;
        } else if (num < midNum) {
            return binSearch(list, num, leftIdx, midIdx-1);
        } else {
            return binSearch(list, num, midIdx + 1, rightIdx);
        }

    }

}
