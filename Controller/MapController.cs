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

        #region /* PRIVATE PROPERTIES */

        private const int mapLength = 42 * 42;
        private int _actualPathCost = 0;

        private Helper.Point[] posAshBdg;

        private int[][] distMap = new int[9][];
        public int[][] DistMap
        {
            get { return distMap; }
        }

        private Map _kantoMap;
        public Map KantoMap
        {
            get
            {
                if (_kantoMap == null)
                {
                    _kantoMap = new Map(Resources.mapPath, Resources.pokePath);
                    posAshBdg = _kantoMap.ashAndBdgsPos;
                }
                return _kantoMap;
            }
        }

        #endregion

        #region /* PUBLIC PROPERTIES */

        public Ash Ash
        {
            get { return this.KantoMap.Ash ; }
        }

        /*Delegates - observer*/

        public AshMovedDelegate listenersAsh;
        public CostChangedDelegate listenersCost;

        #endregion

        #region /* CONSTRUCTOR */

        public MapController()
        {
            /* Creates Map */
            _kantoMap = new Map(Resources.mapPath, Resources.pokePath);

            posAshBdg = _kantoMap.ashAndBdgsPos;
            
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
            var aStar = new AStar(this._kantoMap);
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
            this._kantoMap.AshIndex = point;

            if (isReal)
            {
                listenersAsh(point);
                _actualPathCost += _kantoMap.GetTile(point).TileCost;
                listenersCost(_actualPathCost);
            }
        }

        public void FightPokemon(Pokemon poke )
        {
            Ash.Pokeball(poke.Type);
            this._kantoMap.GetTile(poke.Pos).Pokemon = null;
        }


        #endregion

    }
}
