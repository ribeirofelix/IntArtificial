using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Properties;

namespace Model
{
    public class Ash
    {

        //image
        private int xPoint;
        private Dictionary<PokemonTypes, bool> _pokemons = new Dictionary<PokemonTypes, bool>(5);
        private Image _ashImage;

        public Image AshImage
        {
            get
            {
                if (_ashImage == null)
                    _ashImage = Resources.Pikachu;
                return _ashImage;
            }

        }

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
