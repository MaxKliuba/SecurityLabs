package com.maxclub.GeneticAlgorithm;

import java.util.ArrayList;
import java.util.List;

public class Population {

    private List<Chromosome> chromosomes;

    public Population(List<Chromosome> chromosomes) {
        this.chromosomes = chromosomes;
    }

    public List<Chromosome> getChromosomes() {
        return chromosomes;
    }

    public void setChromosomes(List<Chromosome> chromosomes) {
        this.chromosomes = chromosomes;
    }

    public int getSize() {
        return chromosomes.size();
    }

    public Chromosome getBestChromosome() {
        return chromosomes.get(0);
    }

    public double getTotalFitness() {
        double totalFitness = 0;
        for (Chromosome chromosome : chromosomes) {
            totalFitness += chromosome.getFitness();
        }

        return totalFitness;
    }

    public static Population create(int size) {
        List<Chromosome> chromosomes = new ArrayList<>(size);

        for (int i = 0; i < size; i++) {
            chromosomes.add(Chromosome.create());
        }

        return new Population(chromosomes);
    }
}
