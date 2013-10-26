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
        private Dictionary<PokemonTypes, bool> _pokemons = new Dictionary<PokemonTypes, bool>(5);
        private Image _ashImage;

        public Direction direcition { get; set; }

        public Image AshImage
        {
            get
            {
                if (_ashImage == null)
                    _ashImage = Resources.ash;
                return _ashImage;
            }

        }

        public Ash()
        {

            foreach (PokemonTypes i in Enum.GetValues(typeof(PokemonTypes)))
                _pokemons.Add(i, false);
            direcition = Direction.South;

        }

        public bool HasPokemon(PokemonTypes poke)
        {
            return _pokemons[poke];
        }

        public Helper.Point Pos { get; set; }


        public void Pokeball(PokemonTypes poke)
        {
            this._pokemons[poke] = true;
        }

        public void ReleasePokemon(PokemonTypes poke)
        {
            this._pokemons[poke] = false;
        }

        
    }
}
