﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BRKGA
    {
        private int n;       // number of genes in the chromosome
        private int pop;       // number of elements in the population
        private int popElite;      // number of elite items in the population
        private int popMutant;      // number of mutants introduced at each generation into the population
        private const float rhoe = 0.7f;      // probability that an offspring inherits the allele of its elite parent
        private int[][] dist;       //distance to ash and badges

        private Population previous;    // previous population
        private Population current;             // current population

        public BRKGA(int n, int p, int _pe, int _pm, bool[] capturedBadges)
        {
            // Error check:
            if (n == 0) { throw new Exception("Chromosome size equals zero."); }
            if (pop == 0) { throw new Exception("Population size equals zero."); }
            if (popElite == 0) { throw new Exception("Elite-set size equals zero."); }
            if (popElite > pop) { throw new Exception("Elite-set size greater than population size (pe > p)."); }
            if (popMutant > pop) { throw new Exception("Mutant-set size (pm) greater than population size (p)."); }
            if (popElite + popMutant > pop) { throw new Exception("elite + mutant sets greater than population size (p)."); }

            // Initialize and decode each chromosome of the current population, then copy to previous:
            // Allocate:
            current = new Population(n, p);

            // Initialize:
            Initialize(capturedBadges);

            // Then just copy to previous:
            previous = new Population(current);
        }

        private void Initialize(bool[] bdg) 
        {
            var rand = new Random();

            for(int j = 0; j < pop; ++j) 
            {
                var vet = bdg.Select((b,i)=>new {b,i}).Where(bd=>!bd.b).Select(bi=>bi.i).ToList();
                current.population[j][0] = 0 ;  /* onde o ash está */

                for(int k = 1; k < n; ++k) 
                {
                    int index = rand.Next(0, n-k+1) ;
                    current.population[j][k] = vet[index];
                    vet.RemoveAt(index);
               } 
            }

            // Decode:
            for(int j = 0; j < pop; ++j) {           
                current.SetFitness(Decoder(current.population[j]),j);
            }

            // Sort:
            current.SortFitness();
        }

        private int Decoder (int[] chromossome)
        {
            int path = 0;
            for (int i = 0; i < chromossome.Length; i++)
            {
                if(i+1 < chromossome.Length)
                    path += dist[chromossome[i]][chromossome[i+1]];  /* define total lenght from start point to final point */
            }
            return path;
        }
    }

}
