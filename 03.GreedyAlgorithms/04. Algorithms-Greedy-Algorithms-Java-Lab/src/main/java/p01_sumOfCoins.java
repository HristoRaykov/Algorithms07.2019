import java.util.*;
import java.util.stream.Collectors;

public class p01_sumOfCoins {

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);

        String[] elements = in.nextLine().substring(7).split(", ");
        int[] coins = new int[elements.length];
        for (int i = 0; i < coins.length; i++) {
            coins[i] = Integer.parseInt(elements[i]);
        }

        int targetSum = Integer.parseInt(in.nextLine().substring(5));


        Map<Integer, Integer> usedCoins = chooseCoins(coins, targetSum);

        int coinsCount = 0;
        for (Integer count : usedCoins.values()) {
            coinsCount += count;
        }
        System.out.println("Number of coins to take: " + coinsCount);
        usedCoins.entrySet().stream()
                .sorted((e1, e2) -> {
                    if (e2.getValue().compareTo(e1.getValue()) == 0) {
                        return e2.getKey().compareTo(e1.getKey());
                    }
                    return e2.getValue().compareTo(e1.getValue());
                })
                .forEach(e -> System.out.println(String.format("%s coin(s) with value %s", e.getValue(), e.getKey())));

    }

    public static Map<Integer, Integer> chooseCoins(int[] coins, int targetSum) {
        Integer[] sortedCoins = new Integer[coins.length];
        for (int i = 0; i < coins.length; i++) {
            sortedCoins[i] = coins[i];
        }
        Arrays.sort(sortedCoins, Collections.reverseOrder());

        Map<Integer, Integer> usedCoins = new HashMap<>();
        int coinIdx = 0;
        int currSum = 0;
        int currCoin;
        int currCoinCount;

        while (currSum != targetSum && coinIdx < sortedCoins.length) {
            currCoin = sortedCoins[coinIdx];
            currCoinCount = (targetSum - currSum) / currCoin;
            if (currCoinCount == 0) {
                coinIdx++;
                continue;
            }

            currSum += currCoin * currCoinCount;
            usedCoins.put(currCoin, currCoinCount);
        }

        if (currSum != targetSum) {
            throw new IllegalArgumentException();
        }

        return usedCoins;
    }
}
