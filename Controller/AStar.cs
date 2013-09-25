using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

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

        //protected struct ElemTree
        //{
        //    public ElemTree(int elem , int cost )
        //    {
        //        this.elem =elem;
        //        this.cost = cost;
        //        this.parent = null;
        //    }
        //    int elem;
        //    int cost;
        //    int? parent;
        //};

        public AStar(int qtdNodes , Map graph )
        {
            /* initialize the array with infinity distance */
            dist = Enumerable.Repeat(int.MaxValue, qtdNodes).ToArray() ;
            path = Enumerable.Repeat(int.MinValue, qtdNodes).ToArray();
            this.graph = graph;
        }

        public void Dijkstra(int posIni, int qtdNodes )
        {
            int u, v, w, x, y;
            dist[posIni] = 0;
            path[posIni] = -1;
            vis = Enumerable.Repeat(false, qtdNodes).ToArray();

            while (true)
            {
                v = -1;     /* vertex that will be analyzed */
                y = 0;

                for (int i = 0; i < qtdNodes; i++)
                    if (!vis[i] && (v < 0 || dist[i] < dist[v]))
                        v = i;

                /* if no vertex is selected or if there's no path from posIni to v the algorithm ends */
                if (v < 0 || dist[v] == int.MaxValue) break;

                vis[v] = true;

                /* find variables x,y that relate to v */
                x = v / 42;
                y = v % 42;

                /* we check if v can be used as a path to other vertices */
                if (x != 41)   /* not last line */
                {
                    u = v + 42; /* x, y + 1*/                   /* vertex connected to v (edge v->u) */
                    w = graph.GetTile(x, y + 1).TileCost;    /* cost of edge v->u */
                    SetNewDistance(v, u, w);
                }
                if (x != 0)   /* not first line */
                {
                    u = v - 42; /* x, y - 1 */
                    w = graph.GetTile(x, y - 1).TileCost;
                    SetNewDistance(v, u, w);
                }
                if (y != 41) /* not last column */
                {
                    u = v + 1; /* x+1, y */
                    w = graph.GetTile(x + 1, y).TileCost;
                    SetNewDistance(v, u, w);
                }
                        
                if (y != 0) /* not first column */ 
                {
                    u = v - 1; /* x-1, y */ 
                    w = graph.GetTile(x - 1, y).TileCost;
                    SetNewDistance(v, u, w);
                }
            }       
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

        public int[] Star(int posIni, int posFinal, int qtdNodes, Map graph)
        {

            SortedList<int, Elem> border = new SortedList<int, Elem>();
            
            Dictionary<int, List<int> > fatherSon = new Dictionary<int, List<int> >();
            List<Elem> explored = new List<Elem>();

            
            border.Add( h(posIni,posFinal), new Elem(0, posIni));
            
            while (border.First().Value != posFinal)
            {
                var first = border.First();
                border.RemoveAt(0);

                foreach (var child in Neighborhood(first.Value.index) )
	            {
                    int accChild = first.Value.accCost + GetTileFromIndex(child).TileCost ;
                    border.Add(h(child, posFinal) + accChild, new Elem(accChild, child,first.Value));

	            }

                explored.Insert( 0  , first.Value);
            }


            Elem currParent = border.First().Value;

            List<int> pathReturn = new List<int>();
            pathReturn[0] = currParent.index;
            
            for (int i = 0 , j = 1; i < explored.Count; i++)
			{
                if (explored[i].index == currParent.parent.Value)
                {
                    pathReturn[j] = explored[i].index;
                    j++;
                }
			}            

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

            return (int)Math.Sqrt((xIni - xFin) ^ 2 + (yIni - yFin) ^ 2);
        }

        private List<int> Neighborhood(int inx )
        {

            List<int> retInxs = new List<int>();
            var x = inx / 42;
            var y = inx % 42;

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
