using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning;
using MachineLearning.model;
using Accord.Statistics.Filters;


namespace MachineLearning
{
    public class Pokedex
    {

        KNearestNeighbors<double[]> knnPoke;
        Normalization codebook;
        System.Data.DataTable discPokeMatrix;

        public Pokedex()
        {
            PokeContext ct = new PokeContext();
            var pokeList = ct.Pokemons.ToList();
            var pokeMatrix = pokeList.Select(p => new string[] { p.Body.ToString() , p.Color , p.Habitat , p.Ability1 , p.Ability2 , p.Exp }).ToArray();
            var pokeClasses = pokeList.Select(p => (int)p.Type ).ToArray();


            discPokeMatrix = new System.Data.DataTable();

          
            discPokeMatrix.Columns.Add("Body");
            discPokeMatrix.Columns.Add("Color");
            discPokeMatrix.Columns.Add("Habitat");
            discPokeMatrix.Columns.Add("Ability1");
            discPokeMatrix.Columns.Add("Ability2");
            discPokeMatrix.Columns.Add("Exp");

            foreach (var poke in pokeMatrix)
            {
                discPokeMatrix.Rows.Add(poke[0] ?? "0", poke[1] ?? "0", poke[2] ?? "0", poke[3] ?? "0", poke[4] ?? "0", poke[5] ?? "0");
            }

            codebook = new Accord.Statistics.Filters.Normalization();
            
            var codPokemon = new double[718][];

            discPokeMatrix = codebook.Apply(discPokeMatrix);
            //int i = 0;
            //foreach (var poke in pokeMatrix)
            //{
            //    codPokemon[i] = codebook.Translate(poke[0], poke[1], poke[2], poke[3], poke[4], poke[5]).Select(k => Convert.ToDouble(k)).ToArray();
            //    i++;
            //}

            for (int i = 0; i < 718; i++)
            {
                codPokemon[i] = discPokeMatrix.Rows[i].ItemArray.Select(o => (double)o).ToArray();
            }
            knnPoke = new KNearestNeighbors<double[]>(1, codPokemon, pokeClasses, Accord.Math.Distance.Hamming);
        }

        public bool Classify(Pokemon poke, out int res)
        {
            res = knnPoke.Compute(  GetPokeAttr(poke) );
            return (res == (int)poke.Type);
        }        

        private double[] GetPokeAttr(Pokemon p) 
        {
            System.Data.DataTable tab = new System.Data.DataTable();

            tab.Columns.Add("Body");
            tab.Columns.Add("Color");
            tab.Columns.Add("Habitat");
            tab.Columns.Add("Ability1");
            tab.Columns.Add("Ability2");
            tab.Columns.Add("Exp");

            tab.Rows.Add(discPokeMatrix.Rows[p.Id]);
            return codebook.Apply(tab).Rows[0].ItemArray.Select(o => Convert.ToDouble(o)).ToArray();
        }

       
    }
}
