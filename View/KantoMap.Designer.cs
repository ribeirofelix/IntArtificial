﻿namespace View
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
            this.lsvMyPokemnos = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lsvMyPokemnos
            // 
            this.lsvMyPokemnos.Location = new System.Drawing.Point(1131, 312);
            this.lsvMyPokemnos.Name = "lsvMyPokemnos";
            this.lsvMyPokemnos.Size = new System.Drawing.Size(206, 581);
            this.lsvMyPokemnos.TabIndex = 0;
            this.lsvMyPokemnos.UseCompatibleStateImageBehavior = false;
            // 
            // KantoMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 923);
            this.Controls.Add(this.lsvMyPokemnos);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KantoMap";
            this.Text = "Pokemon";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvMyPokemnos;










    }
}

