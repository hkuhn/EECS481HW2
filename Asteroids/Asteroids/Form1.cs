﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Input;

namespace Asteroids
{
    public partial class GameplayForm : Form
    {
        
        // class vars
        private Boolean MoveLeft, MoveRight, Shooting;

        public GameplayForm()
        {
            InitializeComponent();
            MoveLeft = false;
            MoveRight = false;
            Shooting = false;
            
        }

        private void Asteroids_Load(object sender, EventArgs e)
        {

        }

        private void Asteroids_KeyDown(object sender, KeyEventArgs e)
        {

            //
            // Singular Event Handlers
            //
            if (e.KeyCode == Keys.Left)
            {
                // move left
                if (this.spaceship.Left > 0)
                {
                    MoveLeft = true;
                }
                else
                {
                    MoveLeft = false;
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                // move right
                if (this.spaceship.Left < 780 - this.spaceship.Width)
                {
                    MoveRight = true;
                }
                else
                {
                    MoveRight = false;
                }
            }

            // Evaluate Movements
            Asteroids_Evaluate();

        }

        private void Asteroids_Evaluate()
        {
            if (MoveLeft)
            {
                // move left
                this.spaceship.Location = new Point(this.spaceship.Left - 5, this.spaceship.Top);
            }
            else if (MoveRight)
            {
                // move right
                this.spaceship.Location = new Point(this.spaceship.Left + 5, this.spaceship.Top);
            }
            
        }

        private void Asteroids_KeyUp(object sender, KeyEventArgs e)
        {
            //
            // Singular Event Handlers
            //
            if (e.KeyCode == Keys.Left)
            {
                // move left
                MoveLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                // move right
                MoveRight = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                // resume shooting
                Shooting = false;
            }

            // Evaluate Movements
            Asteroids_Evaluate();
        }

        private void Asteroids_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ' && !Shooting)
            {
                // shoot
                Debug.WriteLine("SHOOT!");
                Shooting = true;
            }
        }



    }
}
