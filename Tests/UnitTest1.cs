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
            var mapCont = MapController.Instance;
            var aStar = new AStar( Map.Instance);
            int totalCost ;
            var path = aStar.Star( new Helper.Point(19, 24) , new Helper.Point(4,36) , out totalCost );

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);     
        }
        [TestMethod]
        public void DistanceToMarsh()
        {
            var mapCont = MapController.Instance;
            var aStar = new AStar( Map.Instance);
            int totalCost;

            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(37, 19), out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);

            Assert.IsTrue(totalCost == 440);
        
            
        }
        [TestMethod]
        public void DistanceToSoul()
        {
            var mapCont = MapController.Instance;
            var aStar = new AStar( Map.Instance);
            int totalCost;

            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(2, 4) , out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            for (int i = 0; i < path.Count; i++)
            {
                Helper.Point pos = path.ElementAt(i);

                Tile temp = Map.Instance.GetTile(pos.x, pos.y );
                Console.WriteLine(temp.TileType);
            }
            Console.WriteLine(totalCost);
            Assert.IsTrue(430 == totalCost);
           
        }
        [TestMethod]
        public void DistanceToThunder()
        {
            var mapCont = MapController.Instance;
            var aStar = new AStar( Map.Instance);
            int totalCost;
            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(2, 19),out totalCost);
            Assert.IsTrue(totalCost == 660);
        }
        [TestMethod]
        public void DistanceToBoulder()
        {
            var mapCont = MapController.Instance;
            var aStar = new AStar( Map.Instance);
            int totalCost;
            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(40, 32),  out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v => v.x.ToString() + ";" + v.y.ToString())));
            Console.WriteLine(totalCost);
            Assert.IsTrue(totalCost == 950);
        }
        [TestMethod]
        public void DistanceToRainbow()
        {
            var mapCont = MapController.Instance;
            var aStar = new AStar( Map.Instance);
            int totalCost;

            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(22, 2), out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);
        }
        [TestMethod]
        public void DistanceToEarth()
        {
            var mapCont = MapController.Instance;
            var aStar = new AStar( Map.Instance);
            int totalCost;

            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(20, 39),  out totalCost);

            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);
            Assert.IsTrue(totalCost == 250);
        }
        [TestMethod]
        public void DistanceToCascade()
        {
            var mapCont = MapController.Instance;
            var aStar = new AStar( Map.Instance);
            int totalCost;
            var path = aStar.Star(new Helper.Point(19, 24), new Helper.Point(19, 14) , out totalCost);


            Console.WriteLine(String.Join("\n", path.Select(v =>  v.x.ToString() + ";" + v.y.ToString() ) ) );
            Console.WriteLine(totalCost);
            Assert.IsTrue(totalCost == 410);
        }
#endregion



  
      
        [TestMethod]
        public void TesteSimpleWalk()
        {
            Map a = Map.Instance;
            var agen = new AgentController(MapController.Instance);
            agen.Walk();


            //Assert.IsTrue(capt.All(a => a));

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


        [TestMethod]
        public void TesteRemoveRep()
        {
            int[] rep1 = { 0, 1, 1, 2};
            int[] rep2 = { 0, 1, 1, 2 };
            int[] rep3 = { 0, 1, 1, 2 };
            int[] rep4 = { 0, 1, 1, 2};
            bool[] valids = { false,true,false };

            rep1 = removeRep(rep1, valids);
            rep2 = removeRep(rep2, valids);
            rep3 = removeRep(rep3, valids);
            rep4 = removeRep(rep4, valids);

            Assert.IsTrue(rep1.GroupBy(a => a).Count() == 4);
            Assert.IsTrue(rep2.GroupBy(a => a).Count() == 4);
            Assert.IsTrue(rep3.GroupBy(a => a).Count() == 4);
            Assert.IsTrue(rep4.GroupBy(a => a).Count() == 4);
        }

        private int[] removeRep(int[] chrom , bool[] captBadgs)
        {
            var valid = captBadgs.Select((b, i) => new { b, i }).Where(bi => bi.b == false).Select(bi => new { i = bi.i+1, qtd = 0 }).ToDictionary(bdic => bdic.i, bele => bele.qtd);

            var repIndex = chrom.Select((c, i) => new { c, i }).GroupBy(d => d.c).Where(cnt => cnt.Count() > 1);

          
            foreach (var allelle in chrom.Skip(1) )
            {
                valid[allelle]++;
            }

         
            foreach (var grRep in repIndex)
            {
                int frsOccu = grRep.First().i;
                var frstNotChoose = valid.Where(v => v.Value == 0).First();
                chrom[frsOccu] = frstNotChoose.Key;
                valid.Remove(frstNotChoose.Key);
              
            }
            return chrom;

        }

     
    
    }
  
}
