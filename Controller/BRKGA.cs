using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sorting;

namespace Controller
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
        private bool[] captBadgs;

        public BRKGA(int n, int pop, int popElt , int popMut , bool[] capturedBadges, int[][] distAshBdg )
        {
            // Error check:
            if (n == 0) { throw new Exception("Chromosome size equals zero."); }
            if (pop == 0) { throw new Exception("Population size equals zero."); }
            if (popElt == 0) { throw new Exception("Elite-set size equals zero."); }
            if (popElt > pop) { throw new Exception("Elite-set size greater than population size (pe > p)."); }
            if (popMut > pop) { throw new Exception("Mutant-set size (pm) greater than population size (p)."); }
            if (popElt + popMut > pop) { throw new Exception("elite + mutant sets greater than population size (p)."); }

            this.n = n;
            this.pop = pop;
            this.popElite = popElt;
            this.popMutant = popMut;
            this.captBadgs = capturedBadges;
            this.dist = distAshBdg;
            // Initialize and decode each chromosome of the current population, then copy to previous:
            // Allocate:
            current = new Population(n, pop);

            // Initialize:
            Initialize();

            // Then just copy to previous:
            previous = new Population(current);
        }

        private void Initialize() 
        {
            RandomizePopulation();

            // Decode:
            for(int j = 0; j < pop; ++j) {           
                current.SetFitness(Decoder(current.population[j]),j);
            }
            // Sort:
            current.SortFitness();
        }

        private void RandomizePopulation()
        {
            var rand = new Random();

            for (int j = 0; j < pop; ++j)
            {
                var vet = captBadgs.Select((b, i) => new { b, i }).Where(bd => !bd.b).Select(bi => bi.i).ToList();
                current.population[j][0] = 0;  /* onde o ash está */

                for (int k = 1; k < n; ++k)
                {
                    int index = rand.Next(0, n - k );
                    current.population[j][k] = vet[index]+1;
                    vet.RemoveAt(index);
                }
            }
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

        public void Evolve(int generations) {
                
            for(int i = 0; i < generations; ++i) {                
                Evolution( previous );   // First evolve the population (curr, next)
                previous = System.Threading.Interlocked.Exchange<Population>(ref current, previous); // Update (prev = curr; curr = prev == next)
            }
        }

        private void Evolution(Population next)
        {
            int inx = 0;
            for (inx = 0; inx < popElite; inx++)
            {
                next.population[inx] = current.population[current.fitness[inx].Item1];
            }
           
            var rnd = new Random(DateTime.Now.Millisecond);

            // 3. We'll mate 'p - pe - pm' pairs; initially, i = pe, so we need to iterate until i < p - pm:
            while (inx < pop - popMutant)
            {
                // Select an elite parent:
                int eliteParent = rnd.Next(popElite - 1);
                // Select a non-elite parent:
                int noneliteParent = rnd.Next(popElite, pop - 1);//colocar de pe a p

                // Mate:
                for (int j = 0; j < n; ++j)
                {
                    int sourceParent = ((rnd.NextDouble() < rhoe) ? eliteParent : noneliteParent);

                    next.population[inx][j] = current.population[sourceParent][j];
                }

                var repeated = new bool[n];   /* positions with false indicate that the badeg doesn't apear in the chromossome */
                var posRepeated = new int[n/2]; /* contains the positions where there are repeated chromossomes */
                repeated.Initialize();
                posRepeated.Initialize();

                int m = 0 ;
                for (int r = 0; r < n; ++r)
                {
                    if (repeated[next.population[inx][r]] == false)
                        repeated[next.population[inx][r]] = true;
                    else
                    {
                        posRepeated[m] = r;  /* there's a repeated number at this index */
                        m++ ;
                    }
                }
                m = 0;
                for (int r = 0; r < repeated.Count(); ++r)
                {
                    if (repeated[r] == false)
                    {
                        next.population[inx][posRepeated[m]] = r;
                        m++;
                    }
                }
                ++inx;
            }

            while (inx < popMutant)
            {
                RandomizePopulation();
            }

            // Decode:
            for (int j = 0; j < pop; ++j)
            {
                next.SetFitness(Decoder(next.population[j]), j);
            }

            // Sort:
            next.SortFitness();
        }

        public ICollection<BadgeTypes> GetChoice()
        {
            Console.WriteLine("SAIDA FINAL");
            Console.WriteLine(current.fitness.Select(a=>a.Item2).Distinct().Count());
            Console.WriteLine(String.Join("\n", current.fitness.Take(100).Select(a=>a.Item2)));
            return current.population[current.fitness[0].Item1].Skip(1).Select(p => (BadgeTypes) Enum.Parse(typeof(BadgeTypes), p.ToString())).ToList();
        }
        
    }
    
    

}
