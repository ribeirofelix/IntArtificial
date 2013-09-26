using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Helper
    {

        public static int[] i2XY(int ix)
        {
            return new int[2] { ix % 42, ix / 42 };
        }

        public static int XY2i(int x, int y)
        {
            return (y * 42) + x;
        }

       
    }
}
