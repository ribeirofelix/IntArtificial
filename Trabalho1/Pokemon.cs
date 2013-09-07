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
                    _pokeImage = Resources.Pikachu;
                return _pokeImage; 
            }
            
        }


        /* Constructor of Pokemon class
         * Parameter: type - type of pokemon
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
