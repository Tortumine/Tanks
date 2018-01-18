using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.Core
{
    // classe dédiée à la gestion de la puissance et de la vie
    public class PowerBar : GameObject
    {
        float tmp = 32;
        public PowerBar(string side)
        {
            
            if(side=="left")
            {
                //rectange source
                Source.X = 0;
                Source.Y = 0;
                Source.Width = 32;
                Source.Height = 32;

                //rectangle destination
                PositionR.X = 10;
                PositionR.Y = 50;
                PositionR.Width = 32;
                PositionR.Height = 32;
            }
            if (side == "right")
            {
                //rectange source
                Source.X = 96;
                Source.Y = 0;
                Source.Width = 32;
                Source.Height = 32;

                //rectangle destination
                PositionR.X = 785;
                PositionR.Y = 50;
                PositionR.Width = 32;
                PositionR.Height = 32;
            }
        }
        // les valeurs sont utilisables uniquement aves le format des sprite fournit (paysage 827p, triagle 128x32p)
        public void AjustL(int entree)
        {
            tmp += entree*0.4f;
            PositionR.Width = (int)tmp;
            Source.Width = (int)tmp;
        }
        public void AjustR(int entree)
        {
            tmp += entree * 0.4f;
            PositionR.X = 817 - (int)tmp;
            PositionR.Width = (int)tmp;
            Source.X = 128 - (int)tmp;
            Source.Width = (int)tmp;
        }
        public void UpdateL(float entree)
        {
            float tmp1 =8+ entree * 4f;
            PositionR.Width = (int)tmp1;
            Source.Width = (int)tmp1;
        }
        public void UpdateR(float entree)
        {
            float tmp1 = 8+ entree * 4f;
            PositionR.X = 817 - (int)tmp1;
            PositionR.Width = (int)tmp1;
            Source.X = 128 - (int)tmp1;
            Source.Width = (int)tmp1;
        }
    }
}
