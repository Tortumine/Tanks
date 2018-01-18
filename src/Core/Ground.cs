using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace Tanks.Core
{
    class Ground : GameObject
    {
        //public List<Rectangle> Hitbox;
        Rectangle Hitbox;
        
        int width1 = 827;
        int height1 = 25;

        public Ground()
        {
            //Hitbox = new List<Rectangle>();
            Rectangle tmp = new Rectangle(0, 312, width1, height1);
            Hitbox = tmp;
            //this.Hitbox.Add(tmp);
        }
        public void Colision(Player PL1,Player PL2, SoundEffect hit)
        {
            if (PL1.shell.Hitbox.Intersects(Hitbox))
            {
                hit.Play();
                PL1.shell.ShellState = true;
                PL1.Explosion(PL1, PL2);

                //changement de joueur
                PL2.TankState = true;
                PL1.shell.ShellOut = false;
                PL1.shell.Reset();

                PL1.shell.Hitbox.X = 0;
                PL1.shell.Hitbox.Y = 0;
            }
        }
    }
}
