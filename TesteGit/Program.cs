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
            int soma=0;

            foreach (var item in listade100)
            {
                Console.WriteLine(item);
                soma = soma * soma;
            }

            Console.WriteLine(soma);
           
        }
    }
}
