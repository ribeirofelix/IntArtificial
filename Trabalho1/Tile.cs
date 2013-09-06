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

        public int TileCost
        {
            get { return _cost; }
            set { _cost = value; }
        }


        private Pokemon _pokemon = null;
        private TileTypes _type;
        private Badge _badge = null;
        private Ash _ash = null;
        private int _cost;

        public Tile(char type)
        {
            switch (type)
            {
                case 'G': _type = TileTypes.Grass; _cost = 10; break;
                case 'M': _type = TileTypes.Mountain; _cost = 120; break;
                case 'L': _type = TileTypes.Volcano; _cost = 150; break;
                case 'A': _type = TileTypes.Water; _cost = 100; break;
                case 'C': _type = TileTypes.Cave; _cost = 120; break;
                default: break;
            }
        }


    }
}
