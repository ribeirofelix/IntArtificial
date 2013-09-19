using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class AStar
    {

        private int[] dist;
        private int posIni;
        private int[] path;

        public AStar(int qtdNodes, int posIni)
        {
            this.posIni = posIni;
            dist = Enumerable.Repeat(int.MaxValue, qtdNodes).ToArray() ;
        }

        public void Calculate(int posIni)
        {
            var a = Enumerable.Repeat<int[]>(new int[10], 100).ToArray(); ;
            var tuple = Enumerable.Repeat<Tuple<int, int>>(new Tuple<int, int>(0, 0), 100);
            var tuples = new Tuple<int,int>[10] ;

            dist[posIni] = 0;

        }

        public double StraightLineDistance(int x1, int x2, int y1, int y2)
        {
            return (double) Math.Sqrt((x1 - x2)^2 + (y1 - y2)^2);
            
        }
    }
}
