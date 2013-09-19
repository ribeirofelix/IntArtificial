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
        public AStar(int qtdNodes)
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


    }
}
