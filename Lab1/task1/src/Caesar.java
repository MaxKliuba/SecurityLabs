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

        int index = 0;
        float maxCount = 0;

        for (int i = 0; i < resultList.size(); i++) {
            float p = getAsciiPercent(resultList.get(i));

            System.out.println("[KEY " + i + " - " + p + "%]: " + resultList.get(i));
            if (p > maxCount) {
                maxCount = p;
                index = i;
            }
        }

        return "[KEY " + index + "]: " + resultList.get(index);
    }

    private static float getAsciiPercent(String str) {
        float n = 0;

        for (int i = 0; i < str.length(); i++) {
            char ch = str.charAt(i);
            if (ch >= 27 && ch <= 128) {
                n++;
            }
        }

        return n / str.length() * 100.0f;
    }
}
