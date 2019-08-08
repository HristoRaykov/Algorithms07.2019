package P03InversionCount;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

public class P03InversionCount {

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        List<Integer> list = Arrays.stream(reader.readLine().split("\\s+"))
                .map(Integer::parseInt)
                .collect(Collectors.toList());

        int invCount = inversionCount(list, 0, list.size() - 1);

        System.out.println(invCount);
    }

    private static int inversionCount(List<Integer> list, int strIdx, int endIdx) {
        if (strIdx >= endIdx) {
            return 0;
        }

        int invCount = 0;
        int midIdx = (strIdx + endIdx) / 2;
        invCount += inversionCount(list, strIdx, midIdx);
        invCount += inversionCount(list, midIdx + 1, endIdx);
        invCount += merge(list, strIdx, midIdx, endIdx);

        return invCount;
    }

    private static int merge(List<Integer> list, int strIdx, int midIdx, int endIdx) {

        int invCount = 0;
        List<Integer> leftList = new ArrayList<>(list.subList(strIdx, midIdx + 1));
        List<Integer> rightList = new ArrayList<>(list.subList(midIdx + 1, endIdx + 1));
        int leftIdx = 0;
        int rightIdx = 0;

        int idx = strIdx;
        while (leftIdx < leftList.size() && rightIdx < rightList.size()) {
            int leftNum = leftList.get(leftIdx);
            int rightNum = rightList.get(rightIdx);
            if (leftNum <= rightNum) {
                leftIdx++;
                list.set(idx, leftNum);
            } else {
                rightIdx++;
                list.set(idx, rightNum);
                invCount += leftList.size() - leftIdx;
            }
            idx++;
        }

        List<Integer> restArr = new ArrayList<>();
        if (leftIdx < leftList.size()) {
            restArr = leftList.subList(leftIdx, leftList.size());
        } else if (rightIdx < rightList.size()) {
            restArr = rightList.subList(rightIdx, rightList.size());
        }

        int j = 0;
        for (int i = idx; i <= endIdx; i++) {
            list.set(i, restArr.get(j++));
        }

        return invCount;
    }

}
