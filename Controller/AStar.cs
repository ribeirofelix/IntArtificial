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
        private int[] dist;
        private int[] distFinal;
        private int posIni;
        private int[] path;
        private bool[] vis;  /* true if vertex i has already been analyzed by the algorithm */
        private Tuple <int, int>[] G;

        private struct elemTree
        {
            int elem;
            int cost;
            int parent;
        };

        public AStar(int qtdNodes )
        {
            /* initialize the array with infinity distance */
            dist = Enumerable.Repeat(int.MaxValue, qtdNodes).ToArray() ;
            distFinal = Enumerable.Repeat(int.MaxValue, qtdNodes).ToArray();
        }

        public void Dijkstra(int posIni, int qtdNodes, Map graph)
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
                    setNewDistance(v, u, w);
                }
                if (x != 0)   /* not first line */
                {
                    u = v - 42; /* x, y - 1 */
                    w = graph.GetTile(x, y - 1).TileCost;
                    setNewDistance(v, u, w);
                }
                if (y != 41) /* not last column */
                {
                    u = v + 1; /* x+1, y */
                    w = graph.GetTile(x + 1, y).TileCost;
                    setNewDistance(v, u, w);
                }
                        
                if (y != 0) /* not first column */ 
                {
                    u = v - 1; /* x-1, y */ 
                    w = graph.GetTile(x - 1, y).TileCost;
                    setNewDistance(v, u, w);
                }
            }       
        }

        private void setNewDistance (int v, int u, int w)
        {
            if (dist[u] > dist[v] + w)
                    {
                        /* we pass by v to get to u */
                        dist[u] = dist[v] + w;
                        path[u] = v;
                    }
        }

        public void star(int posIni, int posFinal, int qtdNodes, Map graph)
        {
            int cost;
            Dijkstra(posIni, qtdNodes, graph) ;

            List <elemTree> leaves = new List<elemTree>();
            List <elemTree> check = new List<elemTree>();
            
            elemTree root =  new elemTree();
            root.elem = posIni;
            root.cost = dist[posIni] + h(posIni, posFinal);
            root.parent = null;

            leaves.Add(root);

            while (leaves.First.elem != posFinal)
            {
                
                leaves.Add
            }
            for (int pos = 0; pos < qtdNodes; pos++)
            {
                distFinal[pos] = dist[pos] + h(pos, posFinal);
            }


        }

        getNeighbours

        public int h(int posIni, int posFin)
        {
            int xIni = posIni % 42;
            int yIni = posIni / 42;
            int xFin = posFin % 42;
            int yFin = posFin / 42;

            return (int)Math.Sqrt((xIni - xFin) ^ 2 + (yIni - yFin) ^ 2);
        }
    }
}
