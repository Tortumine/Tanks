using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks.Core
{
    public class Shell : GameObject
    {
        private Vector2 velocity;       //stores the direction and speed of the projectile
        private Vector2 prevPos;        //previous projectile position
        private float totalTimePassed;  //time passed since start 
        public bool bmoving = false;    //if the projectile is moving
        public bool ShellState=false;   //obus explosé ou pas
        public bool ShellOut=false;     //obus sorti
        public Rectangle Hitbox;

        ///Constants
        private const float GRAVITY = -9.8f;

        public void Start(Vector2 direction, int speed, Vector2 startPos)
        {
            this.velocity = speed * Vector2.Normalize(direction);
            this.Position = startPos;   //Au début la position c'est la position du départ
            this.Hitbox = new Rectangle((int)startPos.X, (int)startPos.Y, 3, 3);
            bmoving = true;
        }

        public void UpdateArching(GameTime time)
        {
            if (Position.X < 0 || Position.X > 828)
            {
                Reset();
                ShellOut = true;
            }
            if (Position.Y > 359)
            {
                Reset();
                ShellOut = true;
            }
            if (bmoving) ArchingFlight(time);
        }

        private void ArchingFlight(GameTime timePassed)
        {
            prevPos = Position;
            // le temps s'accumule sur le trajet
            totalTimePassed += (float)timePassed.ElapsedGameTime.Milliseconds / 4096.0f;

            // calcul des coordonnées (l'axe Y est en plus affecté par le gravité)
            Position = Position + velocity * ((float)timePassed.ElapsedGameTime.Milliseconds / 90.0f);
            Position.Y = Position.Y - 0.5f * GRAVITY * totalTimePassed * totalTimePassed;

            Hitbox.X = (int)Position.X;
            Hitbox.Y = (int)Position.Y;
        }
        public void Reset()
        {
            bmoving = false;
            velocity = new Vector2(0,0);
            totalTimePassed=0;
        }
    }
}
