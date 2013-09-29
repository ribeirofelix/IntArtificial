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

        Button buttonStartSearch;
        Label costTextLabel;
        Label numberCostTextLabel;

        public KantoMap()
        {
            this.Width = 1028;
            this.Height = 960;

            buttonStartSearch = new Button();
            this.Controls.Add(buttonStartSearch);
            buttonStartSearch.Location = new Point(780, 30);
            buttonStartSearch.Text = "Start Search";
            buttonStartSearch.Size = new Size(200, 70);

            costTextLabel = new Label();
            this.Controls.Add(costTextLabel);
            costTextLabel.Location = new Point(780, 120);
            costTextLabel.Text = "Cost";

            numberCostTextLabel = new Label();
            this.Controls.Add(numberCostTextLabel);
            numberCostTextLabel.Location = new Point(780, 170);
            numberCostTextLabel.Text = "0";

            _mapController.listenersCost += UpdateNumberCostLabel;

            this.buttonStartSearch.Click += delegate(object sender, EventArgs e) { /*startsearch*/; };

            this.Controls.Add(new PictureMap(_mapController));

            InitializeComponent();
        }

        public void UpdateNumberCostLabel(int cost)
        {
            numberCostTextLabel.Text = cost.ToString();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void KantoMap_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //stepAsh
            //updateCusto
        }
      
    }
}
