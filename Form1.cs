using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pingpong3
{
    public partial class PingPong : Form
    {
        Ball ball = new Ball();
        Player playerUser = new Player();
        Player playerComp = new Player();
        Timer gameTime;
        int gameTimeInterval=SizeParameters.GAME_INTERVAL;
        Size sizePlayer = new Size(SizeParameters.BAT_WIDTH, SizeParameters.BAT_HEIGHT);
        Size sizeComp = new Size(SizeParameters.BAT_WIDTH, SizeParameters.BAT_HEIGHT);
        Size sizeBall = new Size(SizeParameters.BALL_WIDTH, SizeParameters.BALL_HEIGHT);
        Point pointPlayer = new Point(SizeParameters.BAT_WIDTH/2, 
            SizeParameters.SCREEN_HEIGHT/2 - SizeParameters.BAT_HEIGHT/2);
        Point pointComp = new Point(SizeParameters.SCREEN_WIDTH - (SizeParameters.BAT_WIDTH), 
            SizeParameters.SCREEN_HEIGHT / 2 - SizeParameters.BAT_HEIGHT / 2);
        Random randomNumber = new Random();
        public PingPong()
        {
            InitializeComponent();
            this.MaximumSize = new Size(SizeParameters.SCREEN_HEIGHT, SizeParameters.SCREEN_WIDTH);
            this.MinimumSize = new Size(SizeParameters.SCREEN_HEIGHT, SizeParameters.SCREEN_WIDTH);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(ball.ball);
            this.Controls.Add(playerUser.CreteBat(sizePlayer,pointPlayer));
            this.Controls.Add(playerComp.CreteBat(sizeComp, pointComp));
            this.scorePlayer.Text = "0";
            this.scoreComputer.Text = "0";

            gameTime = new Timer();
            gameTime.Enabled = true;
            gameTime.Interval = gameTimeInterval;
            gameTime.Tick += new EventHandler(OnGameTimeTick);
        }

        void OnGameTimeTick(object sender, EventArgs e)
        {
            ball.ball.Location = new Point(ball.ball.Location.X + ball.ballSpeedX, ball.ball.Location.Y + ball.ballSpeedY);
            BallCollisions();
            BallBatCollision();
            MovePlayerComp();
            CheckScore();
            
             
        }

        private void BallCollisions()
        {
            if (ball.ball.Location.Y > SizeParameters.SCREEN_HEIGHT - ball.ball.Height || ball.ball.Location.Y < 0)
            {
                ball.ballSpeedY = -ball.ballSpeedY;
            }
            else if (ball.ball.Location.X > SizeParameters.SCREEN_WIDTH)
            {
                playerUser.score++;
                this.scorePlayer.Text = playerUser.score.ToString();
                resetBall();
            }
            else if (ball.ball.Location.X < 0)
            {
                playerComp.score++;
                this.scoreComputer.Text = playerComp.score.ToString();
                resetBall();
            }
        }

        private void resetBall()
        {
            ball.ball.Location = new Point(SizeParameters.SCREEN_HEIGHT/2, SizeParameters.SCREEN_WIDTH/2);
        }

        private void BallBatCollision()
        {
            if (playerUser.bat.Bounds.IntersectsWith(ball.ball.Bounds))
                ball.ballSpeedX = -ball.ballSpeedX;
            if (playerComp.bat.Bounds.IntersectsWith(ball.ball.Bounds))
                ball.ballSpeedX = -ball.ballSpeedX;
        }

        private void MovePlayerComp()
        {
            if (ball.ballSpeedX > 0)
            {
                //int newPositionY = (randomNumber.Next(1, 3)) * (ball.ball.Location.Y - playerComp.bat.Height / 2);
                //if ((newPositionY <= 0))
                //    newPositionY=SizeParameters.BAT_HEIGHT + 500;
                //if((newPositionY> 560))
                //    newPositionY=SizeParameters.BAT_HEIGHT;
                   // newPositionY = SizeParameters.SCREEN_HEIGHT / 2;
                int newPositionY = (ball.ball.Location.Y - playerComp.bat.Height / 2);
                playerComp.bat.Location = new Point(SizeParameters.SCREEN_WIDTH - (playerComp.bat.Width + playerComp.bat.Width / 2),newPositionY);
            }
        }

        private void CheckScore()
        {
            if (playerUser.score == SizeParameters.WINNING_SCORE && playerComp.score == SizeParameters.WINNING_SCORE)
            {
                playerUser.score = 0;
                playerComp.score = 0;
            }
            else if (playerUser.score == SizeParameters.WINNING_SCORE)
            {
                gameTime.Enabled = false;
                MessageBox.Show("Player Wins");
            }
            else if (playerComp.score == SizeParameters.WINNING_SCORE)
            {
                gameTime.Enabled = false;
                MessageBox.Show("Computer Wins");
            }
        }

        private void PingPong_KeyUp(object sender, KeyEventArgs e)
        {
            int playerPosX = playerUser.bat.Location.X;
            int playerPosY = playerUser.bat.Location.Y;
            switch (e.KeyData)
            {
                case Keys.Up:
                    playerUser.bat.Location = new Point(playerPosX += 0, playerPosY -= SizeParameters.STEP);
                    if (playerPosY < 0)
                        playerUser.bat.Location = new Point(playerPosX += 0, playerPosY = 0);
                    this.Refresh();
                    break;
                case Keys.Down:
                    playerUser.bat.Location = new Point(playerPosX += 0, playerPosY += SizeParameters.STEP);
                    if (playerPosY > 560)
                        playerUser.bat.Location = new Point(playerPosX += 0, playerPosY = 560);
                    this.Refresh();
                    break;
            }
        }

        private void PingPong_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphicsObj;
            graphicsObj = this.CreateGraphics();
            Pen dashed_pen = new Pen(Brushes.Red, 5);
            dashed_pen.DashPattern = new float[] { 3, 3 };
            graphicsObj.DrawLine(dashed_pen, SizeParameters.SCREEN_WIDTH/2, 0, SizeParameters.SCREEN_WIDTH/2, SizeParameters.SCREEN_HEIGHT);
        }
    }
}
