import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;
import java.util.stream.Collectors;

public class p02_secCover {

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);

        String[] elements = in.nextLine().substring(10).split(", ");
        int[] universe = new int[elements.length];
        for (int i = 0; i < elements.length; i++) {
            universe[i] = Integer.parseInt(elements[i]);
        }

        int numberOfSets = Integer.parseInt(in.nextLine().substring(16));
        List<int[]> sets = new ArrayList<>();
        for (int i = 0; i < numberOfSets; i++) {
            String[] setElements = in.nextLine().split(", ");
            int[] set = new int[setElements.length];
            for (int j = 0; j < setElements.length; j++) {
                set[j] = Integer.parseInt(setElements[j]);
            }
            sets.add(set);
        }

        List<int[]> choosenSets = chooseSets(sets, universe);

        System.out.println();
    }

    public static List<int[]> chooseSets(List<int[]> sets, int[] universe) {
        List<Integer> universeList = convertArrayToList(universe);
        List<int[]> selectedSets = new ArrayList<>();
        int[] currSet;

        while (universeList.size() > 0) {
            sets = sets.stream()
                    .sorted((arr1, arr2) -> {
                       List<Integer> arr1UnivEl = convertArrayToList(arr1);
                       arr1UnivEl.retainAll(universeList);
                       List<Integer> arr2UnivEl = convertArrayToList(arr2);
                       arr2UnivEl.retainAll(universeList);
                       return arr2UnivEl.size() - arr1UnivEl.size();
                    })
            .collect(Collectors.toList());
            currSet = sets.get(0);
            selectedSets.add(currSet);
            sets.remove(0);
            universeList.removeAll(convertArrayToList(currSet));
        }


        return selectedSets;
    }

    public static List<Integer> convertArrayToList(int[] arr){
        List<Integer> list = new ArrayList<>();
        for (int el : arr) {
            list.add(el);
        }
        return list;
    }
}
