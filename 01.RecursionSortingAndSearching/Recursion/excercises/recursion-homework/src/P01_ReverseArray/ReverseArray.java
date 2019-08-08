package P01_ReverseArray;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.stream.Collectors;

public class ReverseArray {

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        List<Integer> arr = Arrays.stream(reader.readLine().split(" "))
                .map(Integer::parseInt)
                .collect(Collectors.toList());
        int index = 0;
        List<String> reversedArr = reverseArray(arr, index).stream()
                .map(String::valueOf)
                .collect(Collectors.toList());

        System.out.println(String.join(" ", reversedArr));
    }

    private static List<Integer> reverseArray(List<Integer> arr, int index) {
        List<Integer> reversedArr = new ArrayList<>();
        int len = arr.size()-1;
        if (index > len) {
            return reversedArr;
        }
        reversedArr = reverseArray(arr, index+1);
        reversedArr.add(arr.get(index));
        return reversedArr;
        }
    }
