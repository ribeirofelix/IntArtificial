
using Controller.Properties;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Controller
{
    //delegates for observer pattern
    public delegate void AshMovedDelegate(Helper.Point point, Direction dir);
 
    public delegate void ShowPokemon(PokemonTypes poke);

    public class MapController
    {
        private static MapController instance;

        public static MapController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MapController();
                }
                return instance;
            }
        }
        #region /* PRIVATE PROPERTIES */

        private const int mapLength = 42 * 42;
        

        public int _actualpathcost = 0; //firststep

        private Helper.Point[] posAshBdg;

        private int[][] distMap = new int[9][];
        public int[][] DistMap
        {
            get { return distMap; }
        }

        #endregion

        #region /* PUBLIC PROPERTIES */

        public Ash Ash
        {
            get { return Map.Instance.Ash ; }
        }

        #endregion

        public AshMovedDelegate listenersAsh;
        
        public ShowPokemon showPoke;

        #region /* CONSTRUCTOR */


        private MapController()
        {
            /* Creates Map */


            posAshBdg = Map.Instance.ashAndBdgsPos;

            /*Creates distances matrix*/

            for (int i = 0; i < distMap.Length; i++)
            {
                distMap[i] = new int[9];
            }
            
        }

        #endregion

        #region /* PUBLIC METHODS */

       
        /* Step Ash */

        public void StepAsh(Helper.Point point)
        {

            Helper.Point oldIndex = Map.Instance.AshIndex;
            Map.Instance.AshIndex = point;

            if (!oldIndex.Equals(point))
            {
                DecidePutElems(point);
                Ash.Step();
#if !TEST
                listenersAsh(point,Ash.direcition);
#endif
            }
        }

        public void TurnAsh(BestMove dir)
        {
            Ash.Turn(dir);          
#if !TEST
            listenersAsh(Ash.Pos, Ash.direcition);
#endif
        }

        public void FightPokemon(Pokemon poke )
        {
            Ash.Pokeball();
            Map.Instance.GetTile(poke.Pos).Pokemon = null;
#if !TEST
           // showPoke(poke.Type);
#endif
        }

        private void DecidePutElems(Helper.Point pt)
        {
            switch (Map.Instance.GetTile(pt).Elem)
            {
                case PokeElem.PokeCenter: Helper.PutPokeCenter(pt.x, pt.y); break;
                case PokeElem.Mart: Helper.PutMart(pt.x, pt.y); break;
                case PokeElem.Trainer: Helper.PutTrainer(pt.x, pt.y); break;
            }
        }
        
        #endregion

    }
}
