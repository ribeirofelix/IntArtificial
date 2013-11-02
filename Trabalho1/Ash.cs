//#define TEST
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Properties;

namespace Model
{
    public delegate void UpdateInfos(Ash ash);
    public delegate void ShowPokemon(Pokemon poke);
    public delegate void PrintHurt(bool isHurt);


    public class Ash
    {
     
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
        public bool IsHurted { get { return hurtedPokemons; } }
        public int Pokeballs { get { return ManagedProlog.Prolog.Pokeballs(); } }

        public UpdateInfos listenerInfo;
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
            direcition = Direction.South;
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
            listenerInfo(this);
            
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
            listenerInfo(this);

        }

        public void Step()
        {
            this.totalCost--;
            listenerInfo(this);
        }

        public void HealPokemons()
        {
            totalCost -= 100;
            hurtedPokemons = true;

            listenerInfo(this);

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

            listenerInfo(this);

        }

        public void BuyPokeballs()
        {
            totalCost -= 10;

            listenerInfo(this);

        }
        
    }
}
