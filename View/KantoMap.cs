﻿using System;
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
        private AgentController _agentController;

        Button buttonStartSearch;
        Label costTextLabel;
        Label numberCostTextLabel;
        Label isHurt;
        Label pokeCount;
        Label pokeballs;
        PictureMap picsMap;

        public KantoMap()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            this.Width = 1080;
            this.Height = 720;

            buttonStartSearch = new Button();
            this.Controls.Add(buttonStartSearch);
            buttonStartSearch.Location = new Point(1000, 30);
            buttonStartSearch.Text = "Start Search";
            buttonStartSearch.Size = new Size(200, 70);

            costTextLabel = new Label();
            this.Controls.Add(costTextLabel);
            costTextLabel.Location = new Point(1000, 120);
            costTextLabel.Text = "Cost";
            costTextLabel.Size = new Size(150, 50);
            costTextLabel.Font = new Font(costTextLabel.Font.FontFamily.Name, 20);

          
            numberCostTextLabel = new Label();
            this.Controls.Add(numberCostTextLabel);
            numberCostTextLabel .Location = new Point(1000, 170);
            numberCostTextLabel.Text = "0";
            numberCostTextLabel.Size = new Size(150, 50);
            numberCostTextLabel.Font = new Font(costTextLabel.Font.FontFamily.Name, 20);

           isHurt = new Label();
            this.Controls.Add(isHurt);
            isHurt.Location = new Point(1000, 200);
            isHurt.Text = "Not Hurt!";
            isHurt.Size = new Size(150, 50);
            isHurt.Font = new Font(isHurt.Font.FontFamily.Name, 10);

            pokeCount = new Label();
            this.Controls.Add(pokeCount);
            pokeCount.Location = new Point(1000, 260);
            pokeCount.Text = "0";
            pokeCount.Size = new Size(150, 50);
            pokeCount.Font = new Font(isHurt.Font.FontFamily.Name, 10);

            pokeballs = new Label();
            this.Controls.Add(pokeballs);
            pokeballs.Location = new Point(1000, 320);
            pokeballs.Text = "0";
            pokeballs.Size = new Size(150, 50);
            pokeballs.Font = new Font(isHurt.Font.FontFamily.Name, 10);



            this.buttonStartSearch.Click += delegate(object sender, EventArgs e)
            {
                _agentController = new AgentController(MapController.Instance);
                buttonStartSearch.Enabled = false;
               _agentController.Walk();
            };

            picsMap = new PictureMap(MapController.Instance);
            

            this.Controls.Add(picsMap);
           
            InitializeComponent();
            MapController.Instance.Ash.listenerInfo += SetNewInfos;
            MapController.Instance.listenersAsh += UpdateAshPosition;
            MapController.Instance.Ash.showPoke += ShowPokemon;
       

        }

   
        public void SetNewInfos(Ash  ash)
        {

            numberCostTextLabel.Text = ash.TotalCost.ToString();
            isHurt.Text = ash.IsHurted ? "Hurted!" : "Not hurted!";
            pokeballs.Text = ash.Pokeballs.ToString();
            pokeCount.Text = ash.PokeCount.ToString();

            isHurt.Update();
            pokeballs.Update();
            pokeCount.Update();
            numberCostTextLabel.Update();
        }

        public void UpdateAshPosition(Helper.Point newAshPoint, Direction dir  )
        {
            picsMap.mapImage[newAshPoint.x][newAshPoint.y].ash = Map.Instance.Ash.AshImage;

            picsMap.mapImage[newAshPoint.x][newAshPoint.y].Invalidate();
            picsMap.mapImage[newAshPoint.x][newAshPoint.y].Update();
        
            
        }
        public void ShowPokemon(Pokemon poke)
        {
            

            lsvMyPokemnos.Items.Add(poke.ToString());
            lsvMyPokemnos.Update();
        }    
      
    }


    public class TileImage : PictureBox
    {
             
        Tile tile ;
        private const int prop = 22;

       
        public Direction ashDir = Direction.South;
        public Image ash;


        public TileImage(Tile tl, int x , int y)
        {
            this.Width = prop;
            this.Height = prop;
            this.Location = new Point(x, y);
            this.tile = tl;
            this.tile.listUpdView += UpdateTile;
          
            InitLayout();

        }
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
          
            base.OnPaint(pe);
           
            var graphic = pe.Graphics;

            var xPoint = 0;
            var yPoint = 0;

            // All tiles have a backgroud!
            graphic.DrawImage(tile.TileBackgroud, xPoint , yPoint);

            if (tile.HasPokemon)
                graphic.DrawImage(tile.Pokemon.PokeImage, xPoint, yPoint);

            Image pokeElemIg = tile.PokeElemImag;
            if (pokeElemIg != null)
                graphic.DrawImage(pokeElemIg, xPoint, yPoint);

            Image statsImg = tile.StatusImg;
            if (statsImg != null)
                graphic.DrawImage(statsImg, xPoint, yPoint);

            if (ash != null)
            {
                Bitmap ashBit = new Bitmap(ash);

                switch (ashDir)
                {
                    case Direction.North:
                        ashBit = rotateImage(ashBit, 180);
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
                graphic.DrawImage(ashBit, Map.Instance.AshIndex.y * prop, Map.Instance.AshIndex.x * prop);

            }
        }

        private void UpdateTile()
        {
            this.Invalidate();
            this.Update();
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
