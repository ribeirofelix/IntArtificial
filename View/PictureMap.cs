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

        public PictureMap(MapController kantoMap) :base ()
        {
            this.Width = 1028;
            this.Height = 960;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawBackgroudMap(pe.Graphics);
            DrawAshBdgsPokemons(pe.Graphics);
        }

        Image ash = Map.Instance.GetTile(new Helper.Point(19, 24)).Ash.AshImage;
        public Helper.Point ashPoint = new Helper.Point(19,24);

        private void DrawAshBdgsPokemons(Graphics graphic)
        {
            var yPoint = 0;

            foreach (var tileLine in Map.Instance.KantoMap)
            {
                var xPoint = 0;
                foreach (var tile in tileLine)
                {
                    Image toDraw = null;
                    if (tile.HasPokemon)
                        toDraw = tile.Pokemon.PokeImage;
                    else
                        toDraw = tile.TileImage;

                    graphic.DrawImage(toDraw, xPoint, yPoint);
                 
                    xPoint += 18;
                }
                yPoint += 18;
            }   

            graphic.DrawImage(ash, ashPoint.y*18, ashPoint.x*18);
            graphic.DrawRectangle(new Pen(Color.Red, 3), (ashPoint.y - 4) * 18, (ashPoint.x - 4) * 18, 18*9, 18*9);
        }

        private void DrawBackgroudMap(Graphics graphic)
        {
               var yPoint = 0;

            foreach (var tileLine in Map.Instance.KantoMap)
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
