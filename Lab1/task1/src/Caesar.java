public class Caesar {

    public static String decodeXor(String in, int key) {
        StringBuilder out = new StringBuilder();

        for (int i = 0; i < in.length(); i++) {
            out.append((char) (in.charAt(i) ^ key));
        }

        return out.toString();
    }
}
