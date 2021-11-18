package com.maxclub;

import java.util.List;

public class SubstitutionCipher {

    private static final String ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static String decrypt(String cipher, List<Character> key) {
        char[] input = cipher.toUpperCase().toCharArray();
        char[] output = new char[input.length];

        for (int i = 0; i < input.length; i++) {
            output[i] = key.get(input[i] - 'A');
        }

        return new String(output);
    }

    public static String encrypt(String text, List<Character> key) {
        char[] input = text.toUpperCase().replace(" ", "").toCharArray();
        char[] output = new char[input.length];

        for (int i = 0; i < input.length; i++) {
            if (Character.isAlphabetic(input[i])) {
                output[i] = ALPHABET.charAt(key.indexOf(input[i]));
            }
        }

        return new String(output);
    }
}
