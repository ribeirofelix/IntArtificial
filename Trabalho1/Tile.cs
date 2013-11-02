using Model.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public delegate void UpdateView();
  

    public class Tile
    {
        public UpdateView listUpdView;

        private Map MapContent;
      
        private TileTypes _type;
        public TileTypes TileType 
        {
            get { return _type; }
            set { _type = value; }
        }

        private PokeElem _pokeElem;
        public PokeElem Elem
        {
            get { return _pokeElem; }
            set { _pokeElem = value; }
        }

        private TileState status;

        public TileState Status
        {
            get { return status; }
            set 
            {
                if (value == TileState.Safe && ManagedProlog.Prolog.IsVisited(this.XPoint, this.YPoint) )
                    status = TileState.VisitedSafe;
                else
                    status = value;
            }
        }

        public Image StatusImg
        {
            get
            {
                switch (Status)
                {
                    case TileState.Visited: return Resources.visited;
                    case TileState.Safe: return Resources.safe;
                    case TileState.VisitedSafe: return Resources.visitedsafe;
                    default: return null;
                }
            }
        }
        
        public Image TileBackgroud
        {
            get 
            {
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
                }               
            }
            
        }

        public Image PokeElemImag
        {
            get
            {
                switch (Elem)
                {
                    case PokeElem.PokeCenter:
                        return Resources.pokecenter;
                    case PokeElem.Mart:
                        return Resources.mart;
                    case PokeElem.Trainer:
                        return Resources.gary;
                    default: return null;
                }
            }
        }

        private Pokemon _pokemon ;
        public Pokemon Pokemon 
        {
            get { return _pokemon; }
            set 
            { 
                _pokemon = value;
                if(_pokemon != null)
                    _pokemon.Pos = new Helper.Point(this.XPoint, this.YPoint);
            }
        }

        public bool HasPokemon { get { return _pokemon != null; } }


        public bool hasPokeCenter { get { return this.Elem == PokeElem.PokeCenter ; } }

        public bool hasMart { get { return this.Elem == PokeElem.Mart; } }

        public bool hasTrainer { get { return this.Elem == PokeElem.Trainer; } }
        
       
        public Ash Ash 
        {
            get { return HasAsh ? this.MapContent.Ash : null ; }
        }
        public bool HasAsh { get { return (this.MapContent.Ash.Pos.x == this.XPoint && this.MapContent.Ash.Pos.y == this.YPoint); } }
       
    
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

        public Tile(char type,int xPoint, int yPoint , Map map )
        {
            _xPoint = xPoint;
            _yPoint = yPoint;
            Elem = PokeElem.None;
            Status = TileState.Unknow;
            this.MapContent = map;
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


        override public string ToString()
        {
            return Enum.GetName(typeof(TileTypes), this.TileType);
        }
    }
}
