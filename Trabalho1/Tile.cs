﻿using Model.Properties;
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
       
        private int _cost;
        public int TileCost 
        {
            get 
            {
                switch (this._type)
                {
                    case TileTypes.Water:
                        if (this.MapContent.Ash.HasPokemon(PokemonTypes.Water))
                            return _cost / 10;
                        break;
                    case TileTypes.Cave:
                        if (this.MapContent.Ash.HasPokemon(PokemonTypes.Electric))
                            return _cost / 10;
                        break;
                    case TileTypes.Mountain:
                        if (this.MapContent.Ash.HasPokemon(PokemonTypes.Flying))
                            return _cost / 10;
                        break;
                    case TileTypes.Volcano:
                        if (this.MapContent.Ash.HasPokemon(PokemonTypes.Fire))
                            return _cost / 10;
                        break;
                    default:
                        return _cost;
                }
                return _cost;
            }
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

        public Tile(char type,int xPoint, int yPoint , Map map )
        {
            _xPoint = xPoint;
            _yPoint = yPoint;
            Elem = PokeElem.None;
            this.MapContent = map;
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
