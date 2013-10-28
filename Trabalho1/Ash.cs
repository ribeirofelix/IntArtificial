using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Properties;

namespace Model
{
    public delegate void CostChangedDelegate(int newCost);
    public delegate void ShowPokemon(Pokemon poke);


    public class Ash
    {
        private Dictionary<PokemonTypes, bool> _pokemons = new Dictionary<PokemonTypes, bool>(5);
        private Image _ashImage;
        private int pokeCount = 0;

        private bool hurtedPokemons ;
        private int totalCost ;

        public CostChangedDelegate listenersCost;
        public ShowPokemon showPoke;


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


        /* Ash actions! */
        public void Pokeball()
        {
             showPoke(Map.Instance.GetTile(this.Pos).Pokemon );
             Map.Instance.GetTile(this.Pos).Pokemon = null;
            this.pokeCount++;
            this.totalCost -= 5;
            listenersCost(totalCost);
           
            
        }

        public void Turn(BestMove turnDir)
        {
            if (turnDir == BestMove.TurnRight)
            {
                switch (this.direcition)
                {
                    case Direction.North: this.direcition = Direction.East;
                        break;
                    case Direction.South: this.direcition = Direction.West;
                        break;
                    case Direction.East: this.direcition = Direction.South;
                        break;
                    case Direction.West: this.direcition = Direction.North;
                        break;
                }

            }
            else if(turnDir == BestMove.TurnLeft)
            {
                switch (this.direcition)
                {
                    case Direction.North: this.direcition = Direction.West;
                        break;
                    case Direction.South: this.direcition = Direction.East;
                        break;
                    case Direction.East: this.direcition = Direction.North;
                        break;
                    case Direction.West: this.direcition = Direction.South;
                        break;
                }
            }

            totalCost -= 1;
            listenersCost(totalCost);
        }

        public void Step()
        {
            this.totalCost--;
            listenersCost(totalCost);
        }

        public void HealPokemons()
        {
            totalCost -= 100;
            hurtedPokemons = true;
            listenersCost(totalCost);
        }

        public void Battle(bool win)
        {
            if (!win)
                totalCost -= 1000;
            else
            {
                totalCost += 150;
                Map.Instance.GetTile(Pos).Elem = PokeElem.None;
            }
            listenersCost(totalCost);
        }

        public void BuyPokeballs()
        {
            totalCost -= 10;
            listenersCost(totalCost);
        }
        
    }
}
