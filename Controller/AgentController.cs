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
using System.IO;

namespace Controller
{
    public class AgentController
    {
      
        public MapController mapCont;

       
        public AgentController(MapController mapController)
        {
            updatePerceptions(Map.Instance.Ash.Pos);
            mapCont = mapController;   
        }

        public void Walk()
        {
            var aStar = new AStar(Map.Instance);
            while (Map.Instance.Ash.PokeCount <= 149)
            {
                Helper.Action act;
                unsafe { act = Helper.GetAction(Prolog.BestMove()); }

                int totalCost;
                switch (act.move)
                {
                    case BestMove.Launch: mapCont.Ash.Pokeball(); break;
                    case BestMove.Heal: mapCont.Ash.HealPokemons(); break;
                    case BestMove.Buy: mapCont.Ash.BuyPokeballs(); break;
                    case BestMove.Battle:

                        if (act.win == false)
                        {
                            Prolog.Trainers();
                            mapCont.sw.Close();
                            throw new Exception(act.point.ToString());
                        }
                        else
                        mapCont.Ash.Battle(act.win,act.point); 
                        break;
                    case BestMove.Move: updatePerceptions(act.point);  mapCont.StepAsh(act.point); break;
                    case BestMove.AStar :
                        var path = aStar.Star(Map.Instance.Ash.Pos, act.point, out totalCost) ;
                        mapCont.AshFromTo(path, updatePerceptions);
                        break;
                    case BestMove.TurnRight:
                    case BestMove.TurnLeft:
                        mapCont.TurnAsh(act.move); break;
                    case BestMove.Joker:
                        goto looser;
                        break;
                    case BestMove.KillGary:
                        var pathK = aStar.Star(Map.Instance.Ash.Pos, act.point, out totalCost) ;
                        //mapCont.Ash.Battle(true,act.point);
                        mapCont.AshFromTo(pathK, updatePerceptions);                        
                        Prolog.Trainers();
                        break;
                    case BestMove.GoPokeCenter:
                        var pathG = aStar.Star(Map.Instance.Ash.Pos, act.point, out totalCost) ;
                        mapCont.AshFromTo(pathG, updatePerceptions);
                      
                        break;
                    case BestMove.CatchPokemon:
                        var pathC = aStar.Star(Map.Instance.Ash.Pos, act.point, out totalCost) ;
                        mapCont.AshFromTo(pathC, updatePerceptions);
                        break;
                    default:
                        break;
                }

            }
looser: 

            Prolog.ScreamsT();
            Prolog.Trainers();
            Prolog.Pokemons();
            Prolog.Safes();
            mapCont.sw.Close();
            mapCont.Ash.listenerInfo(mapCont.Ash);
            Console.WriteLine(mapCont.Ash.TotalCost);

            
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
           
            // has mart ?
             if ( (up != null && up.hasMart) || (down != null && down.hasMart)
                || (left != null && left.hasMart) || (right != null && right.hasMart) )
                hasScreamS = true;
           
            // has trainer ?
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
    