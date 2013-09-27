using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controller;

namespace View
{
    public class PictureMap : PictureBox
    {

        private MapController _mapController = new MapController();
        

        
        public PictureMap() :base()
        {
            this.Width = 1028;
            this.Height = 960;
           
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawBackgroudMap(pe.Graphics);

        }

        private void DrawBackgroudMap(Graphics graphic)
        {
            var yPoint = this.Width / 42;
            foreach (var tileLine in _mapController.KantoMap.KantoMap)
            {
                var xPoint = this.Height / 42;
                foreach (var tile in tileLine)
                {
                    graphic.DrawImage(tile.TitleImage, xPoint, yPoint);
                    xPoint += tile.TitleImage.Width;
                }
                yPoint += 18;
            }
        }
    }
}
