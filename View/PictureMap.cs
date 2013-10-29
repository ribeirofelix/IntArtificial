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
            this.Width = 1920;
            this.Height = 1080;
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
          
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            DrawMap(pe.Graphics);
            paintG = pe.Graphics;
            base.OnPaint(pe);
        }
               

        private void DrawMap(Graphics graphic)
        {
            var yPoint = 0;
            this.Invalidate();
            foreach (var tileLine in Map.Instance.KantoMap)
            {
                var xPoint = 0;
                foreach (var tile in tileLine)
                {
                    // All tiles have a backgroud!
                    graphic.DrawImage(tile.TileBackgroud, xPoint, yPoint);

                    if (tile.HasPokemon)
                        graphic.DrawImage(tile.Pokemon.PokeImage, xPoint, yPoint);

                    Image pokeElemIg = tile.PokeElemImag;
                    if (pokeElemIg != null)
                        graphic.DrawImage(pokeElemIg, xPoint, yPoint);

                    xPoint += 32;
                }
                yPoint += 32;
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
            graphic.DrawImage(ashBit, ashPoint.y*32, ashPoint.x*32);

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
