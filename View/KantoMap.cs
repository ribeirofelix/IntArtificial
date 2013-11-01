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
        private AgentController _agentController;

        Button buttonStartSearch;
        Label costTextLabel;
        Label numberCostTextLabel;
        Label isHurt;
        Label pokeCount;
        Label pokeballs;
        PictureMap picsMap;

        AshImage imgAsh;
     
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
            imgAsh = new AshImage(this);



            this.Controls.Add(picsMap);
            this.Controls.Add(imgAsh);

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

        public void UpdateAshPosition(Helper.Point newAshPoint, Direction dir )
        {
        
            picsMap.ashPoint.x = newAshPoint.x;
            picsMap.ashPoint.y = newAshPoint.y;
            picsMap.ashDir = dir;
            picsMap.Update();

        
            
        }
        public void ShowPokemon(Pokemon poke)
        {
            

            lsvMyPokemnos.Items.Add(poke.ToString());
            lsvMyPokemnos.Update();
        }

    
        
     


  
     
      
    }


    public class AshImage : PictureBox
    {
        

        private Helper.Point position;

        public AshImage(Form fm)
        {
            this.InitialImage = Map.Instance.Ash.AshImage;
            this.Width = 100;
            this.Height = 100;
            this.Location = new Point(100, 500);

            InitLayout();

        }

        

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            this.Invalidate();
            pe.Graphics.DrawImage(Map.Instance.Ash.AshImage, 1000, 500);
        }
    }

}
