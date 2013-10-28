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
        private int[] dist;
        private int[] path;
      
        private Map graph;
        private int qtdNodes;
        protected struct Elem
        {
            public Elem(int accCost, Helper.Point pos, Helper.Point? parent = null)
            {
                this.accCost = accCost;
                this.pos = pos;
                this.parent = parent;
            }

            public int accCost ;
            public Helper.Point pos;
            public Helper.Point? parent;
        }

        public AStar( Map graph )
        {
            /* initialize the array with infinity distance */
            dist = Enumerable.Repeat(int.MaxValue, qtdNodes).ToArray() ;
            path = Enumerable.Repeat(int.MinValue, qtdNodes).ToArray();
            this.graph = graph;
            this.qtdNodes = graph.KantoMap.Sum(a => a.Count);
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

        public ICollection<Helper.Point> Star( Helper.Point posIni, Helper.Point posFinal, out int totalCost)
        {            
            var heapBorder = new Heap<Elem>();

         //   Console.WriteLine("cheguei no astar");

            List<Elem> explored = new List<Elem>();
            /* Array to verify if a position was explored */
            var hasExpl = new bool[qtdNodes,qtdNodes];
            var inBorder = new bool[qtdNodes,qtdNodes];
            hasExpl.Initialize();
            inBorder.Initialize();
            
            
            Elem father = new Elem(0, posIni);
            heapBorder.HeapAdd( h(posIni,posFinal), father );


            while (heapBorder.HeapSize() > 0 )
            {
                father = heapBorder.HeapExtractMin().Item3 ;
                inBorder[father.pos.x, father.pos.y] = false;
                if( father.pos.Equals(posFinal) )
                    break;

                explored.Insert(0, father);
                hasExpl[father.pos.x, father.pos.y] = true;


                foreach (var child in father.pos.Neighborhood( posFinal) )
	            {
                    int accChild = 0;
                    accChild = father.accCost + GetTileFromPos(child).TileCost;

                    if (hasExpl[child.x, child.y] && accChild >= father.accCost)
                        continue;

                    if (inBorder[child.x, child.y] == false || accChild < father.accCost)
                    {
                        heapBorder.HeapAdd(h(child, posFinal) + accChild, new Elem(accChild, child, father.pos));
                        inBorder[child.x, child.y] = true;
                    }
	            }             
            }

           
            var pathReturn = new List<Helper.Point>();
            pathReturn.Insert(0, father.pos );
            totalCost = father.accCost;

            if (!father.parent.HasValue)
               return pathReturn;

            var currParent = father.parent.Value ;
           

           
           
            
            for (int i = 0 , j = 1; i < explored.Count; i++)
			{
                if (explored[i].pos.Equals(currParent) )
                {
                    pathReturn.Insert(j,explored[i].pos);
                    j++;
                    currParent = explored[i].parent.HasValue ? explored[i].parent.Value : posIni  ;
                    //Debug.WriteLine("custo "+explored[i].accCost);
                }
			}
            pathReturn.Reverse();
            return pathReturn;

        }


        public int h(Helper.Point posIni, Helper.Point posFin)
        {
            int xIni = posIni.x ;
            int yIni = posIni.y ;
            int xFin = posFin.x ;
            int yFin = posFin.y ;

            return (int)Math.Sqrt( Math.Pow( (xIni - xFin), 2) + Math.Pow( (yIni - yFin), 2)*100)  ;
        }

        
        private Tile GetTileFromPos(Helper.Point pos)
        {
          return graph.GetTile(pos.x, pos.y);
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
