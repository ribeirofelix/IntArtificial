using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Model
{
    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            //MessageBox.Show(text, caption);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow(null, _caption);
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }

    public class Helper
    {

     

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

            public override string ToString()
            {
                return String.Format("({0},{1})",this.x , this.y);
            }

            public override int GetHashCode()
            {
                return x ^ y;
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


        public static Point GetBadgePoint(BadgeTypes bdg)
        {
            switch (bdg)
            {
                case BadgeTypes.soul: return new Helper.Point(2, 4);
                case BadgeTypes.volcano: return new Helper.Point(4, 36);
                case BadgeTypes.thunder: return new Helper.Point(2, 19);
                case BadgeTypes.boulder: return new Helper.Point(40, 32);
                case BadgeTypes.rainbow: return new Helper.Point(22, 2);
                case BadgeTypes.earth: return new Helper.Point(20, 39);
                case BadgeTypes.cascade: return new Helper.Point(19, 14);
                case BadgeTypes.marsh: return new Helper.Point(37, 19);
                default: return new Helper.Point(0, 0);
            }

        }
       
    }
}
