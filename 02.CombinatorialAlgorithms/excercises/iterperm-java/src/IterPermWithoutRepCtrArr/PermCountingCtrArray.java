package IterPermWithoutRepCtrArr;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class PermCountingCtrArray {

    // Utility function to swap two characters in a character array
    private static void swap(String[] arr, int i, int j) {
        var temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    // Iterative function to find permutations of a String in Java
    public static void permutations(String[] arr) {
        // Weight index control array
        // using control array of zeros "counting" perm not "countdown" perm http://www.quickperm.org/
        int[] ctrArr = new int[arr.length];

        // upIdx, lowIdx represents upper and lower bound index resp. for swapping
        int upIdx = 1, lowIdx = 0;

        // Print given string, as only its permutations will be printed later
        printArray(arr);

        while (upIdx < arr.length) {
            if (ctrArr[upIdx] < upIdx) {
                // if upIdx is odd then lowIdx = ctrArr[upIdx], otherwise lowIdx = 0
                lowIdx = (upIdx % 2) * ctrArr[upIdx];

                // swap(a[lowIdx], a[upIdx])
                swap(arr, upIdx, lowIdx);

                // Print current permutation
                printArray(arr);

                ctrArr[upIdx]++;    // increase index "weight" for upIdx by one
                upIdx = 1;    // reset index upIdx to 1
            }
            // otherwise ctrArr[upIdx] == upIdx
            else {
                // reset ctrArr[upIdx] to zero
                ctrArr[upIdx] = 0;

                // set new index value for upIdx (increase by one)
                upIdx++;
            }
        }
    }

    public static void printArray(String[] arr){
        System.out.println(String.join(" ", arr));
    }

    // Iterative program to find permutations of a String in Java
    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
        String[] arr = reader.readLine().split(" ");
        permutations(arr);
    }

}
