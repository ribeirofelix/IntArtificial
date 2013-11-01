#define TEST
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
    public delegate void PrintHurt(bool isHurt);


    public class Ash
    {
        private Dictionary<PokemonTypes, bool> _pokemons = new Dictionary<PokemonTypes, bool>(5);
        private Image _ashImage;
        private int pokeCount = 0;
        public int PokeCount
        {
            get { return pokeCount; }
            set { pokeCount = value; }
        }

        private bool hurtedPokemons ;
        private int totalCost ;

        public int TotalCost { get { return totalCost; } }

        public CostChangedDelegate listenersCost;
        public ShowPokemon showPoke;
        public PrintHurt printHurt;


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
#if !TEST
             showPoke(Map.Instance.GetTile(this.Pos).Pokemon );
#endif
             Map.Instance.GetTile(this.Pos).Pokemon = null;
            this.pokeCount++;
            this.totalCost -= 5;
#if !TEST
            listenersCost(totalCost);
#endif
           
            
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
#if !TEST
            listenersCost(totalCost);
#endif
        }

        public void Step()
        {
            this.totalCost--;
#if !TEST
            listenersCost(totalCost);
            printHurt(ManagedProlog.Prolog.IsHurt());
#endif
        }

        public void HealPokemons()
        {
            totalCost -= 10;
            hurtedPokemons = true;
#if !TEST
            listenersCost(totalCost);
#endif
        }

        public void Battle(bool win, Helper.Point trnPoint )
        {
            if (!win)
                totalCost -= 1000;
            else
            {
                totalCost += 150;
                Map.Instance.GetTile(trnPoint).Elem = PokeElem.None;
            }
#if !TEST
            listenersCost(totalCost);
#endif
        }

        public void BuyPokeballs()
        {
            totalCost -= 10;
#if !TEST
            listenersCost(totalCost);
#endif
        }
        
    }
}
