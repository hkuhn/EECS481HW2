using System;
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

    public struct Shot
    {
        public Graphics graphic;
        public int x;
        public int y;
    }

    public partial class GameplayForm : Form
    {
        
        // class vars
        private Boolean MoveLeft, MoveRight, Shooting;
        private static int ship_speed = 7;
        private static int laser_speed = 12;
        private static int laser_size = 7;
        private Timer laser_timer = new Timer();

        // laser queue
        private List<Shot> laser_list;

        public GameplayForm()
        {
            InitializeComponent();
            MoveLeft = false;
            MoveRight = false;
            Shooting = false;

            laser_list = new List<Shot>();
            laser_timer.Interval = 5;
            laser_timer.Tick += new EventHandler(laser_Tick);
            laser_timer.Start();
            
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
                this.spaceship.Location = new Point(this.spaceship.Left - ship_speed, this.spaceship.Top);
            }
            else if (MoveRight)
            {
                // move right
                this.spaceship.Location = new Point(this.spaceship.Left + ship_speed, this.spaceship.Top);
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
                Shooting = true;
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.HotPink);
                System.Drawing.Graphics laser;
                laser = this.CreateGraphics();
                laser.FillRectangle(myBrush, new Rectangle(this.spaceship.Location.X + 23, this.spaceship.Location.Y - 15, laser_size, laser_size));
                myBrush.Dispose();
                Shot shot = new Shot();
                shot.graphic = laser;
                shot.x = this.spaceship.Location.X + 23;
                shot.y = this.spaceship.Location.Y - 15;
                laser_list.Add(shot);
            }
        }

        private void laser_Tick(object sender, EventArgs e)
        {
            // do stuff on timer
            for (int i = 0; i < laser_list.Count; i++)
            {
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.HotPink);
                System.Drawing.Graphics laser;
                laser = this.CreateGraphics();
                laser.FillRectangle(myBrush, new Rectangle(laser_list[i].x, laser_list[i].y - laser_speed, laser_size, laser_size));
                myBrush.Dispose();
                Shot s = new Shot();
                s.graphic = laser;
                s.x = laser_list[i].x;
                s.y = laser_list[i].y - laser_speed;
                laser_list.RemoveAt(i);
                laser_list.Insert(i, s);
            }

            this.Invalidate();
        }

    }
}
