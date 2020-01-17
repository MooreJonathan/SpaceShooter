using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace SpaceShooterAttemptTwo
{
    public partial class Form1 : Form
    {

        WindowsMediaPlayer gameMedia;
        WindowsMediaPlayer shootgMedia;

        PictureBox[] stars;
        int backgroundspeed;
        int playerSpeed;

        PictureBox[] bullets;
        int bulletSpeed;

        Random rnd;

        public Form1()
        {
            InitializeComponent();
        }
        
        
        private void Form1_Load_1(object sender, EventArgs e)
        {
            backgroundspeed = 4;
            playerSpeed = 4;
            bulletSpeed = 16;

            bullets = new PictureBox[3];
            stars = new PictureBox[15];
            rnd = new Random();

            // Bullet image properties
            Image bullet = Image.FromFile(@"asserts\munition.png");

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new PictureBox();
                bullets[i].Size = new Size(8, 8);
                bullets[i].Image = bullet;
                bullets[i].SizeMode = PictureBoxSizeMode.Zoom;
                bullets[i].BorderStyle = BorderStyle.None;
                bullets[i].Location = new Point(rnd.Next(20, 580), rnd.Next(-10, 400));
                this.Controls.Add(bullets[i]);
            }

            // sets the varied speed for the stars
            for (var i = 0; i < stars.Length; i++)
            {
                stars[i] = new PictureBox();
                stars[i].BorderStyle = BorderStyle.None;
                stars[i].Location = new Point(rnd.Next(20, 580), rnd.Next(-10, 400));

                // stars speed 1
                if (i % 2 == 1)
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.Wheat;
                }

                // stars speed 2
                else
                {
                    stars[i].Size = new Size(3, 3);
                    stars[i].BackColor = Color.DarkGray;
                }

                this.Controls.Add(stars[i]);
            }

            //Create WMP
            gameMedia = new WindowsMediaPlayer();
            shootgMedia = new WindowsMediaPlayer();

            // Load mp3 files
            gameMedia.URL = "songs\\GameSong.mp3";
            shootgMedia.URL = "songs\\shoot.mp3";

            // Song Settings
            gameMedia.settings.setMode("loop", true);
            gameMedia.settings.volume = 5;
            shootgMedia.settings.volume = 1;

            gameMedia.controls.play();
        }

        // Movement Timers
        private void MoveBgTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < stars.Length / 2; i++)
            {
                stars[i].Top += backgroundspeed;

                if (stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }

            for (int i = stars.Length / 2; i < stars.Length; i++)
            {
                stars[i].Top += backgroundspeed - 2;

                if (stars[i].Top >= this.Height)
                {
                    stars[i].Top = -stars[i].Height;
                }
            }
        }
        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Left > 10)
            {
                Player.Left -= playerSpeed;
            }
        }
        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Right < 580)
            {
                Player.Left += playerSpeed;
            }
        }
        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top > 10)
            {
                Player.Top -= playerSpeed;
            }
        }
        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if (Player.Top < 400)
            {
                Player.Top += playerSpeed;
            }
        }

        // Starts movement timers on keydown
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                RightMoveTimer.Start();
            }
            if (e.KeyCode == Keys.Left)
            {
                LeftMoveTimer.Start();
            }
            if (e.KeyCode == Keys.Up)
            {
                UpMoveTimer.Start();
            }
            if (e.KeyCode == Keys.Down)
            {
                DownMoveTimer.Start();
            }

        }

        // Stops all movement timers on keyup
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            RightMoveTimer.Stop();
            LeftMoveTimer.Stop();
            UpMoveTimer.Stop();
            DownMoveTimer.Stop();
        }

        //Bullet Timer Control
        private void MoveBulletTimer_Tick(object sender, EventArgs e)
        {
            shootgMedia.controls.play();
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].Top > 0)
                {
                    bullets[i].Visible = true;
                    bullets[i].Top -= bulletSpeed;
                }
                else 
                {
                    bullets[i].Visible = false;
                    bullets[i].Location = new Point(Player.Location.X + 20, Player.Location.Y - i * 30);
                }
            }
        }
    }
}
