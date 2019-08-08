package P04Words;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.HashMap;
import java.util.Map;

public class P04Words {

    private static char[] chars;

    private static int len;

    private static Map<Character, Integer> charMap;

    public static void main(String[] args) throws IOException {
        BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));

        chars = reader.readLine().toCharArray();
        len = chars.length;
        charMap = new HashMap<>();

        for (char ch : chars) {
            if (!charMap.containsKey(ch)) {
                charMap.put(ch, 1);
            } else {
                int count = charMap.get(ch);
                charMap.put(ch, count + 1);
            }
        }

        int wordsCount = generateWords(new char[len], 0, 'A');
        System.out.println(wordsCount);
    }

    private static int generateWords(char[] arr, int idx, char prevChar) {
        int wordsCount = 0;

        if (idx == arr.length) {
            return ++wordsCount;
        }

        for (Character ch : charMap.keySet()) {
            if (ch == prevChar) {
                continue;
            }

            if (charMap.get(ch) == 0){
                continue;
            }
            arr[idx] = ch;
            charMap.put(ch, charMap.get(ch)-1);
            wordsCount += generateWords(arr, idx+1, ch);
            charMap.put(ch, charMap.get(ch)+1);

        }


        return wordsCount;
    }


}
