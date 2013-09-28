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

        public struct Point
        {
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public int x;
            public int y;

            public override bool Equals(object obj)
            {

                return obj is Point && obj != null && ((Point)obj).x == this.x && ((Point)obj).y == this.y;
            }
            public ICollection<Helper.Point> Neighborhood()
            {
                
                List<Helper.Point> retInxs = new List<Helper.Point>();

                if (this.x != 41)   /* not last line */
                    retInxs.Add( new Point(this.x + 1, this.y )); /* x + 1, y */                

                if (this.x != 0)   /* not first line */
                    retInxs.Add(new Point( this.x - 1, this.y) ); /* x - 1, y */

                if (this.y != 41) /* not last column */
                    retInxs.Add( new Point(this.x , this.y + 1) ); /* x, y + 1 */ 
                
                if (this.y != 0) /* not first column */
                    retInxs.Add( new Point(this.x , this.y - 1) ); /* x, y - 1 */

                return retInxs;
            }
        }

       
    }
}
