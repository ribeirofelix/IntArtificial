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

        public MapController()
        {
            _kantoMap = new Map(Resources.mapPath, Resources.pokePath);
            posAshBdg = _kantoMap.ashAndBdgsPos; 
        }

        private const int mapLenght = 42 * 42;
        private int[] posAshBdg;
        
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
        


        public void UpdateDistances()
        {
            var aStar = new AStar(42 * 42, this._kantoMap);
            int totalCost ;
            for (int i = 0; i < this.posAshBdg.Length; i++)
            {
                distMap[i][i] = 0;
                for (int j = i+1; j < this.posAshBdg.Length; j++)
                {
                    aStar.Star(posAshBdg[i], posAshBdg[j], mapLenght, this._kantoMap, out totalCost);
                    distMap[i][j] = totalCost;
                    distMap[j][i] = totalCost;
                }
            }

        }


    }
}
