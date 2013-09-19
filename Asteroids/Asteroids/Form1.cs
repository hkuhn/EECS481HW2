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

    public struct Asteroid
    {
        public Graphics graphic;
        public int x;
        public int y;
        public int speed;
        public int type;
        public int hp;
        public int size;
    }

    public partial class GameplayForm : Form
    {
        
        // class vars
        private Boolean MoveLeft, MoveRight, Shooting;
        private static int ship_speed = 7;
        private static int laser_speed = 12;
        private static int laser_size = 7;
        private static int l1_asteroid_size = 40;
        private static int l2_asteroid_size = 60;
        private static int l3_asteroid_size = 100;
        private static int l1_asteroid_hp = 10;
        private static int l2_asteroid_hp = 15;
        private static int l3_asteroid_hp = 20;
        private Timer movement_timer = new Timer();
        private Timer spawn_timer = new Timer();

        private static Random rnd = new Random();

        // laser queue
        private List<Shot> laser_list;
        private List<Asteroid> asteroid_list;

        public GameplayForm()
        {
            InitializeComponent();
            // bool init
            MoveLeft = false;
            MoveRight = false;
            Shooting = false;
            // laser timer init
            laser_list = new List<Shot>();
            movement_timer.Interval = 5;
            movement_timer.Tick += new EventHandler(movement_Tick);
            movement_timer.Enabled = true;
            movement_timer.Start();
            // asteroid timer init
            asteroid_list = new List<Asteroid>();
            spawn_timer.Interval = 2000;
            spawn_timer.Tick += new EventHandler(spawn_Tick);
            spawn_timer.Enabled = true;
            spawn_timer.Start();
            
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

        private void movement_Tick(object sender, EventArgs e)
        {
            // laser movement
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

            // asteroid movement
            for (int i = 0; i < asteroid_list.Count; i++)
            {
                System.Drawing.SolidBrush myBrush;
                int hp = asteroid_list[i].hp;
                if (hp < 6)
                {
                    myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                }
                else if (hp < 11)
                {
                    myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Orange);
                }
                else if (hp < 16)
                {
                    myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
                }
                else
                {
                    myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
                }
                System.Drawing.Graphics asteroid;
                asteroid = this.CreateGraphics();
                asteroid.FillRectangle(myBrush, new Rectangle(asteroid_list[i].x, asteroid_list[i].y + asteroid_list[i].speed, asteroid_list[i].size, asteroid_list[i].size));
                myBrush.Dispose();
                Asteroid a = new Asteroid();
                a.graphic = asteroid;
                a.x = asteroid_list[i].x;
                a.y = asteroid_list[i].y - asteroid_list[i].speed;
                a.speed = asteroid_list[i].speed;
                a.type = asteroid_list[i].type;
                a.hp = asteroid_list[i].hp;
                a.size = asteroid_list[i].size;
                asteroid_list.RemoveAt(i);
                asteroid_list.Insert(i, a);
            }

            this.Invalidate();
        }

        private void spawn_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine("Spawn Tick");
            // generate an asteroid
            int type = rnd.Next(1,4);
            Debug.WriteLine(type);
            int size, hp;
            if (type == 1)
            {
                size = l1_asteroid_size;
                hp = l1_asteroid_hp;
            }
            else if (type == 2)
            {
                size = l2_asteroid_size;
                hp = l2_asteroid_hp;
            }
            else
            {
                size = l3_asteroid_size;
                hp = l3_asteroid_hp;
            }
            int placement_x = rnd.Next(size, 800 - size);
            Debug.WriteLine(size);
            Debug.WriteLine(hp);
            Debug.WriteLine(placement_x);
            // graphics
            //System.Drawing.SolidBrush myBrush;
            //if (hp < 6)
            //{
            //    myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            //}
            //else if (hp < 11)
            //{
            //    myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Orange);
            //}
            //else if (hp < 16)
            //{
            //    myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
            //}
            //else
            //{
            //    myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            //}
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics asteroid;
            asteroid = this.CreateGraphics();
            asteroid.FillRectangle(myBrush, new Rectangle(100, 600, size, size));
            myBrush.Dispose();
            Asteroid a = new Asteroid();
            a.graphic = asteroid;
            a.x = placement_x;
            a.y = size;
            a.speed = rnd.Next(5, 16);
            a.type = type;
            a.hp = hp;
            a.size = size;
            asteroid_list.Add(a);

        }

    }
}
