using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    /* Pokemon types enum */

    enum PokemonTypes
    {
        Grass,
        Water,
        Flying,
        Electric,
        Fire
    }

    class Pokemon
    {
        private PokemonTypes _type;

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
    }
}
