namespace View
{
    partial class KantoMap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureEle = new System.Windows.Forms.PictureBox();
            this.pictureBoxFire = new System.Windows.Forms.PictureBox();
            this.pictureBoxFly = new System.Windows.Forms.PictureBox();
            this.pictureBoxWater = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWater)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEle
            // 
            this.pictureEle.Image = global::View.Properties.Resources.pokeelectric;
            this.pictureEle.Location = new System.Drawing.Point(1107, 355);
            this.pictureEle.Name = "pictureEle";
            this.pictureEle.Size = new System.Drawing.Size(100, 50);
            this.pictureEle.TabIndex = 0;
            this.pictureEle.TabStop = false;
            this.pictureEle.Visible = false;
            // 
            // pictureBoxFire
            // 
            this.pictureBoxFire.Image = global::View.Properties.Resources.pokefire;
            this.pictureBoxFire.Location = new System.Drawing.Point(1107, 440);
            this.pictureBoxFire.Name = "pictureBoxFire";
            this.pictureBoxFire.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxFire.TabIndex = 1;
            this.pictureBoxFire.TabStop = false;
            this.pictureBoxFire.Visible = false;
            // 
            // pictureBoxFly
            // 
            this.pictureBoxFly.Image = global::View.Properties.Resources.pokeflying;
            this.pictureBoxFly.Location = new System.Drawing.Point(1107, 536);
            this.pictureBoxFly.Name = "pictureBoxFly";
            this.pictureBoxFly.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxFly.TabIndex = 2;
            this.pictureBoxFly.TabStop = false;
            this.pictureBoxFly.Visible = false;
            // 
            // pictureBoxWater
            // 
            this.pictureBoxWater.Image = global::View.Properties.Resources.pokewater;
            this.pictureBoxWater.Location = new System.Drawing.Point(1107, 628);
            this.pictureBoxWater.Name = "pictureBoxWater";
            this.pictureBoxWater.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxWater.TabIndex = 3;
            this.pictureBoxWater.TabStop = false;
            this.pictureBoxWater.Visible = false;
            // 
            // KantoMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 923);
            this.Controls.Add(this.pictureBoxWater);
            this.Controls.Add(this.pictureBoxFly);
            this.Controls.Add(this.pictureBoxFire);
            this.Controls.Add(this.pictureEle);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "KantoMap";
            this.Text = "Pokemon";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWater)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureEle;
        private System.Windows.Forms.PictureBox pictureBoxFire;
        private System.Windows.Forms.PictureBox pictureBoxFly;
        private System.Windows.Forms.PictureBox pictureBoxWater;



    }
}

