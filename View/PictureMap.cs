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
        Image ash = Map.Instance.GetTile(new Helper.Point(19, 24)).Ash.AshImage;
        public Helper.Point ashPoint = new Helper.Point(19, 24);
        public Direction ashDir = Direction.South;
        private System.ComponentModel.IContainer components;
        private Graphics paintG;

        public PictureMap(MapController kantoMap) :base ()
        {
            this.Width = 1028;
            this.Height = 960;
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
          
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            DrawBackgroudMap(pe.Graphics);
            DrawAshBdgsPokemons(pe.Graphics);
            paintG = pe.Graphics;
            base.OnPaint(pe);
        }
               

        private void DrawAshBdgsPokemons(Graphics graphic)
        {
            var yPoint = 0;
            this.Invalidate();
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
                    ashBit = rotateImage(ashBit, 279);
                    break;
                case Direction.West:
                    ashBit = rotateImage(ashBit, 90);
                    break;
                default:
                    break;
            }
            graphic.DrawImage(ashBit, ashPoint.y*18, ashPoint.x*18);

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
                    
                    graphic.DrawImage( new Bitmap(tile.TileImage), xPoint, yPoint);
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

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

       

     
    
    
    }
}
