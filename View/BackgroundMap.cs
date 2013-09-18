
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public class BackgroundMap : PictureBox
    {
        ICollection<ICollection<Image>> mapImage;

        public BackgroundMap(ICollection<ICollection<Image>> mapImage)
        {
            this.mapImage = mapImage;
            this.Name = "bm";
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
             var xPoint = this.Height / 42;

            foreach (var lineImage in mapImage)
            {
                lineImage.Select((im,inx) => new { im , pnt = new Point((xPoint + (inx + im.Width)) ,yPoint) }).ToList().ForEach(a => graphic.DrawImage(a.im,a.pnt));
                yPoint += lineImage.First().Height ;
            }


            //foreach (var tileLine in _mapController.KantoMap)
            //{
               
            //    foreach (var tile in tileLine)
            //    {
            //        var currImag = tile.TitleImage;
            //        graphic.DrawImage(currImag, xPoint, yPoint, currImag.Width, currImag.Height);
            //        xPoint += tile.TitleImage.Width;
            //    }
            //    yPoint += 18;
            //}
        }
    }
}
