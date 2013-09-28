using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Pokedex
    {
        private Map _map;

        public Pokedex(Map map)
        {
            _map = map;
        }

        public ICollection<Pokemon> getPokemons (){

            var _pokemonsperarea = new List<Pokemon>();

            for (int x = _map.AshIndex.x - 4; x < _map.AshIndex.x + 4; x++)
                for (int y = _map.AshIndex.y - 4; y < _map.AshIndex.y + 4; y++)
                {
                    Pokemon pokemon = _map.GetTile(x, y).Pokemon;
                    if (pokemon != null)
                        _pokemonsperarea.Add(pokemon);
                }

            return _pokemonsperarea;
        }
    }
}
