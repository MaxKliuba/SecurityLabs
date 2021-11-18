package com.maxclub.GeneticAlgorithm;

import java.util.*;

public class Chromosome implements Comparable<Chromosome> {

    private static final String ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private List<Character> genes;
    private double fitness;

    public Chromosome(List<Character> genes) {
        this.genes = genes;
    }

    public List<Character> getGenes() {
        return genes;
    }

    public void setGenes(List<Character> genes) {
        this.genes = genes;
    }

    public double getFitness() {
        return fitness;
    }

    public void setFitness(double fitness) {
        this.fitness = fitness;
    }

    @Override
    public int compareTo(Chromosome o) {
        if (this.fitness > o.getFitness()) {
            return 1;
        } else if (this.fitness < o.getFitness()) {
            return -1;
        }

        return 0;
    }

    public static Chromosome create() {
        return create(false);
    }

    public static Chromosome create(boolean empty) {
        List<Character> genes = new ArrayList<>(ALPHABET.length());
        for (int i = 0; i < ALPHABET.length(); i++) {
            genes.add(empty ? '_' : ALPHABET.charAt(i));
        }
        Collections.shuffle(genes);

        return new Chromosome(genes);
    }

    @Override
    public String toString() {
        return String.format("(%,.012f) -> %s", fitness, genes);
    }
}
