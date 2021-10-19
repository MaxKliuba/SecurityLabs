import java.util.*;

public class Vigenere {

    private static final String ANSI_RESET = "\u001B[0m";
    private static final String ANSI_GREEN = "\u001B[32m";

    public static int analyzeKeyLength(String in) {
        float[] probabilityArray = new float[in.length() - 1];
        List<Integer> highProbabilityIndexList = new ArrayList<>();

        for (int i = 1; i < in.length(); i++) {
            int n = 0;

            for (int j = 0; j < in.length(); j++) {
                if (in.charAt(j) == in.charAt((j + i) % in.length())) {
                    n++;
                }
            }

            probabilityArray[i - 1] = (float) n / in.length();

            if (probabilityArray[i - 1] >= 1 / 26.0f) {
                highProbabilityIndexList.add(i - 1);
            }

            if (probabilityArray[i - 1] >= 1 / 26.0f) {
                System.out.print(ANSI_GREEN + probabilityArray[i - 1] + "\t" + ANSI_RESET);
            } else {
                System.out.print(probabilityArray[i - 1] + "\t");
            }
            if (i == in.length() - 1) {
                System.out.println();
            }
        }

        Map<Integer, Integer> possibleLengthMap = new HashMap<>();
        int length = 0;

        for (int i = 1; i < highProbabilityIndexList.size(); i++) {
            int possibleLength = highProbabilityIndexList.get(i) - highProbabilityIndexList.get(i - 1);
            if (possibleLengthMap.containsKey(possibleLength)) {
                possibleLengthMap.replace(possibleLength, possibleLengthMap.get(possibleLength) + 1);
            } else {
                possibleLengthMap.put(possibleLength, 1);
            }
            length = possibleLength;
        }

        for (Integer possibleLength : possibleLengthMap.keySet()) {
            if (possibleLengthMap.get(possibleLength).compareTo(possibleLengthMap.get(length)) < 0) {
                length = possibleLength;
            }
        }

        return length;
    }

    public static String decodeXor(String in, String key) {
        StringBuilder out = new StringBuilder();

        for (int i = 0; i < in.length(); i++) {
            out.append((char) (in.charAt(i) ^ key.charAt(i % key.length())));
        }

        return out.toString();
    }
}
