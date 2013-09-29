using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Population
    {
        public int[][] population;
        public Tuple<int, int>[] fitness;   /* < chromossome , fitness> */

        public Population (int n, int p)
        {
            if (p == 0) { throw new Exception("Population size p cannot be zero."); }
            if (n == 0) { throw new Exception("Chromosome size n cannot be zero."); }
            population = new int[p][] ;
            for(int k = 0; k< p; k++)
            {
                population[k] = new int[n] ;
                for (int h = 0; h < n; h++)
                {
                    population[k][h] = 0;
                }
            }
            population.Initialize();
            fitness = new Tuple <int, int> [p] ;
        }

        public Population (Population pop)
        {
                population = (pop.population);
                fitness = (pop.fitness);
        }

        public void SortFitness()                                 // Sorts 'fitness' by its first parameter
        {
            fitness = fitness.OrderBy(f => f.Item2).ToArray();
        }

        public void SetFitness(int fit, int ind)                     // Sets the fitness of chromosome i
        {
            fitness[ind] = new Tuple <int, int> (ind, fit) ;  
        }

        private int[] GetChromosome(int i)       // Returns a chromosome
        {
            return population[ fitness[i].Item1 ];
        }

        public int GetN ()
        {
            return population[0].Length;
        }

        public int getP ()
        {
            return population.Length ;
        }

        public int getBestFitness()
        {
            return getFitness(0);
        }

        public int getFitness(int i)   // Returns the fitness of chromosome i
        {
            return fitness[i].Item2 ;
        }
    
        
    
    }
   
}
