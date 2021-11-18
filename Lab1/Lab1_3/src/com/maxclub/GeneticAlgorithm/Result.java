package com.maxclub.GeneticAlgorithm;

import java.util.List;

public class Result {

    private List<Character> key;
    private String text;

    public Result(List<Character> key, String text) {
        this.key = key;
        this.text = text;
    }

    public List<Character> getKey() {
        return key;
    }

    public void setKey(List<Character> key) {
        this.key = key;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    @Override
    public String toString() {
        return String.format("%s -> %s", key, text);
    }
}
