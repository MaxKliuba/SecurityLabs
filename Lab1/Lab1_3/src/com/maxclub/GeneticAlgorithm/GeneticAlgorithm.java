package com.maxclub.GeneticAlgorithm;

import com.maxclub.NGrams;
import com.maxclub.SubstitutionCipher;

import java.util.*;

public class GeneticAlgorithm {

    private final Population population;
    private final Map<String, Double> trigrams;
    private final int generationCount;

    public GeneticAlgorithm(Map<String, Double> trigrams, int populationSize, int generationCount) {
        this.trigrams = trigrams;
        this.population = Population.create(populationSize);
        this.generationCount = generationCount;
    }

    public Result decrypt(String cipher) {
        evaluate(cipher);

        for (int i = 0; i < generationCount; i++) {
            nextGeneration();
            evaluate(cipher);
            System.out.println(String.format("[%s]:\t %s", i, population.getBestChromosome()));
        }

        return new Result(population.getBestChromosome().getGenes(),
                SubstitutionCipher.decrypt(cipher, population.getBestChromosome().getGenes()));
    }

    private void evaluate(String cipher) {
        for (int i = 0; i < population.getSize(); i++) {
            Chromosome chromosome = population.getChromosomes().get(i);
            population.getChromosomes().get(i).setFitness(fitnessFunction(chromosome, cipher));
        }

        Collections.sort(population.getChromosomes());
    }

    private double fitnessFunction(Chromosome chromosome, String cipher) {
        String decrypt = SubstitutionCipher.decrypt(cipher, chromosome.getGenes());
        Map<String, Double> textTrigrams = NGrams.fromText(3, decrypt);

        double fitness = 0;
        for (String key : textTrigrams.keySet()) {
            double freq = textTrigrams.get(key);
            double freqEng = trigrams.containsKey(key) ? trigrams.get(key) : 0;
            fitness += freq - freqEng;
        }

        return fitness;
    }

    private void nextGeneration() {
        List<Chromosome> newChromosomes = new ArrayList<>(population.getSize());

        for (int i = 0; i < population.getSize() / 5; i++) {
            newChromosomes.add(population.getChromosomes().get(i));
        }

        for (int i = population.getSize() / 5; i < population.getSize(); i += 2) {
            Chromosome firstPatentChromosome = population.getChromosomes().get(selectionMethod(population));
            Chromosome secondPatentChromosome = population.getChromosomes().get(selectionMethod(population));

            crossover(firstPatentChromosome, secondPatentChromosome, newChromosomes);
        }

        population.setChromosomes(newChromosomes);
    }

    private int selectionMethod(Population population) {
        double totalFitness = population.getTotalFitness();

        Random rnd = new Random();
        double limit = rnd.nextDouble() * totalFitness;

        double sum = 0;
        for (int i = 0; i < population.getSize(); i++) {
            sum += population.getChromosomes().get(i).getFitness();

            if (sum >= limit) {
                return i;
            }
        }

        return 0;
    }

    public void crossover(Chromosome firstPatentChromosome, Chromosome secondPatentChromosome, List<Chromosome> newChromosomes) {
        Random rn = new Random();
        int end = rn.nextInt(firstPatentChromosome.getGenes().size()) + 1;
        int start = rn.nextInt(end);

        Chromosome firstChildChromosome = Chromosome.create(true);
        Chromosome secondChildChromosome = Chromosome.create(true);

        for (int i = start; i < end; i++) {
            firstChildChromosome.getGenes().set(i, secondPatentChromosome.getGenes().get(i));
            secondChildChromosome.getGenes().set(i, firstPatentChromosome.getGenes().get(i));

        }

        for (int i = 0; i < firstPatentChromosome.getGenes().size(); i++) {
            if (i == start) {
                i += end - start;

                if (i == firstPatentChromosome.getGenes().size()) {
                    continue;
                }
            }

            int index = i;
            while (firstChildChromosome.getGenes().contains(firstPatentChromosome.getGenes().get(index))) {
                index = firstChildChromosome.getGenes().indexOf(firstPatentChromosome.getGenes().get(index));
            }
            firstChildChromosome.getGenes().set(i, firstPatentChromosome.getGenes().get(index));

            index = i;
            while (secondChildChromosome.getGenes().contains(secondPatentChromosome.getGenes().get(index))) {
                index = secondChildChromosome.getGenes().indexOf(secondPatentChromosome.getGenes().get(index));
            }
            secondChildChromosome.getGenes().set(i, secondPatentChromosome.getGenes().get(index));

        }

        newChromosomes.add(firstChildChromosome);
        newChromosomes.add(secondChildChromosome);
    }
}

