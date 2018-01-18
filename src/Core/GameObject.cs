using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tanks.Core
{
    public class GameObject
    {
        #region Données membres
        public Vector2 Position;
        public Rectangle PositionR;
        
        public Texture2D Texture;
        public Rectangle Source;
        public Vector2 Origin;

        public float Angle=0;
        public float Scale=1.0f;
        public float time;
        public float frameTime = 0.1f;
        private int _totalFrames;
        public int totalFrames
        {
            get { return _totalFrames; }
        }
        private int _frameWidth;
        public int frameWidth
        {
            get { return _frameWidth; }
        }
        private int _frameHeight;
        public int frameHeight
        {
            get { return _frameHeight; }
        }
        public int index = 24;
        List<Rectangle> frames = new List<Rectangle>();
        #endregion

        #region constructeur
        public GameObject()
        {
        }
        public GameObject(int width, int height, int X_frames, int Y_frames,int sizeX,int sizeY)
        {
            //calcul et découpage des frames depuis le png
            int frameWidth = width / X_frames;
            int frameHeight = height / Y_frames;
            _totalFrames = X_frames * Y_frames;

            int i, j;
            PositionR = new Rectangle(0,0, sizeX, sizeY);

            for (i = 0; i < height; i += frameHeight)
            {
                for (j = 0; j < width; j += frameWidth)
                {
                    frames.Add(new Rectangle(j, i, frameWidth, frameHeight));
                }
            }
        }

        #endregion

        #region Méthodes
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
        public void DrawR(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, PositionR, Source, Color.White);
        }
        public void DrawF(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Angle,Origin, Scale, SpriteEffects.None, 0f);
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, PositionR,Source,Color.White);
        }
        public void UpdateFrame(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds*3;

            while (time > frameTime)
            {
                if (index < 24)
                {
                    index++;
                }
                this.time = 0f;
            }

            this.Source = frames[index];

        }

        #endregion
    }

}
