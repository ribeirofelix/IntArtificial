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
      
        private TileTypes _type;
        public TileTypes TileType 
        {
            get { return _type; }
            set { _type = value; } 
        }     
      
        //private Image _titleImage;
        public Image TitleImage
        {
            get 
            {
                if (Pokemon != null)
                    return Pokemon.PokeImage;

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


        private Pokemon _pokemon ;
        public Pokemon Pokemon 
        {
            get { return _pokemon; }
            set { _pokemon = value; }
        }
        public bool HasPokemon { get { return _pokemon != null; } }

        
        private Badge _badge ;
        public Badge Badge 
        {
            get { return _badge; }
            set { _badge = value; }
        }
        public bool HasBadge { get { return _badge != null; } }

        private Ash _ash ;
        public Ash Ash 
        {
            get { return _ash; }
            set { _ash = value; }
        }
        public bool HasAsh { get { return _ash != null; } }
       
        private int _cost;
        public int TileCost 
        {
            get { return _cost; }
            set { _cost = value; }
        }


        private int _xPoint;
        public int XPoint
        {
            get { return _xPoint; }
            set { _xPoint = value; }
        }

        private int _yPoint;
	    public int YPoint
	    {
		    get { return _yPoint;}
		    set { _yPoint = value;}
	    }
	
        

        /* Constructor of Tile class
         * Parameter: type - type of pokemon
         */

        public Tile(char type,int xPoint, int yPoint )
        {
            _xPoint = xPoint;
            _yPoint = yPoint;
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


        override public string ToString()
        {
            return Enum.GetName(typeof(TileTypes), this.TileType);
        }
    }
}
