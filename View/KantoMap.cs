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
            this.Controls.Add(new PictureMap(_mapController));
       
           InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);         

        }

       
    }
}
