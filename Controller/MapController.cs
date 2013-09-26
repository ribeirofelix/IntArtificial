using Controller.Properties;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class MapController
    {
        private const int mapLength = 42 * 42;
        private int[] posAshBdg;
        private int[][] distMap =  new int[9][] ; 

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

        private void ChangeRoute()
        {
            //call genetic
            //call astar
        }

        private void UpdateDistances()
        {
            var aStar = new AStar(42 * 42, this._kantoMap);
            int totalCost ;
            for (int i = 0; i < this.posAshBdg.Length; i++)
            {
                distMap[i][i] = 0;
                for (int j = i+1; j < this.posAshBdg.Length; j++)
                {
                    aStar.Star(posAshBdg[i], posAshBdg[j], mapLength, this._kantoMap, out totalCost);
                    distMap[i][j] = totalCost;
                    distMap[j][i] = totalCost;
                }
            }

        }

    }
}
