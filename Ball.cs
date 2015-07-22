using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Pingpong3
{
    class Ball
    {
        Size sizeBall = new Size(SizeParameters.BALL_WIDTH, SizeParameters.BALL_HEIGHT);
        public PictureBox ball;
        public int ballSpeedX = SizeParameters.BALL_SPEEDX;
        public int ballSpeedY = SizeParameters.BALL_SPEEDY;
        public Ball()
        {
            ball = new PictureBox();
            ball.Size = sizeBall;
            ball.Location = new Point(SizeParameters.SCREEN_WIDTH/2 - SizeParameters.BALL_WIDTH/2, 
                SizeParameters.SCREEN_HEIGHT / 2 - SizeParameters.BALL_HEIGHT / 2);
            ball.BackColor = Color.Green;
        }
    }
}
