using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {
       

        int player1Score = 0;
        int player2Score = 0;

        int player1Speed = 4;
        int player2Speed = 4;
        int superPlayerSpeed = 7;

        int glow1Strength = 1;
        int glow2Strength = 1;
        int count = 0;

        bool player1Turn = true;
        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        Random randGen = new Random();

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush yellowBrush = new SolidBrush(Color.LightGoldenrodYellow);

        Rectangle player1 = new Rectangle(10, 90, 20, 20);
        Rectangle player2 = new Rectangle(10, 250, 20, 20);
        Rectangle ball = new Rectangle(295, 195, 10, 10);
        Rectangle powerUp = new Rectangle(295, 450, 8, 8);
        // Rectangle glow1 = new Rectangle(8, 80, 20, 20);
        // Rectangle glow2 = new Rectangle(8, 80, 20, 20);
        SoundPlayer scoreSound = new SoundPlayer(Properties.Resources.score);
        SoundPlayer speedSound = new SoundPlayer(Properties.Resources.speed);
        SoundPlayer winSound = new SoundPlayer(Properties.Resources.win);
        public Form1()
        {
            InitializeComponent();
            powerUp.X = randGen.Next(1, 600 - powerUp.Width);
            powerUp.Y = randGen.Next(1, 400 - powerUp.Height);
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {

                // player 1
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;

                // player 2
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;

                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {
              //move player 1
              if (wDown == true && player1.Y > 0)
              {
                  player1.Y -= player1Speed;
              }

              if (sDown == true && player1.Y < 400 - player1.Height)
              {
                  player1.Y += player1Speed;
              }

              if (aDown == true && player1.X > 0)
              {
                  player1.X -= player1Speed;
              }

              if (dDown == true && player1.X < 600 - player1.Width)
              {
                  player1.X += player1Speed;
              }

              //move player 2
              if (upArrowDown == true && player2.Y > 0)
              {
                  player2.Y -= player2Speed;
              }

              if (downArrowDown == true && player2.Y < 400 - player2.Height)
              {
                  player2.Y += player2Speed;
              }

              if (leftArrowDown == true && player2.X > 0)
              {
                  player2.X -= player2Speed;
              }

              if (rightArrowDown == true && player2.X < 600 - player2.Width)
              {
                  player2.X += player2Speed;
              }

            
            if (player1.IntersectsWith(ball))
            {
                changeBallPosition();
                player1Score++;
                p2ScoreLabel.Text = $"{player1Score}";
                
            }
            else if (player2.IntersectsWith(ball))
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";
            }

            if (player1.IntersectsWith(powerUp))
            {
                changePowerupPosition();
                player1Speed++;
                glow1Strength++;
            }
            else if (player2.IntersectsWith(powerUp))
            {
                changePowerupPosition();
                player2Speed++;
                glow2Strength++;
            }
            //check if game is over
            if (player1Score == 7)
            {
                winLabel.Visible = true;
                winLabel.Text = "Player 1 wins!!";
                gameEngine.Enabled = false;
                winSound.Play();
            }
            else if (player2Score == 7)
            {
                gameEngine.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 wins!!";
                winSound.Play();
            }
            Refresh(); // runs the Paint method
        }

        void changeBallPosition() 
        {
            ball.X = randGen.Next(1, 600 - ball.Width);
            ball.Y = randGen.Next(1, 400 - ball.Height);
            scoreSound.Play();
        }
        void changePowerupPosition()
        {
            powerUp.X = randGen.Next(1, 600 - powerUp.Width);
            powerUp.Y = randGen.Next(1, 400 - powerUp.Height);
            speedSound.Play();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush,player1.X - (2 *glow1Strength), player1.Y - (2 * glow1Strength), player1.Width + glow1Strength * 4, player1.Height + glow1Strength * 4);
            e.Graphics.FillRectangle(whiteBrush, player2.X - (2 * glow2Strength), player2.Y - (2 * glow2Strength), player2.Width + glow2Strength * 4, player2.Height + glow2Strength * 4);
            e.Graphics.FillRectangle(redBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(yellowBrush, powerUp);
        }
    }
}