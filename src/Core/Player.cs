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
    public class Player
    {
        public Tank tank;
        public Cannon canon;
        public int HP=5;
        public bool TankState;      //char actif
        public Shell shell;
        public int power=8;
        public float tmp_power=8f;
        public Rectangle Hitbox;

        public Explosion explosion,destruction;
     

        public PowerBar Power;
        public HealthBar Health;

        public Player() { }
        public Player(string side)
        {
            shell = new Shell();
            if (side == "left")
            {
                TankState = true;
                shell.ShellState = false;

                tank = new Tank();
                tank.Position = new Vector2(50, 285);
                tank.Scale = 0.5f;
                canon = new Cannon();
                canon.Position = tank.Position + new Vector2(43, 3);
                Power = new PowerBar(side);
                Health = new HealthBar(side,5);
            }
            if (side == "right")
            {
                TankState = false;
                shell.ShellState = false;

                tank = new Tank();
                tank.Position = new Vector2(700, 285);
                tank.Scale = 0.5f;
                canon = new Cannon();
                canon.Position = tank.Position + new Vector2(18, 6);
                canon.Angle = -3.14f;
                Power = new PowerBar(side);
                Health = new HealthBar(side,5);
            }
            
            canon.Scale = 0.5f;
            explosion = new Explosion(32);
            destruction = new Explosion(130);

        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            Hitbox = new Rectangle((int)tank.Position.X, (int)tank.Position.Y, (int)(tank.Texture.Width * tank.Scale), (int)(tank.Texture.Height * tank.Scale));
            canon.DrawF(spriteBatch);
            tank.DrawF(spriteBatch);
            Power.DrawR(spriteBatch);
            Health.DrawR(spriteBatch);
            if(shell.bmoving)shell.Draw(spriteBatch);
        }

        public void MoveL(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.A))
            {
                if(canon.Angle >=-1.5f) canon.Angle -= 0.01f;
            }
            if (state.IsKeyDown(Keys.Q))
            {
                if (tank.Position.X > 0)
                {
                    tank.Position -= new Vector2(1, 0);
                    canon.Position -= new Vector2(1, 0);
                    Hitbox.X--;
                }
            }
            if (state.IsKeyDown(Keys.E))
            {
                if (canon.Angle <= 0f) canon.Angle += 0.01f;
            }
            if (state.IsKeyDown(Keys.D))
            {
                if (tank.Position.X < 70)
                {
                    tank.Position += new Vector2(1, 0);
                    canon.Position += new Vector2(1, 0);
                    Hitbox.X++;
                }
            }
            if (state.IsKeyDown(Keys.Z))
            {
                if (tmp_power < 30f)
                {
                    tmp_power += 0.1f;
                    Power.AjustL(1);
                }
                power = (int)tmp_power;

            }
            if (state.IsKeyDown(Keys.S))
            {
                if (tmp_power > 8f)
                {
                    tmp_power -= 0.1f;
                    Power.AjustL(-1);
                }
                power = (int)tmp_power;
            }
        }
        public void MoveR(KeyboardState state)
        {
            if (state.IsKeyDown(Keys.A))
            {
                if (canon.Angle >= -3.14f) canon.Angle -= 0.01f;
            }
            if (state.IsKeyDown(Keys.Q))
            {
                if (tank.Position.X > 693)
                {
                    tank.Position -= new Vector2(1, 0);
                    canon.Position -= new Vector2(1, 0);
                }
            }
            if (state.IsKeyDown(Keys.E))
            {
                if (canon.Angle <= -1.7f) canon.Angle += 0.01f;
            }
            if (state.IsKeyDown(Keys.D))
            {
                if (tank.Position.X < 763)
                {
                    tank.Position += new Vector2(1, 0);
                    canon.Position += new Vector2(1, 0);
                }
            }
            if (state.IsKeyDown(Keys.Z))
            {
                if (tmp_power < 30f)
                {
                    tmp_power += 0.1f;
                    //Power.AjustR(1);
                }
                power = (int)tmp_power;
            }
            if (state.IsKeyDown(Keys.S))
            {
                if (tmp_power > 8f)
                {
                    tmp_power -= 0.1f;
                    //Power.AjustR(-1);
                }
                power = (int)tmp_power;
            }
        }
        public void Fire(KeyboardState state,SoundEffect cannon)
        {
            if (state.IsKeyDown(Keys.Space))
            {
                shell.Start(AngVec(canon.Angle),(int)tmp_power,canon.Position);
                TankState = false;
                cannon.Play();
            }
        }

        public Vector2 AngVec(float ang)
        {
            Vector2 tmp;
            int x, y;
            x = (int)(Math.Cos(ang) * 10000);
            y = (int)(Math.Sin(ang) * 10000);
            tmp = new Vector2(x, y);
            tmp.Normalize();
            return tmp;
        }

        public void Hit(Player player1, Player player2,SoundEffect hit)
        {
            if(player1.shell.Hitbox.Intersects(player2.Hitbox))
            {
                hit.Play();
                player1.shell.ShellState=true;
                this.Explosion(player1, player2);
                player2.Health.Damge();
                player2.HP--;
            }
        }
        public void Explosion(Player player1,Player player2)
        {
            if(player1.shell.ShellState)
            {

                if (player1.explosion.index == 23)
                {
                    player1.shell.ShellState = false;
                }
                if (player1.explosion.index == 24)
                {
                    player1.explosion.index = 0;
                    player1.explosion.PositionR.X = (int)shell.Position.X-13;
                    player1.explosion.PositionR.Y = (int)shell.Position.Y-13;
                }
            }

            //changement de joueur
            player2.TankState = true;
            player1.shell.ShellOut = false;
            player1.shell.Reset();

            player1.shell.Hitbox.X = 0;
            player1.shell.Hitbox.Y = 0;

        }

        public void Destruction()
        {


                if (this.destruction.index == 23)
                {
                    this.shell.ShellState = false;
                }
                if (this.destruction.index == 24)
                {
                    this.destruction.index = 0;
                    this.destruction.PositionR.X = (int)this.tank.Position.X - 38;
                    this.destruction.PositionR.Y = (int)this.tank.Position.Y - 48;
                }


            //Désactivation des commandes
                this.shell.ShellOut = false;
                this.shell.Reset();

                this.shell.Hitbox.X = 0;
                this.shell.Hitbox.Y = 0;

        }
    }
}
