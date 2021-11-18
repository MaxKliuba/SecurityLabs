package com.maxclub;

import java.io.*;
import java.util.HashMap;
import java.util.Map;

public class NGrams {

    public static Map<String, Double> fromFile(String path) {
        Map<String, Double> nGramsMap = new HashMap<>();

        try (BufferedReader bufferedReader = new BufferedReader(new FileReader(path))) {
            String line;
            while ((line = bufferedReader.readLine()) != null) {
                String[] strs = line.split(" ");
                nGramsMap.put(strs[0], Double.parseDouble(strs[1]));
            }
        } catch (IOException e) {
            e.printStackTrace();
        }

        return nGramsMap;
    }

    public static Map<String, Double> fromText(int n, String text) {
        Map<String, Double> nGrams = new HashMap<>();

        int count = 0;
        for (int i = 0; i < text.length() - n + 1; i++) {
            String ngram = text.substring(i, i + n);
            nGrams.put(ngram, nGrams.containsKey(ngram) ? nGrams.get(ngram) + 1 : 1);
            count++;
        }

        for (String key : nGrams.keySet()) {
            nGrams.put(key, nGrams.get(key) / count);
        }

        return nGrams;
    }
}
