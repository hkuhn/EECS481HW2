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
        private Boolean GameOver;
        private Boolean MoveLeft, MoveRight, Shooting;
        private static int ship_speed = 12;
        private static int laser_speed = 12;
        private static int laser_size = 7;
        private static int l1_asteroid_size = 40;
        private static int l2_asteroid_size = 60;
        private static int l3_asteroid_size = 100;
        private static int l1_asteroid_hp = 10;
        private static int l2_asteroid_hp = 15;
        private static int l3_asteroid_hp = 20;
        private static int level_decrement = 500; // 0.5 sec
        private static int level_limit = 3000; // 3 sec
        private static int point_increment = 5;
        private Timer movement_timer = new Timer();
        private Timer spawn_timer = new Timer();
        private Timer level_timer = new Timer();
        private Timer countdown_timer = new Timer();
        private int count = 9; // init countdown
        private int numLives = 5;
        private int points = 0;

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
            spawn_timer.Interval = 9500;
            spawn_timer.Tick += new EventHandler(spawn_Tick);
            spawn_timer.Enabled = true;
            spawn_timer.Start();
            // level timer init
            level_timer.Interval = 10000; // 10 sec
            level_timer.Tick += new EventHandler(level_Tick);
            level_timer.Enabled = true;
            level_timer.Start();
            // countdown timer init
            countdown_timer.Interval = 1000; // 1 sec
            countdown_timer.Tick += new EventHandler(countdown_Tick);
            countdown_timer.Enabled = true;
            countdown_timer.Start();

            // game over
            GameOver = false;


            // paint and buffer
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);
            this.Paint += new PaintEventHandler(Asteroids_Paint);
            
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
            if (MoveLeft && !GameOver)
            {
                // move left
                this.spaceship.Location = new Point(this.spaceship.Left - ship_speed, this.spaceship.Top);
            }
            else if (MoveRight && !GameOver)
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
            if (e.KeyChar == ' ' && !Shooting && !GameOver)
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

        private void Asteroids_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < laser_list.Count; i++)
            {
                // redraw lasers
                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.HotPink);
                System.Drawing.Graphics laser;
                laser = this.CreateGraphics();
                laser.FillRectangle(myBrush, new Rectangle(laser_list[i].x, laser_list[i].y - laser_speed, laser_size, laser_size));
                myBrush.Dispose();
                laser.Dispose();
            }

            for (int i = 0; i < asteroid_list.Count; i++)
            {
                // redraw asteroids
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
                asteroid.Dispose();
            }
        }

        private void movement_Tick(object sender, EventArgs e)
        {
            // laser movement
            for (int i = 0; i < laser_list.Count; i++)
            {
                Shot s = new Shot();
                s.graphic = laser_list[i].graphic;
                s.x = laser_list[i].x;
                s.y = laser_list[i].y - laser_speed;
                laser_list.RemoveAt(i);
                laser_list.Insert(i, s);
                
            }

            // asteroid movement
            for (int i = 0; i < asteroid_list.Count; i++)
            {
                Asteroid a = new Asteroid();
                a.graphic = asteroid_list[i].graphic;
                a.x = asteroid_list[i].x;
                a.y = asteroid_list[i].y + asteroid_list[i].speed;
                a.speed = asteroid_list[i].speed;
                a.type = asteroid_list[i].type;
                a.hp = asteroid_list[i].hp;
                a.size = asteroid_list[i].size;
                asteroid_list.RemoveAt(i);
                asteroid_list.Insert(i, a);
            }

            // detect collisions
            // asteroid with ground
            for (int i = 0; i < asteroid_list.Count; i++)
            {
                Asteroid cur_asteroid = asteroid_list[i];

                // COLLISION WITH SHIP
                if ((cur_asteroid.y > (800 - cur_asteroid.size - 100)) && (Math.Abs(cur_asteroid.x - spaceship.Location.X) < 50))
                {
                    // reduce lives
                    numLives = 0;
                    Debug.WriteLine("GAME OVER");
                    // remove all lasers and asteroids
                    asteroid_list.Clear();
                    laser_list.Clear();
                    // disable timers
                    movement_timer.Stop();
                    spawn_timer.Stop();
                    level_timer.Stop();
                    movement_timer.Enabled = false;
                    spawn_timer.Enabled = false;
                    level_timer.Enabled = false;
                    // display game over message
                    countdown_label.Text = "Game Over";
                    countdown_label.Show();
                    // set boolean
                    GameOver = true;
                    lives.Text = "Lives: 0";
                }

                // COLLISION WITH FLOOR
                if (cur_asteroid.y > (800 - cur_asteroid.size))
                {
                    // reduce # lives
                    Debug.WriteLine("LIFE LOST");
                    numLives--;
                    String lives_text = "Lives: " + numLives.ToString();
                    lives.Text = lives_text;
                    // remove graphic
                    asteroid_list.RemoveAt(i);
                    // end game if # lives == 0
                    if (numLives == 0)
                    {
                        Debug.WriteLine("GAME OVER");
                        // remove all lasers and asteroids
                        asteroid_list.Clear();
                        laser_list.Clear();
                        // disable timers
                        movement_timer.Stop();
                        spawn_timer.Stop();
                        level_timer.Stop();
                        movement_timer.Enabled = false;
                        spawn_timer.Enabled = false;
                        level_timer.Enabled = false;
                        // display game over message
                        countdown_label.Text = "Game Over";
                        countdown_label.Show();
                        // set boolean
                        GameOver = true;

                    }
                }

                // COLLISION WITH LASER
                for (int j = 0; j < laser_list.Count; j++)
                {
                    Shot cur_laser = laser_list[j];
                    if ((Math.Abs(cur_asteroid.y - cur_laser.y) < cur_asteroid.size) && (Math.Abs(cur_asteroid.x - cur_laser.x) < cur_asteroid.size))
                    {
                        // decrement asteroid HP
                        cur_asteroid.hp = cur_asteroid.hp - 1;
                        Debug.WriteLine(cur_asteroid.hp);
                        // remove laser
                        laser_list.RemoveAt(j);
                        // increase points & redo label
                        points = points + point_increment;
                        String score_text = "Score: " + points.ToString();
                        score.Text = score_text;
                        // reinsert asteroid
                        if (cur_asteroid.hp != 0)
                        {
                            asteroid_list.RemoveAt(i);
                            asteroid_list.Insert(i, cur_asteroid);
                        }
                        else
                        {
                            asteroid_list.RemoveAt(i);
                        }
                    }
                }
                    
            }





            // redraw objects
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
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics asteroid;
            asteroid = this.CreateGraphics();
            asteroid.FillRectangle(myBrush, new Rectangle(placement_x, 0, size, size));
            myBrush.Dispose();
            Asteroid a = new Asteroid();
            a.graphic = asteroid;
            a.x = placement_x;
            a.y = 0;
            a.speed = rnd.Next(1, 5);
            a.type = type;
            a.hp = hp;
            a.size = size;
            asteroid_list.Add(a);

        }

        private void level_Tick(object sender, EventArgs e)
        {
            // decrement spawn time
            if (spawn_timer.Interval != level_limit)
            {
                spawn_timer.Interval = spawn_timer.Interval - level_decrement;
            }
        }

        private void countdown_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine("COUNTDOWN");
            // display countdown
            if (count == -1)
            {
                // stop timer
                countdown_timer.Enabled = false;
                countdown_timer.Stop();
                // hide label
                countdown_label.Hide();
                countdown_label.Visible = false;
            }
            else
            {
                countdown_label.Text = count.ToString();
                countdown_label.Show();

                // decrement count
                count = count - 1;
            }
        }



    }
}
