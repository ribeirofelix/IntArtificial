﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Security.Cryptography;

namespace Controller
{
    public class AgentController
    {
        private const int pop = 42*42 ;        
        private const int popElt = 500 ;
        private const int popMut = 300 ;
        private const int generations = 2000;

        private MapController mapCont = new MapController();
        private BRKGA genetic ; 
        private bool[] captBadges = new bool[8];
        private Pokedex pokedex;
        private Dictionary<Tuple<int, BadgeTypes>, Helper.Point[]> paths = new Dictionary<Tuple<int, BadgeTypes>, Helper.Point[]>(new CompGoTo());
        private int currentCost;

        public AgentController()
        {
            captBadges.Initialize();
            paths= mapCont.UpdateDistances();
            genetic = new BRKGA(9,pop,popElt,popMut,captBadges,mapCont.DistMap);
            pokedex = new Pokedex(mapCont.KantoMap);
            genetic.Evolve(generations);
            currentCost = genetic.BestFitness;
            
        }

        public bool[] Walk()
        {
            int ashPos = 0;
            while (captBadges.Any(a => !a )) /* While we dont rule all badges */
            {
                foreach (var badg in genetic.GetChoice())
                {
                    ashPos = GoFromTo(ashPos, badg);
                    captBadges[((int)badg) - 1] = true;
                    
                }

            }
            return captBadges;
        }

        private int GoFromTo(int from , BadgeTypes to)
        {
            foreach (var step in paths[new Tuple<int,BadgeTypes>(from,to)])
            {
                mapCont.StepAsh(step);
                
                 /* For each pokemon that Ash                        Here we say : the types of pokemons Ash hasnt (and  pokemons is not a grass pokemon) */
                foreach (var pokemon in pokedex.getPokemons().Where(p => !mapCont.Ash.HasPokemon(p.Value.Type) && p.Value.Type != PokemonTypes.Grass))
                {
                    if (DecideGotoPokemon(pokemon.Value))
                    {
                        var pathToPoke = VerifyPath(pokemon.Value);
                        if (pathToPoke != null)
                        {
                            foreach (var stepToPoke in pathToPoke)
                            {
                                mapCont.StepAsh(step);
                            }
                            this.mapCont.FightPokemon(pokemon.Value);
                        }
                    }
                }
                
            }
            return (int)to;
        }


        private bool DecideGotoPokemon(Pokemon poke)
        {
            switch (poke.Type)
            {
                case PokemonTypes.Grass:    return false ;
                case PokemonTypes.Flying:  
                case PokemonTypes.Water:    return true;
                case PokemonTypes.Electric:
                    if (!captBadges[(int)BadgeTypes.boulder - 1]) return true;
                    break;
                case PokemonTypes.Fire:
                    if (    !captBadges[(int)BadgeTypes.volcano-1] 
                        || (!captBadges[(int)BadgeTypes.boulder-1] && ! mapCont.Ash.HasPokemon(PokemonTypes.Electric) ) )
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        private List<Helper.Point> VerifyPath(Pokemon  possiblePoke)
        {
            /* Set possible position to verify the path*/
            mapCont.Ash.Pokeball(possiblePoke.Type);
            var currPosAsh = mapCont.Ash.Pos;
            mapCont.StepAsh(possiblePoke.Pos);

            /* Recalculate all distances and the cost to travel */
            var tempPaths = this.mapCont.UpdateDistances();
            
            var tempGen = new BRKGA(captBadges.Count(a => !a) + 1, pop, popElt, popMut, captBadges, mapCont.DistMap);
            tempGen.Evolve(generations);

            /* AStart to know the cost from Ash to possible Pokemon */
            int  ashPossCost ;
            var pathToPokemon =  (new AStar(this.mapCont.KantoMap)).Star(currPosAsh, possiblePoke.Pos, out ashPossCost).ToList();

            var tempCost = tempGen.BestFitness + ashPossCost ;

            /* Remove the possible settings : back to the "REAL" life */
            mapCont.Ash.ReleasePokemon(possiblePoke.Type);
            mapCont.StepAsh(currPosAsh);

            if (currentCost > tempCost)
            {
                currentCost = tempCost;
                paths = tempPaths;
                genetic = tempGen;
                return pathToPokemon;
            }

            return null;

        }

        private class CompGoTo : IEqualityComparer<Tuple<int,BadgeTypes>> 
        {

            public bool Equals(Tuple<int,BadgeTypes> x, Tuple<int,BadgeTypes> y)
            {
                if(x.Item1 == y.Item1 && x.Item2 == x.Item2)
                    return true;
                return false;
            }

            public int GetHashCode(Tuple<int,BadgeTypes> obj)
            {
 	            return obj.Item2.GetHashCode() + obj.Item1.GetHashCode();
            }
        }

    }
}
    