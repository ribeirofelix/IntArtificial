using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Controller;
using System.Linq;
using Sorting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        #region Distances Tests
        [TestMethod]
        public void DistanceToVolcano()
        {
            var mapCont = new MapController();
            var aStar = new AStar( mapCont.KantoMap);
            int totalCost ;
            var path = aStar.Star( new Helper.Point(19, 24) , new Helper.Point(4,36) , out totalCost );

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);     
        }
        [TestMethod]
        public void DistanceToMarsh()
        {
            var mapCont = new MapController();
            var aStar = new AStar( mapCont.KantoMap);
            int totalCost;

            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(37, 19), out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);

            Assert.IsTrue(totalCost == 410);
        
            
        }
        [TestMethod]
        public void DistanceToSoul()
        {
            var mapCont = new MapController();
            var aStar = new AStar( mapCont.KantoMap);
            int totalCost;

            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(2, 4) , out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            for (int i = 0; i < path.Count; i++)
            {
                Helper.Point pos = path.ElementAt(i);

                Tile temp = mapCont.KantoMap.GetTile(pos.x, pos.y );
                Console.WriteLine(temp.TileType);
            }
            Console.WriteLine(totalCost);
            Assert.IsTrue(430 == totalCost);
           
        }
        [TestMethod]
        public void DistanceToThunder()
        {
            var mapCont = new MapController();
            var aStar = new AStar( mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(2, 19),out totalCost);
            Assert.IsTrue(totalCost == 660);
        }
        [TestMethod]
        public void DistanceToBoulder()
        {
            var mapCont = new MapController();
            var aStar = new AStar( mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(32, 40),  out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v => v.x.ToString() + ";" + v.y.ToString())));
            Console.WriteLine(totalCost);
            Assert.IsTrue(totalCost == 950);
        }
        [TestMethod]
        public void DistanceToRainbow()
        {
            var mapCont = new MapController();
            var aStar = new AStar( mapCont.KantoMap);
            int totalCost;

            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(22, 2), out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);
        }
        [TestMethod]
        public void DistanceToEarth()
        {
            var mapCont = new MapController();
            var aStar = new AStar( mapCont.KantoMap);
            int totalCost;

            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(20, 39),  out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);
            Assert.IsTrue(totalCost == 250);
        }
        [TestMethod]
        public void DistanceToCascade()
        {
            var mapCont = new MapController();
            var aStar = new AStar( mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(19, 14) , out totalCost);


            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);
            Assert.IsTrue(totalCost == 410);
        }
#endregion




        public void TesteGenetics()
        {
             var mapCont = new MapController();
             mapCont.UpdateDistances();
            var captBdg = Enumerable.Repeat(false,8).ToArray();
            var gen = new BRKGA(9, 42 * 42, 500, 100, captBdg, mapCont.DistMap);
            gen.Evolve(10);
            
        }

        public void TestHeapMinEmptySimple()
        {

            var testHeap = new Heap<string>();
            Random rd = new Random();
            int size = 1000000;

            //Heap comeca vazio
            int heapTreeSizeBefore = 0;
            for (int i = 1; i <= size; i++)
            {
                testHeap.HeapAdd(rd.Next(1, int.MaxValue), i.ToString());
                Assert.IsTrue(testHeap.HeapSize() > heapTreeSizeBefore);
                heapTreeSizeBefore = testHeap.HeapSize();
            }

            int minElementBefore = -1;
            heapTreeSizeBefore = size;
            for (int i = 1; i <= size; i++)
            {
                var minElement = testHeap.HeapExtractMin();
                Assert.IsTrue(minElementBefore <= minElement.Item1);
                Assert.IsTrue(testHeap.HeapSize() < heapTreeSizeBefore);
                heapTreeSizeBefore = testHeap.HeapSize();
            }
        }
    }
}
