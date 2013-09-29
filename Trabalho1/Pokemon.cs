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
        private PokemonTypes _type;

        private Image _pokeImage;

        public Image PokeImage
        {
            get 
            {
                if (_pokeImage == null)
                {
                    if (_type == PokemonTypes.Electric) return Resources.pokeelectric;
                    if (_type == PokemonTypes.Fire) return Resources.pokefire;
                    if (_type == PokemonTypes.Flying) return Resources.pokeflying;
                    if (_type == PokemonTypes.Grass) return Resources.pokegrass;
                    if (_type == PokemonTypes.Water) return Resources.pokewater;

                }
                 
                return _pokeImage; 
            }
            
        }

         /* Constructor of Pokemon class
         * Parameters: 
         *  type - type of pokemon
         */

        public Pokemon(char type)
        {
            switch (type)
            {
                case 'G': _type = PokemonTypes.Grass; break;
                case 'W': _type = PokemonTypes.Water; break;
                case 'A': _type = PokemonTypes.Flying; break;
                case 'E': _type = PokemonTypes.Electric; break;
                case 'F': _type = PokemonTypes.Fire; break;
                default: break;
            }


        }

        public override string ToString()
        {
            return Enum.GetName(typeof(PokemonTypes), _type);
        }
    }
}
