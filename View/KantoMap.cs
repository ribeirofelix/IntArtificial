using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Controller;

namespace View
{
    public partial class KantoMap : Form
    {
        private MapController _mapController = new MapController();
        
        public KantoMap()
        {
            this.Width = 1028;
            this.Height = 960;
           InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawBackgroudMap(e.Graphics);
        }

        private void DrawBackgroudMap(Graphics graphic)
        {
            var yPoint = this.Width / 42;
            foreach (var tileLine in _mapController.KantoMap)
            {
                var xPoint = this.Height / 42;
                foreach (var tile in tileLine)
                {
                    graphic.DrawImage( tile.TitleImage , xPoint, yPoint);
                    xPoint += tile.TitleImage.Width;
                }
                yPoint += 16;
            }            
        }
    }
}
