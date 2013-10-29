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
        ListView lsvMyPokemnos;

        PictureMap picsMap;
     
        public KantoMap()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            this.Width = 1920;
            this.Height = 1080;

            buttonStartSearch = new Button();
            this.Controls.Add(buttonStartSearch);
            buttonStartSearch.Location = new Point(1080, 30);
            buttonStartSearch.Text = "Start Search";
            buttonStartSearch.Size = new Size(200, 70);

            costTextLabel = new Label();
            this.Controls.Add(costTextLabel);
            costTextLabel.Location = new Point(1080, 120);
            costTextLabel.Text = "Cost";
            costTextLabel.Size = new Size(150, 50);
            costTextLabel.Font = new Font(costTextLabel.Font.FontFamily.Name, 20);

            numberCostTextLabel = new Label();
            this.Controls.Add(numberCostTextLabel);
            numberCostTextLabel .Location = new Point(1080, 170);
            numberCostTextLabel.Text = "0";
            numberCostTextLabel.Size = new Size(150, 50);
            numberCostTextLabel.Font = new Font(costTextLabel.Font.FontFamily.Name, 20);

            lsvMyPokemnos = new ListView();
            lsvMyPokemnos.Location = new Point(1080, 180);
            lsvMyPokemnos.Size = new Size(230, 675);
            lsvMyPokemnos.Visible = true;



            this.buttonStartSearch.Click += delegate(object sender, EventArgs e)
            {
                _agentController = new AgentController(MapController.Instance);
                buttonStartSearch.Enabled = false;
               _agentController.Walk();
            };

            picsMap = new PictureMap(MapController.Instance);

            this.Controls.Add(picsMap);

            InitializeComponent();
            MapController.Instance.Ash.listenersCost += SetNewCost;
            MapController.Instance.listenersAsh += UpdateAshPosition;
            MapController.Instance.Ash.showPoke += ShowPokemon;


        }

        void mTimer_Tick(object sender, EventArgs e)
        {
            picsMap.Invalidate();
            picsMap.Update();
 
        }

        public void SetNewCost(int cost)
        {

            AutoClosingMessageBox.Show("Cost:" + cost.ToString(), "Cost:" + cost.ToString(), 50);           
            numberCostTextLabel.Text = cost.ToString();
            numberCostTextLabel.Invalidate();
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
}
