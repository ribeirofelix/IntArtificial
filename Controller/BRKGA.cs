﻿using Model;
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
        private int currentGen;

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
            this.currentGen = 1;
            // Initialize and decode each chromosome of the current population, then copy to previous:
            // Allocate:
            current = new Population(n, pop);

            // Initialize:
            Initialize();
          //  Console.WriteLine("PRIMEIRO");
            //Console.WriteLine(current.fitness.Select(a => a.Item2).Distinct().Count());
            //Console.WriteLine(String.Join("\n", current.fitness.Take(100).Select(a => a.Item2)));
            

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
            if( current.population.Where( p => p.GroupBy(ch => ch ).Count() !=  captBadgs.Count(a => !a) + 1 ).Any() )
            {
                    throw new Exception("repetidos");
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
                this.currentGen++;
            }
        }
        // current 
        // previus
        // curr e prev na primeira iteracao sao iguais
        // chama evolution : pega a previous e mexe nela... colocando a proxima geracao! entao previus vai sera next!
        // depois chama a previous ( q tem o valor da next), passa pra current e a current vira a previous!
        private void Evolution(Population next)
        {
            int inx = 0;
            //var temRep = next.population.Where(p => p.GroupBy(c => c).Count() != 9);
            //Console.WriteLine(String.Join("\n", temRep));


            //var temRepCuur = current.population.Where(p => p.GroupBy(c => c).Count() != 9);
            //Console.WriteLine(String.Join("\n", temRepCuur));

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

                next.population[inx] = removeRep(next.population[inx]);

                //var histChild = new int[9];
                //histChild.Initialize();

                //var indsRepetidos = Enumerable.Repeat(-1, 4).ToArray();



                //for (int i = 0, j = 0; i < next.population[inx].Length; i++)
                //{
                //    int allelle = next.population[inx][i];
                //    histChild[allelle]++;
                //    if (histChild[allelle] > 1) /* repetido */
                //    {
                //        indsRepetidos[j] = i;
                //        j++;
                //    }
                //}


                //for (int i = 1, j = 0; i < current.population[eliteParent].Length; i++)
                //{
                //    var parnAll = current.population[eliteParent][i];
                //    try
                //    {
                //        if (!captBadgs[parnAll - 1] && histChild[parnAll] == 0)
                //        {
                //            next.population[inx][indsRepetidos[j]] = current.population[eliteParent][i];
                //            j++;
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        if (next.population[inx].GroupBy(nc => nc).All(gnc => gnc.Count() == 1))
                //            break;
                //        throw e;
                //    }

                //}

               // var popInx = next.population[inx].Select((p, i) => new ChroIndexed { allele = p, inx = i });
                
               
               //var ellite = current.population[eliteParent].Select( (pe,ix) => new ChroIndexed { allele = pe, inx = ix });
               // var cmp = new CompChromossome();
               // var dist = popInx.Distinct(cmp);
               // var rep = ellite.Except(popInx.Distinct(cmp), cmp).ToList();

               // var aTrocar = popInx.GroupBy(a => a.allele).Where(c => c.Count() > 1);
               // foreach (var item in aTrocar )
               // {
                    
                    
               //     var frsPrnt = rep.FirstOrDefault();
                    
               //     var primeiro = item.First().inx;
               //     next.population[inx][primeiro] = frsPrnt.allele;
               //     rep.RemoveAt(0);
               //     Console.WriteLine(item);
               // }

               // if (next.population[inx].GroupBy(j => j).Count() != 9)
               //     Console.WriteLine(String.Join(",", next.population[inx]));

               
                ++inx;
            }

            while (inx < popMutant)
            {
                var rand = new Random();

                    var vet = captBadgs.Select((b, i) => new { b, i }).Where(bd => !bd.b).Select(bi => bi.i).ToList();
                    next.population[inx][0] = 0;  /* onde o ash está */

                    for (int k = 1; k < n; ++k)
                    {
                        int index = rand.Next(0, n - k);
                        next.population[inx][k] = vet[index] + 1;
                        vet.RemoveAt(index);
                    }
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

        public int BestFitness
        {
            get { return current.getBestFitness(); }
        }

        private int[] removeRep(int[] chrom)
        {
            var valid = captBadgs.Select((b, i) => new { b, i }).Where(bi => bi.b == false).Select(bi => new { i = bi.i + 1, qtd = 0 }).ToDictionary(bdic => bdic.i, bele => bele.qtd);

            var repIndex = chrom.Select((c, i) => new { c, i }).GroupBy(d => d.c).Where(cnt => cnt.Count() > 1);


            foreach (var allelle in chrom.Skip(1))
            {
                valid[allelle]++;
            }


            foreach (var grRep in repIndex)
            {
                int frsOccu = grRep.First().i;
                var frstNotChoose = valid.Where(v => v.Value == 0).First();
                chrom[frsOccu] = frstNotChoose.Key;
                valid.Remove(frstNotChoose.Key);

            }
            return chrom;

        }

        
        private struct ChroIndexed
        {
            public int allele;
            public int inx;
        }

        private class CompChromossome : IEqualityComparer<ChroIndexed>
        {

            public bool Equals(ChroIndexed x, ChroIndexed y)
        {
            return x.allele == y.allele;
        }

            public int GetHashCode(ChroIndexed obj)
            {
                return obj.allele.GetHashCode() ;
            }
        }
        

    }
    
}
