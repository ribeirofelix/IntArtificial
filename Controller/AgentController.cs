using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using Model;
using System.Security.Cryptography;
using ManagedProlog;

namespace Controller
{
    public class AgentController
    {
      
        public MapController mapCont;
    
        public Pokedex pokedex;
       
        public static int currentCost;
       

        public int PathCost { get { return mapCont._actualpathcost; } }

        private static System.Timers.Timer aTimer;

        public AgentController(MapController mapController)
        {
            unsafe { 
                updatePerceptions(Map.Instance.Ash.Pos);
            }
            mapCont = mapController;   
            pokedex = new Pokedex(Map.Instance); 

        }

        public void Walk()
        {
            for (int i = 0; i < 100; i++)
            {
                Helper.Action act;
                unsafe
                {
                    act = Helper.GetAction(Prolog.BestMove());
                }


                switch (act.move)
                {
                    case BestMove.Launch: mapCont.Ash.Pokeball(); break;
                    case BestMove.Heal: mapCont.Ash.HealPokemons(); break;
                    case BestMove.Buy: mapCont.Ash.BuyPokeballs(); break;
                    case BestMove.Battle: mapCont.Ash.Battle(act.win);  break;
                    case BestMove.Move: mapCont.StepAsh(act.point); updatePerceptions(act.point); break;
                    case BestMove.TurnRight: 
                    case BestMove.TurnLeft: 
                        mapCont.TurnAsh(act.move); break;
                    default:
                        break;
                }
            }
           
           
           
            

        }

        private void updatePerceptions(Helper.Point from)
        {
            Tile up = Map.Instance.GetTile(new Helper.Point(from.x - 1, from.y));
            Tile down = Map.Instance.GetTile(new Helper.Point(from.x + 1, from.y));
            Tile left = Map.Instance.GetTile(new Helper.Point(from.x, from.y - 1));
            Tile right = Map.Instance.GetTile(new Helper.Point(from.x, from.y + 1));


            bool hasPerfum = false , hasScreamT = false , hasScreamS = false , hasPokemon = false ;
            string pokeName = "P";
            // joy perfum ?
            if ( (up != null && up.hasPokeCenter) || (down != null && down.hasPokeCenter)
                || (left != null && left.hasPokeCenter) || (right != null && right.hasPokeCenter) )
                hasPerfum = true;
           
            // joy mart ?
             if ( (up != null && up.hasMart) || (down != null && down.hasMart)
                || (left != null && left.hasMart) || (right != null && right.hasMart) )
                hasScreamS = true;
           
            // joy trainer ?
             if ( (up != null && up.hasTrainer) || (down != null && down.hasTrainer)
                || (left != null && left.hasTrainer) || (right != null && right.hasTrainer) )
                hasScreamT = true;

            if (Map.Instance.GetTile(from).HasPokemon)
            {
                pokeName = Map.Instance.GetTile(from).Pokemon.ToString();                 
                hasPokemon = true;
            }

            unsafe
            {
                Prolog.UpdPerc(from.x,from.y,Helper.StrToSbt(pokeName),hasPerfum,hasScreamS,hasScreamT,hasPokemon);
            }


        }

   
    

   

    }
}
    