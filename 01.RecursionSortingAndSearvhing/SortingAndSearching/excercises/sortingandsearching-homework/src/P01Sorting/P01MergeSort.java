package P01Sorting;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;
import java.util.stream.Collectors;

public class P01MergeSort {

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        List<Integer> list = Arrays.stream(reader.readLine().split("\\s+"))
                .map(Integer::parseInt)
                .collect(Collectors.toList());

        List<Integer> ordList = mergeSort(list);

        String listStr = list.stream()
                .map(String::valueOf)
                .collect(Collectors.joining(" "));
        System.out.println(listStr);
    }

    private static List<Integer> mergeSort(List<Integer> list) {
        if (list.size() <= 1) {
            return list;
        }

        List<Integer> firstList = list.subList(0, (list.size() + 1) / 2);
        List<Integer> secondList = list.subList((list.size() + 1) / 2, list.size());

        List<Integer> ordFirstList = mergeSort(firstList);
        List<Integer> ordSecondList = mergeSort(secondList);

        List<Integer> ordMergedList = merge(ordFirstList, ordSecondList);

        return ordMergedList;
    }

    private static List<Integer> merge(List<Integer> ordFirstList, List<Integer> ordSecondList) {
        List<Integer> ordMergedList = new ArrayList<>();

        int firstListIdx = 0;
        int secondListIdx = 0;

        while (firstListIdx < ordFirstList.size() && secondListIdx < ordSecondList.size()) {
            int firstNum = ordFirstList.get(firstListIdx);
            int secondNum = ordSecondList.get(secondListIdx);
            if (firstNum <= secondNum) {
                ordMergedList.add(firstNum);
                firstListIdx++;
            } else {
                ordMergedList.add(secondNum);
                secondListIdx++;
            }
        }
        if (firstListIdx >= ordFirstList.size()) {
            ordMergedList.addAll(ordSecondList.subList(secondListIdx, ordSecondList.size()));
        } else {
            ordMergedList.addAll(ordFirstList.subList(firstListIdx, ordFirstList.size()));
        }

        return ordMergedList;
    }

}
