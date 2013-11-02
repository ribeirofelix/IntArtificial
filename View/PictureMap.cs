using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;
using Model;

namespace View
{
    public class PictureMap : PictureBox
    {
        Image ash = Map.Instance.Ash.AshImage ;
        public Helper.Point ashPoint = Map.Instance.AshIndex;
        public Direction ashDir = Direction.South;

        public TileImage[][] mapImage = new TileImage[42][];
       
        private const int prop = 22;

        public PictureMap(MapController kantoMap) :base ()
        {
            this.Width = 1000;
            this.Height = 1000;
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            InitilizeMatrix();

        }
          
        
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }


        private void InitilizeMatrix()
        {
            var yPoint =0;
            int i = 0;
            foreach (var tileLine in Map.Instance.KantoMap)
            {
                var xPoint = 0;
                int j = 0;
                mapImage[i] = new TileImage[42];
                foreach (var tile in tileLine)
                {
                    mapImage[i][j] = new TileImage(tile, xPoint, yPoint);
                    this.Controls.Add(  mapImage[i][j] );
                    xPoint += prop;
                    j++;
                }
                i++;
                yPoint += prop;
            }

        }
         
    
    }
}
