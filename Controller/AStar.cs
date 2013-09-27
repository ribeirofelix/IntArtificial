using System;
using System.Collections.Generic;
using System.Diagnostics;
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
       // private bool[] vis;  /* true if vertex i has already been analyzed by the algorithm */
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
            var hasExpl = Enumerable.Repeat(false, qtdNodes).ToArray() ;
            heapBorder.HeapAdd( h(posIni,posFinal), new Elem(0, posIni) );

            Console.WriteLine(qtdNodes);
            int cont = 0;
            Tuple<int,int,Elem> first = null ;
            while (heapBorder.HeapSize() > 0 )
            {
                first = heapBorder.HeapExtractMin() ;
                if(first.Item3.index == posFinal)
                    break;
                
                foreach (var child in Neighborhood(first.Item3.index) )
	            {
                    int accChild = 0;
                    accChild = first.Item3.accCost + GetTileFromIndex(child).TileCost;
                    
                    if (child >= hasExpl.Length)
                        throw new Exception("in " + child + ";has :" + hasExpl.Length);
                    if( !hasExpl[child] )
                        heapBorder.HeapAdd( h(child, posFinal) + accChild , new Elem(accChild, child, first.Item3.index) );
                    
	            }

                explored.Insert( 0  , first.Item3);
                if (first.Item3.index >= hasExpl.Length)
                    throw new Exception("in " + first.Item3.index + ";has :" + hasExpl.Length);
                hasExpl[first.Item3.index] = true;

                cont++;               
            }

            /*for (int i = 0; i < explored.Count; i++)
            {
                Elem a = explored.ElementAt(i);
                int ind = a.index; 
                int[] xy = i2XY(ind);
                int c = a.accCost;
                Debug.WriteLine("x"+xy[0]+" ;y "+xy[1]+" custo: "+c);
            }*/
            Debug.WriteLine("explored");
            Debug.WriteLine(String.Join("\n", explored.Select(v => new { xy = i2XY(v.index) , v.accCost}  ).Select(e => "cost: " + e.accCost + "; "  + e.xy[0] + ";" + e.xy[1])));
            Debug.WriteLine("heap");
            while (heapBorder.HeapSize() > 0 )
            {
                var frst = heapBorder.HeapExtractMin();
                var xy = i2XY( frst.Item3.index);
                Debug.WriteLine(string.Format("cost: {0} ; {1} , {2} ", frst.Item1,xy[0] , xy[1]  ) );
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
                    Debug.WriteLine("custo "+explored[i].accCost);
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
            int xIni = posIni / 42;
            int yIni = posIni % 42;
            int xFin = posFin / 42;
            int yFin = posFin % 42;

            return (int)Math.Sqrt( Math.Pow( (xIni - xFin)*12, 2) + Math.Pow( (yIni - yFin)*12, 2) )  ;
        }

        private List<int> Neighborhood(int inx )
        {

            List<int> retInxs = new List<int>();
            var y = inx % 42;
            var x = inx / 42;

            if (x != 41)   /* not last line */
                retInxs.Add(inx + 42); /* x + 1, y */                

            if (x != 0)   /* not first line */
                retInxs.Add(inx - 42); /* x - 1, y */

            if (y != 41) /* not last column */
                retInxs.Add( inx + 1); /* x, y + 1 */
            

            if (y != 0) /* not first column */
                retInxs.Add(inx - 1); /* x, y - 1 */

            return retInxs;
        }

        private Tile GetTileFromIndex(int inx)
        {
            int x = inx / 42;
            int y = inx % 42;
            if (x >= 42 || y >= 42)
                throw new Exception("fora da matriz: " + x + " " + y);

            return graph.GetTile(x, y);
        }

        private int[] i2XY(int ix)
        {
            return new int[2] { ix / 42, ix % 42 };
        }

        private int XY2i(int x, int y)
        {
            return y + (x * 42);
        }
    
    }
}
