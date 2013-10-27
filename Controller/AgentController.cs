using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using Model;
using System.Security.Cryptography;
using ManagedProlog;

namespace Controller
{
    public class AgentController
    {
      
        public MapController mapCont;
    
        public Pokedex pokedex;
       
        public static int currentCost;
       

        public int PathCost { get { return mapCont._actualpathcost; } }

        private static System.Timers.Timer aTimer;

        public AgentController(MapController mapController)
        {
            unsafe { 
                updatePerceptions(Map.Instance.Ash.Pos);
            }
            mapCont = mapController;   
            pokedex = new Pokedex(Map.Instance); 

        }

        public void Walk()
        {
            for (int i = 0; i < 100; i++)
            {
                Helper.Action act;
                unsafe
                {
                    act = Helper.GetAction(Prolog.BestMove());
                }


                switch (act.move)
                {
                    case BestMove.Launch: mapCont.Ash.Pokeball(); break;
                    case BestMove.Heal:
                        break;
                    case BestMove.Buy:
                        break;
                    case BestMove.Battle:
                        break;
                    case BestMove.Move: mapCont.StepAsh(act.point, true); updatePerceptions(act.point); break;
                    case BestMove.TurnRight: mapCont.TurnAsh('L'); break;
                    case BestMove.TurnLeft: mapCont.TurnAsh('R'); break;
                    default:
                        break;
                }
            }
           
           
           
            

        }

        private void updatePerceptions(Helper.Point from)
        {
            Tile up = Map.Instance.GetTile(new Helper.Point(from.x - 1, from.y));
            Tile down = Map.Instance.GetTile(new Helper.Point(from.x + 1, from.y));
            Tile left = Map.Instance.GetTile(new Helper.Point(from.x, from.y - 1));
            Tile right = Map.Instance.GetTile(new Helper.Point(from.x, from.y + 1));


            bool hasPerfum = false , hasScreamT = false , hasScreamS = false , hasPokemon = false ;
            string pokeName = "P";
            // joy perfum ?
            if ( (up != null && up.hasPokeCenter) || (down != null && down.hasPokeCenter)
                || (left != null && left.hasPokeCenter) || (right != null && right.hasPokeCenter) )
                hasPerfum = true;
           
            // joy mart ?
             if ( (up != null && up.hasMart) || (down != null && down.hasMart)
                || (left != null && left.hasMart) || (right != null && right.hasMart) )
                hasScreamS = true;
           
            // joy trainer ?
             if ( (up != null && up.hasTrainer) || (down != null && down.hasTrainer)
                || (left != null && left.hasTrainer) || (right != null && right.hasTrainer) )
                hasScreamT = true;

            if (Map.Instance.GetTile(from).HasPokemon)
            {
                //TODO : implementar esse to String 
                switch (Map.Instance.GetTile(from).Pokemon.Type)
                {
                    case PokemonTypes.Grass:pokeName = "bulbasaur";
                        break;
                    case PokemonTypes.Water:pokeName = "squirtle";
                        break;
                    case PokemonTypes.Flying: pokeName = "pidgey";
                        break;
                    case PokemonTypes.Electric:pokeName = "pikachu";
                        break;
                    case PokemonTypes.Fire:pokeName = "charizard";
                        break;
                    default:
                        break;
                }
                 
                hasPokemon = true;
            }

            unsafe
            {
                Prolog.UpdPerc(from.x,from.y,Helper.StrToSbt(pokeName),hasPerfum,hasScreamS,hasScreamT,hasPokemon);
            }


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
            mapCont.Ash.Pokeball();
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
    