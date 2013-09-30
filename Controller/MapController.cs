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
    public delegate void AshMovedDelegate(Helper.Point point);
    public delegate void CostChangedDelegate(int newCost);

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

        private int _actualpathcost = -10; //firststep

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
        public CostChangedDelegate listenersCost;

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

        #region /* PRIVATE METHODS */

        private void ChangeRoute()
        {
            //call genetic
            //call astar
        }

        #endregion

        #region /* PUBLIC METHODS */

        /* Update Distances */
        public Dictionary<Tuple<int, BadgeTypes>, Helper.Point[]> UpdateDistances()
        {
            var aStar = new AStar(Map.Instance);
            int totalCost ;
            var paths = new Dictionary<Tuple<int, BadgeTypes>, Helper.Point[]>(8);

            for (int i = 0; i < this.posAshBdg.Length; i++)
            {
                distMap[i][i] = 0;
                for (int j = i+1; j < this.posAshBdg.Length; j++)
                {
                    var path = aStar.Star(posAshBdg[i], posAshBdg[j], out totalCost).ToArray();
                    paths.Add(new Tuple<int, BadgeTypes>(i, (BadgeTypes)j), path);
                    paths.Add(new Tuple<int, BadgeTypes>(j, (BadgeTypes)i), path.Reverse().ToArray() );
                    distMap[i][j] = totalCost;
                    distMap[j][i] = totalCost;
                }
            }
            return paths;

        }

        /* Step Ash */

        public void StepAsh(Helper.Point point, bool isReal)
        {

            Helper.Point oldIndex = Map.Instance.AshIndex;
            Map.Instance.AshIndex = point;
            if (isReal && !oldIndex.Equals(point))
            {
                Console.WriteLine(point.x.ToString() + ";" + point.y.ToString());
                _actualpathcost += Map.Instance.GetTile(point).TileCost;
                Console.WriteLine(_actualpathcost.ToString());
                listenersCost(_actualpathcost);
                listenersAsh(point);
            }
        }

        public void FightPokemon(Pokemon poke )
        {
            Ash.Pokeball(poke.Type);
            Map.Instance.GetTile(poke.Pos).Pokemon = null;
        }
        #endregion

    }
}
