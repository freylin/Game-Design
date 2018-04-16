package dk.itu.mario.engine.level.generator;

import java.util.*;

import dk.itu.mario.MarioInterface.Constraints;
import dk.itu.mario.MarioInterface.GamePlay;
import dk.itu.mario.MarioInterface.LevelGenerator;
import dk.itu.mario.MarioInterface.LevelInterface;
import dk.itu.mario.engine.level.Level;
import dk.itu.mario.engine.level.MyLevel;
import dk.itu.mario.engine.level.MyDNA;

import dk.itu.mario.engine.PlayerProfile;

import dk.itu.mario.engine.sprites.SpriteTemplate;
import dk.itu.mario.engine.sprites.Enemy;

public class MyLevelGenerator{
	
	public boolean verbose = true; //print debugging info
	
	// Called by the game engine.
	// Returns the level to be played.
	public Level generateLevel(PlayerProfile playerProfile)
	{
		// Call genetic algorithm to optimize to the player profile
		MyDNA dna = this.geneticAlgorithm(playerProfile);
		
		// Post process
		dna = this.postProcess(dna);
		
		// Convert the solution to the GA into a Level
		MyLevel level = new MyLevel(dna, LevelInterface.TYPE_OVERGROUND);
		
		return (Level)level;
	}
	
	// Genetic Algorithm implementation
	private MyDNA geneticAlgorithm (PlayerProfile playerProfile)
	{
		// Set the population size
		int populationSize = getPopulationSize();
		
		// Make the population array
		ArrayList<MyDNA> population = new ArrayList<MyDNA>();
		
		// Make the solution, which is initially null
		MyDNA solution = null;
		
		// Generate a random population
		for (int i=0; i < populationSize; i++) {
			MyDNA newIndividual = this.generateRandomIndividual();
			newIndividual.setFitness(this.evaluateFitness(newIndividual, playerProfile));
			population.add(newIndividual);
		}
		if (this.verbose) {
			System.out.println("Initial population:");
			printPopulation(population);
		}
		
		// Iteration counter
		int count = 0;
		
		// Iterate until termination criteria met
		while (!this.terminate(population, count)) {
			// Make a new, possibly larger population
			ArrayList<MyDNA> newPopulation = new ArrayList<MyDNA>();

			// Keep track of individual's parents (for this iteration only)
			Hashtable parents = new Hashtable();

			// Mutuate a number of individuals
			ArrayList<MyDNA> mutationPool = this.selectIndividualsForMutation(population);
			for (int i=0; i < mutationPool.size(); i++) {
				MyDNA parent = mutationPool.get(i);
				// Mutate
				MyDNA mutant = parent.mutate();
				// Evaluate fitness
				double fitness = this.evaluateFitness(mutant, playerProfile);
				mutant.setFitness(fitness);
				// Add mutant to new population
				newPopulation.add(mutant);
				// Create a list of parents and remember it in a hash
				ArrayList<MyDNA> p = new ArrayList<MyDNA>();
				p.add(parent);
				parents.put(mutant, p);
			}
			
			
			// Do Crossovers
			for (int i=0; i < this.numberOfCrossovers(); i++) {
				// Pick two parents
				MyDNA parent1 = this.pickIndividualForCrossover(newPopulation, null);
				MyDNA parent2 = this.pickIndividualForCrossover(newPopulation, parent1);
				
				if (parent1 != null && parent2 != null) {
					// Crossover produces one or more children
					ArrayList<MyDNA> children = parent1.crossover(parent2);
					
					// Add children to new population and remember their parents
					for (int j=0; j < children.size(); j++) {
						// Get a child
						MyDNA child = children.get(j);
						// Evaluate fitness
						double fitness = this.evaluateFitness(child, playerProfile);
						child.setFitness(fitness);
						// Add it to new population
						newPopulation.add(child);
						// Create a list of parents and remember it in a hash
						ArrayList<MyDNA> p = new ArrayList<MyDNA>();
						p.add(parent1);
						p.add(parent2);
						parents.put(child, p);
					}
				}
				
			}
			
			// Cull the population
			// There is more than one way to do it.
			if (this.competeWithParentsOnly()) {
				population = this.competeWithParents(population, newPopulation, parents);
			}
			else {
				population = this.globalCompetition(population, newPopulation);
			}
			
			//increment counter
			count = count + 1;

			if (this.verbose) {
				MyDNA best = this.getBestIndividual(population);
				System.out.println("" + count + ": Best: " + best + " fitness: " + best.getFitness());
			}
		}
		
		// Get the winner
		solution = this.getBestIndividual(population);
		
		if (this.verbose) {
			System.out.println("Solution: " + solution + " fitnes: " + solution.getFitness());
		}
		
		return solution;
	}
	
