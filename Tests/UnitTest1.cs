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
        [TestMethod]
        public void TestAStar()
        {
            var mapCont = new MapController();
            var aStar = new AStar(42 * 42, mapCont.KantoMap);
            int totalCost ;
            var path = aStar.Star( XY2i(24,19) , XY2i(39,20) , 42 * 42, mapCont.KantoMap , out totalCost );

            Console.WriteLine(String.Join("\n", path.Select(v => i2XY(v)  ).Select(v => v[0].ToString() + ";" + v[1].ToString() ) ) );
            Console.WriteLine(totalCost);
            Assert.IsTrue(totalCost == 250);
        
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
