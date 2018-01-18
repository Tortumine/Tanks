using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Tanks.Core;

namespace Tanks
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //taille fenetre
        public const int WINDOW_WIDTH = 827;
        public const int WINDOW_HEIGHT = 358;


        World world;
        private Ground ground;
        Player playerl, playerr;
        SoundEffect sound, shot, hit;
        SaveLoad test = new SaveLoad();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            world = new World();
            ground = new Ground();
            playerl = new Player("left");
            playerr = new Player("right");
            SaveLoad save = new SaveLoad();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            spriteBatch = new SpriteBatch(GraphicsDevice);
            // 
            world.Texture = Content.Load<Texture2D>("Images/World/back_ground");
            ground.Texture = Content.Load<Texture2D>("Images/World/ground");
            world.Position = new Vector2(0, 0);

            playerl.canon.Texture = Content.Load<Texture2D>("Images/Players/cannon_L");
            playerl.tank.Texture = Content.Load<Texture2D>("Images/Players/tank_L");
            playerl.shell.Texture = Content.Load<Texture2D>("Images/Players/shell");

            playerr.canon.Texture = Content.Load<Texture2D>("Images/Players/cannon_l");
            playerr.tank.Texture = Content.Load<Texture2D>("Images/Players/tank_R");
            playerr.shell.Texture = Content.Load<Texture2D>("Images/Players/shell");

            playerr.explosion.Texture = Content.Load<Texture2D>("Images/Players/Explosion");
            playerl.explosion.Texture = Content.Load<Texture2D>("Images/Players/Explosion");
            playerr.destruction.Texture = Content.Load<Texture2D>("Images/Players/Explosion");
            playerl.destruction.Texture = Content.Load<Texture2D>("Images/Players/Explosion");
            
            playerl.Power.Texture = Content.Load<Texture2D>("Images/Players/Power_L");
            playerr.Power.Texture = Content.Load<Texture2D>("Images/Players/Power_R");

            playerl.Health.Texture = Content.Load<Texture2D>("Images/Players/Health");
            playerr.Health.Texture = Content.Load<Texture2D>("Images/Players/Health");

            /// Sound

            shot = Content.Load<SoundEffect>("Sounds/fire");
            hit = Content.Load<SoundEffect>("Sounds/explosion");

            // Uncomment to load a background music
            //sound = Content.Load<SoundEffect>("music");
            //SoundEffectInstance instance = sound.CreateInstance();
            //instance.IsLooped = true;
            //instance.Volume = 0.5f;

            //instance.Play();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (playerl.TankState)
            {
                playerl.MoveL(Keyboard.GetState());
                if (!playerl.shell.bmoving) playerl.Fire(Keyboard.GetState(), shot);
            }
            if (playerr.TankState)
            {
                playerr.MoveR(Keyboard.GetState());
                if (!playerr.shell.bmoving) playerr.Fire(Keyboard.GetState(), shot);
            }

            playerl.Hit(playerl, playerr, hit);
            playerr.Hit(playerr, playerl, hit);

            ground.Colision(playerl, playerr, hit);
            ground.Colision(playerr, playerl, hit);

            if (playerr.shell.ShellOut)
            {
                playerr.Explosion(playerr, playerl);
            }
            if (playerl.shell.ShellOut)
            {
                playerl.Explosion(playerl, playerr);
            }

            if (playerl.shell.bmoving) playerl.shell.UpdateArching(gameTime);
            if (playerr.shell.bmoving) playerr.shell.UpdateArching(gameTime);

            if (playerl.HP == 0) playerl.Destruction();
            if (playerr.HP == 0) playerr.Destruction();

            playerl.explosion.UpdateFrame(gameTime);
            playerr.explosion.UpdateFrame(gameTime);

            playerl.destruction.UpdateFrame(gameTime);
            playerr.destruction.UpdateFrame(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.F9))
            {
                test.Save(playerl, playerl, "save_test.xna");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F10))
            {
                test.Load(playerl, playerl, "save_test.xna");
            }
            playerl.Power.UpdateL(playerl.tmp_power);
            playerr.Power.UpdateR(playerr.tmp_power);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            world.Draw(spriteBatch);
            ground.Draw(spriteBatch);
            playerl.Draw(spriteBatch);
            playerr.Draw(spriteBatch);

            playerl.explosion.DrawAnimation(spriteBatch);
            playerr.explosion.DrawAnimation(spriteBatch);

            playerl.destruction.DrawAnimation(spriteBatch);
            playerr.destruction.DrawAnimation(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
