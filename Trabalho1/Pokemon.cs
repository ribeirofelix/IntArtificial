using Model.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Pokemon
    {
       
        private Pokemons pokeName;
        public Pokemons PokeName
        {
            get { return pokeName; }
            set { pokeName = value; }
        }

        public bool IsVisited = false;

        public Image PokeImage
        {
            get 
            {

              //if (_type == PokemonTypes.Electric) return Resources.pokeelectric;
              //if (_type == PokemonTypes.Fire) return Resources.pokefire;
              //if (_type == PokemonTypes.Flying) return Resources.pokeflying;
              //if (_type == PokemonTypes.Grass) return Resources.pokegrass;
              //if (_type == PokemonTypes.Water) return Resources.pokewater;

              return Resources.ditto;
              
            }
            
        }

         /* Constructor of Pokemon class
         * Parameters: 
         *  type - type of pokemon
         */

        public Pokemon(Pokemons pokeName)
        {
            PokeName = pokeName;
        }

        public override string ToString()
        {
            return Enum.GetName(typeof(Pokemons), PokeName);
        }


        public Helper.Point Pos  { get; set; }
    }

}
