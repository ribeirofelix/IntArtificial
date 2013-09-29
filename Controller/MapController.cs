﻿using Controller.Properties;
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
        public Dictionary<BadgeTypes, Helper.Point[]> UpdateDistances()
        {
            var aStar = new AStar(this._kantoMap);
            int totalCost ;
            Dictionary<BadgeTypes,Helper.Point[]> paths = new Dictionary<BadgeTypes,Helper.Point[]>(8);

            for (int i = 0; i < this.posAshBdg.Length; i++)
            {
                distMap[i][i] = 0;
                for (int j = i+1; j < this.posAshBdg.Length; j++)
                {
                    if(i == 0)
                        paths.Add( (BadgeTypes) j , aStar.Star(posAshBdg[i], posAshBdg[j], out totalCost).ToArray() );
                    else
                        aStar.Star(posAshBdg[i], posAshBdg[j], out totalCost);
                    
                    distMap[i][j] = totalCost;
                    distMap[j][i] = totalCost;
                }
            }
            return paths;

        }

        /* Step Ash */

        public void StepAsh(Helper.Point point)
        {
            this._kantoMap.AshIndex = point;
            //custo do path += custo da tile nova
            listenersAsh(point); //observer
            listenersCost(/*custodopath*/0);
        }

        /* Launch Pokeball */

        public void LaunchPokeball(PokemonTypes poke)
        {
            this.KantoMap.GetTile(KantoMap.AshIndex.x, KantoMap.AshIndex.y).Ash.Gotcha(poke);
        }

        #endregion

    }
}
