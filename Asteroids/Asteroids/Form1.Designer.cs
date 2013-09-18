using System;
using System.Drawing;
using System.Windows.Forms;


namespace Asteroids
{
    partial class GameplayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameplayForm));
            this.spaceship = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.spaceship)).BeginInit();
            this.SuspendLayout();
            // 
            // spaceship
            // 
            this.spaceship.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.spaceship.Image = ((System.Drawing.Image)(resources.GetObject("spaceship.Image")));
            this.spaceship.Location = new System.Drawing.Point(334, 700);
            this.spaceship.Name = "spaceship";
            this.spaceship.Size = new System.Drawing.Size(50, 50);
            this.spaceship.TabIndex = 0;
            this.spaceship.TabStop = false;
            // 
            // GameplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(784, 762);
            this.Controls.Add(this.spaceship);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 800);
            this.MinimumSize = new System.Drawing.Size(800, 800);
            this.Name = "GameplayForm";
            this.Text = "Asteroids";
            this.Load += new System.EventHandler(this.Asteroids_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Asteroids_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Asteroids_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Asteroids_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.spaceship)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox spaceship;
    }
}