	// Create a random individual.
	private MyDNA generateRandomIndividual ()
	{
		MyDNA individual = new MyDNA();
		Random random = new Random();
		String ss = "abcde";
		int ss_length = ss.length();
		String s = "";
		int s_length = 150;
		for (int i = 0; i < s_length; i++) {
			s = s + ss.charAt(random.nextInt(ss_length));
		}
		individual.setChromosome(s);
		return individual;
	}
	
	// Returns true if the genetic algorithm should terminate.
	private boolean terminate (ArrayList<MyDNA> population, int count)
	{
		boolean decision = false;
		if (count > 1000) return true;
		MyDNA BestIndividual = this.getBestIndividual(population);
		if (BestIndividual.getFitness() > 0.8) return true;
		return decision;
	}
	
	// Return a list of individuals that should be copied and mutated.
	private ArrayList<MyDNA> selectIndividualsForMutation (ArrayList<MyDNA> population)
	{
		ArrayList<MyDNA> selected = new ArrayList<MyDNA>();
		Random random = new Random();
		int size = population.size();
		int num = random.nextInt(size) + 1;
		for (int i = 1; i <= num; i++) {
			int ind = random.nextInt(size);
			selected.add(population.get(ind));
		}
		return selected;
	}
	
	// Returns the size of the population.
	private int getPopulationSize ()
	{
		int num = 1; // Default needs to be changed
		num = 280;
		return num;
	}
	
	// Returns the number of times crossover should happen per iteration.
	private int numberOfCrossovers ()
	{
		int num = 0; // Default is no crossovers
		num = 140;
		return num;

	}
	
	// Pick one of the members of the population that is not the same as excludeMe
	private MyDNA pickIndividualForCrossover (ArrayList<MyDNA> population, MyDNA excludeMe)
	{
		MyDNA picked = null;
		Random random = new Random();
		int size = population.size();
		int ind = random.nextInt(size);
		picked = population.get(ind);
		int count = 0;
		while (picked == excludeMe && count<500) {
			count++;
			ind = random.nextInt(size);
			picked = population.get(ind);
		}
		if (picked == excludeMe) {
			return null;
		}
		else {
			return picked;
		}
	}
	
	// Returns true if children compete to replace parents.
	// Retursn false if the the global population competes.
	private boolean competeWithParentsOnly ()
	{
		boolean doit = false;
		doit = false;
		return doit;
	}
	
	// Determine if children are fitter than parents and keep the fitter ones.
	private ArrayList<MyDNA> competeWithParents (ArrayList<MyDNA> oldPopulation, ArrayList<MyDNA> newPopulation, Hashtable parents)
	{
		ArrayList<MyDNA> finalPopulation = new ArrayList<MyDNA>();
		if (finalPopulation.size() != this.getPopulationSize()) {
			throw new IllegalStateException("Population not the correct size.");
		}
		return finalPopulation;
	}
	
	// Combine the old population and the new population and return the top fittest individuals.
	private ArrayList<MyDNA> globalCompetition (ArrayList<MyDNA> oldPopulation, ArrayList<MyDNA> newPopulation)
	{
		ArrayList<MyDNA> finalPopulation = new ArrayList<MyDNA>();
		ArrayList<MyDNA> allPopulation = new ArrayList<MyDNA>();
		allPopulation.addAll(oldPopulation);
		allPopulation.addAll(newPopulation);
		int size = this.getPopulationSize();
		int count = 0;
		while(count < size && allPopulation.size()>0) {
			MyDNA best = getBestIndividual(allPopulation);
			allPopulation.remove(best);
			finalPopulation.add(best);
			count++;
		}

		if (finalPopulation.size() != this.getPopulationSize()) {
			throw new IllegalStateException("Population not the correct size.");
		}
		return finalPopulation;
	}
	
	// Return the fittest individual in the population.
	private MyDNA getBestIndividual (ArrayList<MyDNA> population)
	{
		MyDNA best = population.get(0);
		double bestFitness = Double.NEGATIVE_INFINITY;
		for (int i=0; i < population.size(); i++) {
			MyDNA current = population.get(i);
			double currentFitness = current.getFitness();
			if (currentFitness > bestFitness) {
				best = current;
				bestFitness = currentFitness;
			}
		}
		return best;
	}
	
	// Changing this function is optional.
	private double evaluateFitness (MyDNA dna, PlayerProfile playerProfile)
	{
		double fitness = 0.0;
		MyLevel level = new MyLevel(dna, LevelInterface.TYPE_OVERGROUND);
		fitness = playerProfile.evaluateLevel(level);
		return fitness;
	}
	
	private MyDNA postProcess (MyDNA dna)
	{
		return dna;
	}
	
	//for this to work, you must implement MyDNA.toString()
	private void printPopulation (ArrayList<MyDNA> population)
	{
		for (int i=0; i < population.size(); i++) {
			MyDNA dna = population.get(i);
			System.out.println("Individual " + i + ": " + dna + " fitness: " + dna.getFitness());
		}
	}
	
	// MAKE ANY NEW MEMBER FUNCTIONS HERE
	
}
