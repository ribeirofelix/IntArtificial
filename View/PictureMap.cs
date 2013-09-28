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

        private MapController _mapController = new MapController();
        
        public PictureMap(MapController kantoMap) :base ()
        {
            this.Width = 1028;
            this.Height = 960;
            kantoMap.listeners += RedrawAsh;
         
        }

        public void RedrawAsh(Helper.Point point)
        {
            Console.WriteLine(_mapController.KantoMap.AshIndex.x + ";" + _mapController.KantoMap.AshIndex.y); 

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawBackgroudMap(pe.Graphics);
            DrawAshBdgsPokemons(pe.Graphics);
        }

        private void DrawAshBdgsPokemons(Graphics graphic)
        {

            foreach (var tileLine in _mapController.KantoMap.KantoMap)
            {
                foreach (var tile in tileLine)
                {
                    if (tile.HasAsh)
                    {
                        graphic.DrawImage(tile.Ash.AshImage, tile.XPoint * 18, tile.YPoint * 18);
                        break;
                    }

                }
            }

        }

        private void DrawBackgroudMap(Graphics graphic)
        {
            var yPoint = 0;

            foreach (var tileLine in _mapController.KantoMap.KantoMap)
            {
                var xPoint = 0;
                foreach (var tile in tileLine)
                {
                    if (tile.TileImage == null)
                    {
                        Console.WriteLine("invalido");
                        continue;
                    }
                    graphic.DrawImage(tile.TileImage, xPoint, yPoint);
                    xPoint += 18;
                }
                yPoint += 18;
            }
        }
    }
}
