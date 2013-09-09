using Model.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tile
    {
        public Pokemon TilePokemon { get; set; }

        private TileTypes _type;
        public TileTypes TileType { get { return _type; } set { _type = value; } }

        public Badge TileBadge { get; set; }

        public Ash TileAsh { get; set; }

        public int TileCost { get; set; }

        private Image _titleImage;

        public Image TitleImage
        {
            get 
            {
                if (TilePokemon != null)
                    return TilePokemon.PokeImage;

                switch (TileType)
                {
                    case TileTypes.Grass:
                        return Resources.grass;
                    case TileTypes.Water:
                        return Resources.water;
                    case TileTypes.Cave:
                        return Resources.cave;
                    case TileTypes.Mountain:
                        return Resources.mountain;
                    case TileTypes.Volcano:
                        return Resources.lava;
                    default:
                        return Resources.grass;

                };
    
            }
            
        }


        private Pokemon _pokemon = null;
        private Badge _badge = null;
        private Ash _ash = null;
        private int _cost;

        /* Constructor of Tile class
         * Parameter: type - type of pokemon
         */

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


        public string ToString()
        {
            return Enum.GetName(typeof(TileTypes), this.TileType);
        }
    }
}
