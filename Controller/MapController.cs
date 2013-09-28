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

        public MapController()
        {
            _kantoMap = new Map(Resources.mapPath, Resources.pokePath);
            posAshBdg = _kantoMap.ashAndBdgsPos; 
        }


        private Helper.Point[] posAshBdg;
        
        private int[][] distMap =  Enumerable.Repeat<int[]>(new int[9],9).ToArray() ;
        public int[][] DistMap
        {
            get{ return distMap; }
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
        



        private void ChangeRoute()
        {
            //call genetic
            //call astar
        }

        public void UpdateDistances()
        {
            var aStar = new AStar(this._kantoMap);
            int totalCost ;
            for (int i = 0; i < this.posAshBdg.Length; i++)
            {
                distMap[i][i] = 0;
                for (int j = i+1; j < this.posAshBdg.Length; j++)
                {
                    aStar.Star(posAshBdg[i], posAshBdg[j], out totalCost);
                    distMap[i][j] = totalCost;
                    distMap[j][i] = totalCost;
                }
            }

        }

    }
}
