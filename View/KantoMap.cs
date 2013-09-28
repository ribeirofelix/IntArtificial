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

            Button buttonStartSearch = new Button();
            this.Controls.Add(buttonStartSearch);
            buttonStartSearch.Location = new Point(780, 30);
            buttonStartSearch.Text = "Start Search";
            buttonStartSearch.Size = new Size(200, 70);

            Label costTextLabel = new Label();
            this.Controls.Add(costTextLabel);
            costTextLabel.Location = new Point(780, 100);
            costTextLabel.Text = "Start Search";
            //costTextLabel.Font = new Font(FontFamily.GenericSansSerif, 14, 
            //  buttonStartSearch.Size = new Size(200, 70);

            this.Controls.Add(new PictureMap(_mapController));

      
           InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }

        private void KantoMap_Load(object sender, EventArgs e)
        {

        }
      
    }
}
