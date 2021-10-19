package caesar;

import java.util.ArrayList;
import java.util.List;

public class Caesar {

    public static String decodeXor(String in, int key) {
        StringBuilder out = new StringBuilder();

        for (int i = 0; i < in.length(); i++) {
            out.append((char) (in.charAt(i) ^ key));
        }

        return out.toString();
    }

    public static String decodeXorBruteforce(String in) {
        List<String> resultList = new ArrayList<>();

        for (int key = 0; key < 256; key++) {
            resultList.add(decodeXor(in, key));
        }

        int index = 1;
        float maxCount = 0;

        for (int i = 1; i < resultList.size(); i++) {
            float p = getTextPercent(resultList.get(i));

            System.out.println("[KEY " + i + " - " + p + "%]: " + resultList.get(i));
            if (p > maxCount) {
                maxCount = p;
                index = i;
            }
        }

        return resultList.get(index);
    }

    private static float getTextPercent(String str) {
        float n = 0;

        for (int i = 0; i < str.length(); i++) {
            char ch = str.charAt(i);
            if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || ch == '.' || ch == ',' || ch == ' ') {
                n++;
            }
        }

        return n / str.length() * 100.0f;
    }
}
