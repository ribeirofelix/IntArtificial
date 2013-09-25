﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Population
    {
        public int[][] population;
        public Tuple <int, int> [] fitness ;   /* <fitness, chromossome> */

        public Population (int n, int p)
        {
            if (p == 0) { throw new Exception("Population size p cannot be zero."); }
            if (n == 0) { throw new Exception("Chromosome size n cannot be zero."); }
            population = Enumerable.Repeat<int[]>(new int[n], p).ToArray();
            fitness = new Tuple <int, int> [p] ;
        }

        public Population (Population pop)
        {
                population = (pop.population);
                fitness = (pop.fitness);
        }

        public void SortFitness()                                 // Sorts 'fitness' by its first parameter
        {
            fitness = fitness.OrderBy(f => f.Item1).ToArray();
        }

        public void SetFitness(int i, int f)                     // Sets the fitness of chromosome i
        {
            fitness[i] = new Tuple <int, int> (f, i) ;
        }

        private int[] GetChromosome(int i)       // Returns a chromosome
        {
            return population[ fitness[i].Item2 ];
        }

        public int GetN ()
        {
            return population[0].Length;
        }

        public int getP ()
        {
            return population.Length ;
        }

        public double getBestFitness()  // Returns the best fitness in this population
        {
            return getFitness(0);
        }

        public int getFitness(int i)   // Returns the fitness of chromosome i
        {
            return fitness[i].Item1 ;
        }
    
        
    
    }
   
}
