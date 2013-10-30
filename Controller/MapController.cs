//#define TEST
using Controller.Properties;
using ManagedProlog;
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

        private bool turned(Helper.Point prev, Helper.Point next)
        {
            return (prev.x != next.x && prev.y != next.y);           
        }

        private BestMove TurnTo(Direction facing, Helper.Point curr, Helper.Point next)
        {
            switch (facing)
            {
                case Direction.North:
                    if (curr.y > next.y)
                        return BestMove.TurnLeft;
                    else if (curr.y < next.y)
                        return BestMove.TurnRight;
                    else if (curr.x < next.x)
                        return BestMove.TurnBack;
                    else
                        return BestMove.Move;
                case Direction.South:
                    if (curr.y > next.y)
                        return BestMove.TurnRight;
                    else if (curr.y < next.y)
                        return BestMove.TurnLeft;
                    else if (curr.x > next.x)
                        return BestMove.TurnBack;
                    else
                        return BestMove.Move;
                case Direction.East:
                    if (curr.x > next.x)
                        return BestMove.TurnLeft;
                    else if (curr.x < next.x)
                        return BestMove.TurnRight;
                    else if (curr.y > next.y)
                        return BestMove.TurnBack;
                    else
                        return BestMove.Move;
                case Direction.West:
                    if (curr.x > next.x)
                        return BestMove.TurnRight;
                    else if (curr.x < next.y)
                        return BestMove.TurnLeft;
                    else if (curr.y < next.y)
                        return BestMove.TurnBack;
                    else
                        return BestMove.Move;
                default: return BestMove.Move;
            }
        }

        public void StepAsh(Helper.Point point)
        {

            Helper.Point oldIndex = Map.Instance.AshIndex;
            Map.Instance.AshIndex = point;

            if (!oldIndex.Equals(point))
            {
                DecidePutElems(point);
                Ash.Step();
                Map.Instance.GetTile(point).Status = TileState.Visited;
                foreach (var safeNgh in point.SafeNeighborhood().Where(p => !p.Equals(oldIndex)) ) 
                {
                    Map.Instance.GetTile(safeNgh).Status = TileState.Safe;
                }
                
#if !TEST
                listenersAsh(point,Ash.direcition);
#endif
            }
        }

        public void AshFromTo(ICollection<Helper.Point> path,Action<Helper.Point> updPerc)
        {
            Helper.Point curr = Map.Instance.AshIndex;

            foreach (var step in path)
            {
                switch (TurnTo(Ash.direcition,Map.Instance.AshIndex,step))
                {                    
                    case BestMove.TurnRight:
                       TurnAsh(BestMove.TurnRight);
                        break;
                    case BestMove.TurnLeft:
                         TurnAsh(BestMove.TurnLeft);
                         break;
                    case BestMove.TurnBack:
                             TurnAsh(BestMove.TurnLeft);
                             TurnAsh(BestMove.TurnLeft);
                         break;
                }
                Prolog.RemoveSafe(step.x, step.y);
                updPerc(step);
                this.StepAsh(step);
            }
            Helper.UpdFacing(Ash.direcition);
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
