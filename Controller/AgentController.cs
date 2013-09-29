using System;
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
        private const int pop = 100 ;        
        private const int popElt = 50 ;
        private const int popMut = 50 ;
        private const int generations = 5;

        private MapController mapCont = new MapController();
        private BRKGA genetic ; 
        private bool[] captBadges = new bool[8];
        private Pokedex pokedex;
        private Dictionary<BadgeTypes, Helper.Point[]> paths;

        public AgentController()
        {
            captBadges.Initialize();
            paths= mapCont.UpdateDistances();
            genetic = new BRKGA(9,pop,popElt,popMut,captBadges,mapCont.DistMap);
            pokedex = new Pokedex(mapCont.KantoMap);
            
        }

        public void Walk()
        {
            genetic.Evolve(generations);
            while (captBadges.Any(a => !a )) /* While we dont rule all badges */
            {
                foreach (var badg in genetic.GetChoice())
                {
                    foreach (var step in paths[badg])
                    {
                        mapCont.StepAsh(step);
                        /* For each pokemon that Ash hasnt */                    /* Here we say : the types of pokemons Ash hasnt */
                        foreach (var pokemon in pokedex.getPokemons().Where(p => !mapCont.Ash.HasPokemon( p.Value.Type ) ) )
                        {
                        }
                    }
                    this.captBadges[((int)badg)-1] = true;
                }

            }
            
        }

        private void DecideGotoPokemon(PokemonTypes poke)
        {
            if (mapCont.Ash.HasPokemon(poke))
                return;
        }


    }
}
    