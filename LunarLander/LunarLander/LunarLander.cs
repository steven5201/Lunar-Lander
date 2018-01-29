#region Usings
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using System.Collections.Generic;
using System.IO;
using System;

using GameLibrary;
#endregion

namespace LunarLander
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class LunarLander : Game
    {
        #region Variables
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private FPSCounterComponent fps;
        private StateManager screen;
        #endregion

        #region Constructor
        public LunarLander()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            Window.Position = new Point(((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - graphics.PreferredBackBufferWidth) / 2), ((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 100) - graphics.PreferredBackBufferHeight) / 2);
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = new StateManager(this);

            screen.Push(new Screens.SplashScreen());

            base.Initialize();
        }
        #endregion

        #region Load and Unload
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            #region FPS
            spriteFont = Content.Load<SpriteFont>("FPS");
            fps = new FPSCounterComponent(this, spriteBatch, spriteFont);
            Components.Add(fps);
            #endregion

            GlobalValues.InitializeGlobalValues(new PrimitiveBatch(GraphicsDevice), new Vector2(this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height / 2));

            #region Sounds
            List<SoundEffect> landerSounds = new List<SoundEffect>();
            landerSounds.Add(Content.Load<SoundEffect>("Explosion"));
            landerSounds.Add(Content.Load<SoundEffect>("RocketBooster"));
            landerSounds.Add(Content.Load<SoundEffect>("Landing"));
            Sound.SetSounds(landerSounds);
            #endregion
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        #endregion

        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            screen.Update(gameTime);

            base.Update(gameTime);
        }
        #endregion

        #region Draw
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);

            // TODO: Add your drawing code here
            screen.Draw(gameTime);

            base.Draw(gameTime);
        }
        #endregion
    }
}
