using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho1
{
    enum TileTypes
    {
        Grass,
        Water,
        Cave,
        Mountain,
        Volcano
    }

    class Tile
    {
        public Pokemon TilePokemon
        {
            get{ return _pokemon; }
            set{ _pokemon = value; }
        }

        public TileTypes TileType
        {
            get { return _type; }
            set { _type = value; }
        }

        public Badge TileBadge
        {
            get { return _badge; }
            set { _badge = value; }
        }
        
        public Ash TileAsh
        {
            get { return _ash; }
            set { _ash = value; }
        }

        private Pokemon _pokemon = null;
        private TileTypes _type;
        private Badge _badge = null;
        private Ash _ash = null;

        public Tile(char type)
        {
            switch (type)
            {
                case 'G': _type = TileTypes.Grass; break;
                case 'M': _type = TileTypes.Mountain; break;
                case 'L': _type = TileTypes.Volcano; break;
                case 'A': _type = TileTypes.Water; break;
                case 'C': _type = TileTypes.Cave; break;
                default: break;
            }
        }


    }
}
