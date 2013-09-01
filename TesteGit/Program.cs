using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteGit
{
    class Program
    {
        static void Main(string[] args)
        {
            var listade100 = Enumerable.Range(0, 10);
            var lista2 = Enumerable.Range(0, 10);
            int soma=0;

            var iteratorList2 = lista2.GetEnumerator();
            iteratorList2.MoveNext();
            foreach (var item in listade100)
            {
                Console.WriteLine(item);
                soma += item + iteratorList2.Current;
                iteratorList2.MoveNext();
            }

            Console.WriteLine(soma);
           
        }
    }
}
