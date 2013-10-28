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
            this.lsvPokemons = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lsvPokemons
            // 
            this.lsvPokemons.Location = new System.Drawing.Point(1176, 234);
            this.lsvPokemons.Name = "lsvPokemons";
            this.lsvPokemons.Size = new System.Drawing.Size(161, 677);
            this.lsvPokemons.TabIndex = 0;
            this.lsvPokemons.UseCompatibleStateImageBehavior = false;
            // 
            // KantoMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 923);
            this.Controls.Add(this.lsvPokemons);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KantoMap";
            this.Text = "Pokemon";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvPokemons;





    }
}

