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
        public Direction ashDir = Direction.South;

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

            Bitmap ashBit = new Bitmap(ash);
            switch (ashDir)
            {
                case Direction.North:
                    ashBit = rotateImage( ashBit , 180);
                    break;
                case Direction.East:
                    ashBit = rotateImage(ashBit, 90);
                    break;
                case Direction.West:
                    ashBit = rotateImage(ashBit, 270);
                    break;
                default:
                    break;
            }
            graphic.DrawImage(ashBit, ashPoint.y*18, ashPoint.x*18);
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

        private Bitmap rotateImage(Bitmap b, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(returnBitmap);
            //move rotation point to center of image
            g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
            //rotate
            g.RotateTransform(angle);
            //move image back
            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            //draw passed in image onto graphics object
            g.DrawImage(b, new Point(0, 0));
            return returnBitmap;
        }
    }
}
