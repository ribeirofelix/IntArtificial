﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using Model;
using System.Security.Cryptography;

namespace Controller
{
    public class AgentController
    {
        private const int pop = 200 ;        
        private const int popElt = 60 ;
        private const int popMut = 40 ;
        private const int generations = 5000;

        public MapController mapCont;
        public bool[] captBadges = new bool[8];
        public Pokedex pokedex;
      //  public Dictionary<Tuple<int, BadgeTypes>, Helper.Point[]> paths = new Dictionary<Tuple<int, BadgeTypes>, Helper.Point[]>(new CompGoTo());
        public static int currentCost;
        //private bool hasCapt = false;

        public int PathCost { get { return mapCont._actualpathcost; } }

        private static System.Timers.Timer aTimer;

        public AgentController(MapController mapController)
        {
            mapCont = mapController;
            captBadges.Initialize();
           // paths = mapCont.UpdateDistances();
           // genetic = new BRKGA(9, pop, popElt, popMut, captBadges, mapCont.DistMap);
            pokedex = new Pokedex(Map.Instance);
            //genetic.Evolve(generations);
            //currentCost = genetic.BestFitness;

        }

        //public bool[] Walk()
        //{
        //    int ashPos = 0;
        //    while (captBadges.Any(a => !a )) /* While we dont rule all badges */
        //    {
        //        foreach (var badg in genetic.GetChoice())
        //        {
        //            ashPos = GoFromTo(ashPos, badg);
        //            if (ashPos != 0)
        //            {
        //                captBadges[((int)badg) - 1] = true;
        //                Map.Instance.GetTile(Map.Instance.AshIndex).Badge = null;
        //            }
        //            else
        //                break;
                   
        //        }

        //    }
        //    return captBadges;
        //}

        //public int GoFromTo(int from , BadgeTypes to)
        //{
        //    bool hasCapt = false;
        //    foreach (var step in paths[new Tuple<int,BadgeTypes>(from,to)])
        //    {

        //        mapCont.StepAsh(step,true);

                
        //         /* For each pokemon that Ash                        Here we say : the types of pokemons Ash hasnt (and  pokemons is not a grass pokemon) */
        //        foreach (var pokemon in pokedex.getPokemons().Where(p => !mapCont.Ash.HasPokemon(p.Value.Type) && p.Value.Type != PokemonTypes.Grass ))
        //        {
        //            if (DecideGotoPokemon(pokemon.Value))
        //            {
        //                var pathToPoke = VerifyPath(pokemon.Value);
        //                if (pathToPoke != null)
        //                {
        //                    foreach (var stepToPoke in pathToPoke)
        //                    {
        //                        mapCont.StepAsh(stepToPoke, true);

        //                    }
        //                    this.mapCont.FightPokemon(pokemon.Value);
        //                    hasCapt = true;
        //                }
        //            }
        //            else
        //                pokemon.Value.IsVisited = true;
        //        }

        //        if (hasCapt)
        //        {
        //            return 0;
        //        }

        //    }
        //    return (int)to;
        //}

        //private void GoFromPointToBadge(Helper.Point pos, int badg)
        //{
        //    int finalCost;
        //    var path = (new AStar(Map.Instance)).Star(pos, Helper.GetBadgePoint(badg), out finalCost);
        //    foreach (var step in path)
        //    {
        //        mapCont.StepAsh(step, true);
        //    }
        //}


        //public bool DecideGotoPokemon(Pokemon poke)
        //{
        //    switch (poke.Type)
        //    {
        //        case PokemonTypes.Grass:    return false ;
        //        case PokemonTypes.Flying: return true;
        //        case PokemonTypes.Water:    return true;
        //        case PokemonTypes.Electric:
        //            if (!captBadges[(int)BadgeTypes.boulder - 1]) return true;
        //            break;
        //        case PokemonTypes.Fire:
        //            if (    !captBadges[(int)BadgeTypes.volcano-1] 
        //                || !captBadges[(int)BadgeTypes.boulder-1] ) /*&& ! mapCont.Ash.HasPokemon(PokemonTypes.Electric) ) )*/
        //            {
        //                return true;
        //            }
        //            break;
        //    }

        //    return false;
        //}

        public List<Helper.Point> VerifyPath(Pokemon  possiblePoke)
        {
            /* Set possible position to verify the path*/
            mapCont.Ash.Pokeball(possiblePoke.Type);
            var currPosAsh = mapCont.Ash.Pos;

            mapCont.StepAsh(possiblePoke.Pos,false);


            /* Recalculate all distances and the cost to travel */
           // var tempPaths = this.mapCont.UpdateDistances();
            
            //var tempGen = new BRKGA(captBadges.Count(a => !a) + 1, pop, popElt, popMut, captBadges, mapCont.DistMap);
            //tempGen.Evolve(generations);

            /* AStart to know the cost from Ash to possible Pokemon */
            int  ashPossCost ;
            var pathToPokemon =  (new AStar(Map.Instance)).Star(currPosAsh, possiblePoke.Pos, out ashPossCost).ToList();

           // var tempCost = tempGen.BestFitness + ashPossCost ;

            /* Remove the possible settings : back to the "REAL" life */
            mapCont.Ash.ReleasePokemon(possiblePoke.Type);

            mapCont.StepAsh(currPosAsh,false);

            //if (currentCost > tempCost)
            //{
            //    currentCost = tempCost;
            //    //paths = tempPaths;
            //    //genetic = tempGen;
            //    return pathToPokemon;
            //}

            return null;

        }

       

    }
}
    