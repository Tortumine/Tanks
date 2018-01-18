using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Core
{
    
    public class HealthBar : GameObject
    {
        private string Side;
        public HealthBar(string side,int hp)
        {

            if (side == "left")
            {
                Side = side;

                //rectange source
                Source.X = 0;
                Source.Y = 0;
                Source.Width = 160;
                Source.Height = 32;

                //rectangle destination
                PositionR.X = 10;
                PositionR.Y = 10;
                PositionR.Width = 32*hp;
                PositionR.Height = 32;
            }
            if (side == "right")
            {
                Side = side;

                //rectange source
                Source.X = 0;
                Source.Y = 0;
                Source.Width = 160;
                Source.Height = 32;

                //rectangle destination
                PositionR.X = 657+32*(5- hp);
                PositionR.Y = 10;
                PositionR.Width = 32*5;
                PositionR.Height = 32;
            }
        }
        public void Damge()
        {
            if (Side == "left")
            {
                PositionR.Width = PositionR.Width-32;
            }
            if (Side == "right")
            {
                PositionR.Width = PositionR.Width - 32;
                PositionR.X = PositionR.X + 32;
            }
        }

    }
}
