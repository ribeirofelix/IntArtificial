using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Ash
    {

        //image
        private int xPoint;
        private Dictionary<PokemonTypes, bool> _pokemons = new Dictionary<PokemonTypes, bool>(5);

        public Ash()
        {

            foreach (PokemonTypes i in Enum.GetValues(typeof(PokemonTypes)))
                _pokemons.Add(i, false);

        }

        public int X
        {
            get { return xPoint; }
            set { xPoint = value; }
        }

        private int yPoint;
        public int Y
        {
            get { return yPoint; }
            set { yPoint = value; }
        }


        
    }
}
