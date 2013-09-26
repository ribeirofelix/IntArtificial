using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Sorting;

namespace Controller
{
    public class AStar
    {
        // 1 2 3 4 5
        // 6 7 8 9 10
        // 11 12 13 14
        private int[] dist;
        private int[] path;
        private bool[] vis;  /* true if vertex i has already been analyzed by the algorithm */
        private Map graph;

        protected struct Elem
        {
            public Elem(int accCost, int inx,int? parent = null)
            {
                this.accCost = accCost;
                this.index = inx;
                this.parent = parent;
            }
            public int accCost ;
            public int index;
            public int? parent ;
        }

        public AStar(int qtdNodes , Map graph )
        {
            /* initialize the array with infinity distance */
            dist = Enumerable.Repeat(int.MaxValue, qtdNodes).ToArray() ;
            path = Enumerable.Repeat(int.MinValue, qtdNodes).ToArray();
            this.graph = graph;
        }

        private void SetNewDistance (int v, int u, int w)
        {
            if (dist[u] > dist[v] + w)
                    {
                        /* we pass by v to get to u */
                        dist[u] = dist[v] + w;
                        path[u] = v;
                    }
        }

        public ICollection<int> Star(int posIni, int posFinal, int qtdNodes, Map graph , out int totalCost)
        {            
            var heapBorder = new Heap<Elem>();

            List<Elem> explored = new List<Elem>();    
            heapBorder.HeapAdd( h(posIni,posFinal), new Elem(0, posIni) );
            
            Tuple<int,int,Elem> first = null ;
            while (heapBorder.HeapSize() > 0 )
            {
                first = heapBorder.HeapExtractMin() ;
                if(first.Item3.index == posFinal)
                    break;
                
                foreach (var child in Neighborhood(first.Item3.index) )
	            {
                    int accChild = first.Item3.accCost + GetTileFromIndex(child).TileCost;
                    
                    if(! explored.Any(e => e.index == accChild ) )
                        heapBorder.HeapAdd(h(child, posFinal) + accChild , new Elem(accChild, child, first.Item3.index) );
                    
	            }

                explored.Insert( 0  , first.Item3);
            }
           
            int currParent = first.Item3.parent.Value ;
            totalCost = first.Item3.accCost;

            List<int> pathReturn = new List<int>();
            pathReturn.Insert(0, first.Item3.index);
           
            
            for (int i = 0 , j = 1; i < explored.Count; i++)
			{
                if (explored[i].index == currParent )
                {
                    pathReturn.Insert(j,explored[i].index);
                    j++;
                    currParent = explored[i].parent.HasValue ? explored[i].parent.Value : posIni  ;
                }
			}

            return pathReturn;

        }

        private int totalCost(int currPos, int posFinal)
        {
            return dist[currPos] + h(currPos, posFinal);
        }

        public int h(int posIni, int posFin)
        {
            int xIni = posIni % 42;
            int yIni = posIni / 42;
            int xFin = posFin % 42;
            int yFin = posFin / 42;

            return (int)Math.Sqrt( Math.Pow( (xIni - xFin) , 2)  + Math.Pow( (yIni - yFin) , 2) ) * 100;
        }

        private List<int> Neighborhood(int inx )
        {

            List<int> retInxs = new List<int>();
            var y = inx / 42;
            var x = inx % 42;

            if (x != 41)   /* not last line */
                retInxs.Add(inx + 42); /* x, y + 1*/                

            if (x != 0)   /* not first line */
                retInxs.Add(inx - 42); /* x, y - 1 */

            if (y != 41) /* not last column */
                retInxs.Add( inx + 1); /* x+1, y */
            

            if (y != 0) /* not first column */
                retInxs.Add(inx - 1); /* x-1, y */

            return retInxs;
        }

        private Tile GetTileFromIndex(int inx)
        {
            return graph.GetTile(inx / 42, inx % 42);
        }
    
    }
}
