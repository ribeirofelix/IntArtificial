using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Controller;
using Sorting;
using System.Linq;

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
            var aStar = new AStar(42 * 42, mapCont.KantoMap);
            int totalCost ;
            var path = aStar.Star( XY2i(24,19) , XY2i(36,4) , 42 * 42, mapCont.KantoMap , out totalCost );

            Console.WriteLine(String.Join("\n", path.Select(v => i2XY(v)  ).Select(v => v[0].ToString() + ";" + v[1].ToString() ) ) );
            Console.WriteLine(totalCost);     
        }
        [TestMethod]
        public void DistanceToMarsh()
        {
            var mapCont = new MapController();
            var aStar = new AStar(42 * 42, mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(XY2i(24, 19), XY2i(19, 37), 42 * 42, mapCont.KantoMap, out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v => i2XY(v)).Select(v => v[0].ToString() + ";" + v[1].ToString())));
            Console.WriteLine(totalCost);
<<<<<<< HEAD
            Assert.IsTrue(totalCost == 250);
        
=======
            
        }
        [TestMethod]
        public void DistanceToSoul()
        {
            var mapCont = new MapController();
            var aStar = new AStar(42 * 42, mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(XY2i(24, 19), XY2i(4, 2) , 42 * 42, mapCont.KantoMap, out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v => i2XY(v)).Select(v => v[0].ToString() + ";" + v[1].ToString())));
            Console.WriteLine(totalCost);
           
        }
        [TestMethod]
        public void DistanceToThunder()
        {
            var mapCont = new MapController();
            var aStar = new AStar(42 * 42, mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(XY2i(24, 19), XY2i(19, 2), 42 * 42, mapCont.KantoMap, out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v => i2XY(v)).Select(v => v[0].ToString() + ";" + v[1].ToString())));
            Console.WriteLine(totalCost);
        }
        [TestMethod]
        public void DistanceToBoulder()
        {
            var mapCont = new MapController();
            var aStar = new AStar(42 * 42, mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(XY2i(24, 19), XY2i(32, 40), 42 * 42, mapCont.KantoMap, out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v => i2XY(v)).Select(v => v[0].ToString() + ";" + v[1].ToString())));
            Console.WriteLine(totalCost);
        }
        [TestMethod]
        public void DistanceToRainbow()
        {
            var mapCont = new MapController();
            var aStar = new AStar(42 * 42, mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(XY2i(24, 19), XY2i(2, 22), 42 * 42, mapCont.KantoMap, out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v => i2XY(v)).Select(v => v[0].ToString() + ";" + v[1].ToString())));
            Console.WriteLine(totalCost);
        }
        [TestMethod]
        public void DistanceToEarth()
        {
            var mapCont = new MapController();
            var aStar = new AStar(42 * 42, mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(XY2i(24, 19), XY2i(39, 20), 42 * 42, mapCont.KantoMap, out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v => i2XY(v)).Select(v => v[0].ToString() + ";" + v[1].ToString())));
            Console.WriteLine(totalCost);
        }
        [TestMethod]
        public void DistanceToCascade()
        {
            var mapCont = new MapController();
            var aStar = new AStar(42 * 42, mapCont.KantoMap);
            int totalCost;
            var path = aStar.Star(XY2i(24, 19), XY2i(14, 19) , 42 * 42, mapCont.KantoMap, out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v => i2XY(v)).Select(v => v[0].ToString() + ";" + v[1].ToString())));
            Console.WriteLine(totalCost);
        }
#endregion




        public void TesteGenetics()
        {
             var mapCont = new MapController();
             mapCont.UpdateDistances();
            var captBdg = Enumerable.Repeat(false,8).ToArray();
            var gen = new BRKGA(9, 42 * 42, 500, 100, captBdg, mapCont.DistMap);
            gen.Evolve(10);
            
>>>>>>> 44f7bae4ff3b4d70c53aa87a8fd0036de85620b4
        }

        private int[] i2XY(int ix)
        {
            return new int[2] { ix % 42, ix / 42 };
        }

        private int XY2i(int x, int y)
        {
            return (y * 42) + x;
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
