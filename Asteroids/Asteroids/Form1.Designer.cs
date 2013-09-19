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
            this.score = new System.Windows.Forms.Label();
            this.lives = new System.Windows.Forms.Label();
            this.countdown_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spaceship)).BeginInit();
            this.SuspendLayout();
            // 
            // spaceship
            // 
            this.spaceship.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.spaceship.Image = ((System.Drawing.Image)(resources.GetObject("spaceship.Image")));
            this.spaceship.Location = new System.Drawing.Point(350, 700);
            this.spaceship.Margin = new System.Windows.Forms.Padding(0);
            this.spaceship.MaximumSize = new System.Drawing.Size(50, 50);
            this.spaceship.MinimumSize = new System.Drawing.Size(50, 50);
            this.spaceship.Name = "spaceship";
            this.spaceship.Size = new System.Drawing.Size(50, 50);
            this.spaceship.TabIndex = 0;
            this.spaceship.TabStop = false;
            // 
            // score
            // 
            this.score.BackColor = System.Drawing.Color.Transparent;
            this.score.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score.ForeColor = System.Drawing.Color.White;
            this.score.Location = new System.Drawing.Point(12, 9);
            this.score.MaximumSize = new System.Drawing.Size(150, 20);
            this.score.MinimumSize = new System.Drawing.Size(150, 20);
            this.score.Name = "score";
            this.score.Size = new System.Drawing.Size(150, 20);
            this.score.TabIndex = 1;
            this.score.Text = "Score: 00000";
            this.score.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lives
            // 
            this.lives.AutoSize = true;
            this.lives.BackColor = System.Drawing.Color.Transparent;
            this.lives.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lives.ForeColor = System.Drawing.Color.White;
            this.lives.Location = new System.Drawing.Point(622, 11);
            this.lives.MaximumSize = new System.Drawing.Size(150, 20);
            this.lives.MinimumSize = new System.Drawing.Size(150, 20);
            this.lives.Name = "lives";
            this.lives.Size = new System.Drawing.Size(150, 20);
            this.lives.TabIndex = 2;
            this.lives.Text = "Lives: 5";
            this.lives.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // countdown_label
            // 
            this.countdown_label.AutoSize = true;
            this.countdown_label.BackColor = System.Drawing.Color.Transparent;
            this.countdown_label.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 100F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countdown_label.ForeColor = System.Drawing.Color.White;
            this.countdown_label.Location = new System.Drawing.Point(234, 154);
            this.countdown_label.MaximumSize = new System.Drawing.Size(300, 300);
            this.countdown_label.MinimumSize = new System.Drawing.Size(300, 300);
            this.countdown_label.Name = "countdown_label";
            this.countdown_label.Size = new System.Drawing.Size(300, 300);
            this.countdown_label.TabIndex = 3;
            this.countdown_label.Text = "10";
            this.countdown_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(784, 762);
            this.Controls.Add(this.countdown_label);
            this.Controls.Add(this.lives);
            this.Controls.Add(this.score);
            this.Controls.Add(this.spaceship);
            this.DoubleBuffered = true;
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox spaceship;
        private Label score;
        private Label lives;
        private Label countdown_label;
    }
}

