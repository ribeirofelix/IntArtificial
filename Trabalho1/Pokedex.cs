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

        public Dictionary<Helper.Point, Pokemon> getPokemons (){

            var _pokemonsperarea = new Dictionary<Helper.Point, Pokemon>();

            for (int x = _map.AshIndex.x - 4; x < _map.AshIndex.x + 4; x++)
                for (int y = _map.AshIndex.y - 4; y < _map.AshIndex.y + 4; y++)
                {
                    Pokemon pokemon = _map.GetTile(x, y).Pokemon;
                    if (pokemon != null)
                        _pokemonsperarea.Add(new Helper.Point(x,y), pokemon);
                }

            return _pokemonsperarea;
        }
    }
}
