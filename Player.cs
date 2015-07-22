using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Pingpong3
{
    class Player
    {
        public PictureBox bat = new PictureBox();
        public int score = 0;
        public Player()
        { 
            
        }

        public PictureBox CreteBat(Size size,Point point)
        {
            bat.Size = size;
            bat.Location = point;
            bat.BackColor = Color.Blue;
            return bat;
        }
    }
}
