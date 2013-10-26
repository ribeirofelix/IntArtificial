using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedProlog;
using System.Runtime.InteropServices;

namespace PrologCSharp
{
    class Program
    {

    

        static void Main(string[] args)
        {
            string path = "E:\\Documentos\\PUC-Rio_Trabalhos\\IntArtificial\\Prolog\\rules.pl";

            unsafe
            {int i;

            Prolog.Initilize(StrToSbt(path));
                do
                {
                    i = Console.Read();
                    int[] a = SbtToStr(Prolog.BestMove());
                    Console.WriteLine(a[0] + " " + a[1] + " " + a[2]);
                    
                } while (i != 0);
             
                

            }
        }
        
        
        public unsafe static sbyte* StrToSbt(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
                
                fixed (byte* p = bytes)
                {
                    sbyte* sp = (sbyte*)p;
                    return sp;
                }                
        }

        

        public unsafe static int[] SbtToStr(int * ptr)
        {
            int[] a = new int[3];
            a[0] = ptr[0];
            a[1] = ptr[1];
            a[2] = ptr[2];
            return a;
        }
    }
    
}
