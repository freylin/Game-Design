package dk.itu.mario.engine.level;

import java.util.Random;
import java.util.*;

public class MyDNA extends DNA
{
	
	public int numGenes = 0; //number of genes

	// Return a new DNA that differs from this one in a small way.
	// Do not change this DNA by side effect; copy it, change the copy, and return the copy.
	public MyDNA mutate ()
	{
		MyDNA copy = new MyDNA();
		Random random = new Random();
		String ss = "abcde";
		String s = this.getChromosome();
		int ind = random.nextInt(ss.length());
		int ind2 = random.nextInt(s.length());

		while (s.charAt(ind2) == ss.charAt(ind)) ind = random.nextInt(ss.length());

		String newChromosome = s.substring(0, ind2)+ss.charAt(ind)+s.substring(ind2+1, s.length());
		copy.setChromosome(newChromosome);
		return copy;
	}
	
	// Do not change this DNA by side effect
	public ArrayList<MyDNA> crossover (MyDNA mate)
	{
		ArrayList<MyDNA> offspring = new ArrayList<MyDNA>();
		MyDNA copy1 = new MyDNA();
		MyDNA copy2 = new MyDNA();
		Random random = new Random();

		String Chromosome1 = this.getChromosome();
		String Chromosome2 = mate.getChromosome();
		String s1 = "";
		String s2 = "";
		for (int i = 0; i < this.getLength(); i++) {
			int k = random.nextInt(2);
			if (k == 1) {
				s1 += Chromosome1.charAt(i);
				s2 += Chromosome2.charAt(i);
			}
			else {
				s1 += Chromosome2.charAt(i);
				s2 += Chromosome1.charAt(i);
			}
		}

		copy1.setChromosome(s1);
		copy2.setChromosome(s2);
		offspring.add(copy1);
		offspring.add(copy2);
		return offspring;
	}
	
	// Optional, modify this function if you use a means of calculating fitness other than using the fitness member variable.
	// Return 0 if this object has the same fitness as other.
	// Return -1 if this object has lower fitness than other.
	// Return +1 if this objet has greater fitness than other.
	public int compareTo(MyDNA other)
	{
		int result = super.compareTo(other);
		return result;
	}
	
	
	// For debugging purposes (optional)
	public String toString ()
	{
		String s = super.toString();
		return s;
	}
	
	public void setNumGenes (int n)
	{
		this.numGenes = n;
	}

}

