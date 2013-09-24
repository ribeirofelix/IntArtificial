using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class AStar
    {

        /* */
        private int[] dist;
        private int posIni;
        private int[] path;

        public AStar(int qtdNodes )
        {
            /* initialize the array with infinity distance */
            dist = Enumerable.Repeat(int.MaxValue, qtdNodes).ToArray() ;
        }

        public void Calculate(int posIni)
        {
            dist[posIni] = 0;
            
        }

        public double StraightLineDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Sqrt((x1 - x2)^2 + (y1 - y2)^2);
            
        }
    }
}
