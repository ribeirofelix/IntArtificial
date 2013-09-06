using System;//
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    public enum PokemonTypes
    {
        Fire,
        Grass,
        Water,
        Electric,
        Flying
    }

    class Pokemon
    {
        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }
        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        //image
        private PokemonTypes _type;
        private int _positionX;
        private int _positionY;

        public Pokemon(char type)
        {
            switch (type)
            {
                case 'F': _type = PokemonTypes.Fire; break;
                case 'G': _type = PokemonTypes.Grass; break;
                case 'W': _type = PokemonTypes.Water; break;
                case 'E': _type = PokemonTypes.Electric; break;
                case 'A': _type = PokemonTypes.Flying; break;
                default: break;
            }
        }
    }
}
