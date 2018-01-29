using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameLibrary;
using GameLibrary.GUIManagement;

namespace LunarLander.Screens
{
    class StudioScreen : GameScreen
    {
        private PrimitiveBatch pb;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private Texture2D myImage;
        private SpriteBatch spriteBatch;

        public StudioScreen()
        {
            LoadContent();
            Sound.StopThrust();
        }

        public override void LoadContent()
        {
            StateManager.game.Window.Title = "Lunar Lander";

            myImage = StateManager.content.Load<Texture2D>("MyLogo");

            pb = GlobalValues.PrimitiveBatch;

            spriteBatch = new SpriteBatch(StateManager.graphicsDevice);
        }

        public override void Update(GameTime gameTime, StateManager screen, GamePadState gamePadState, MouseState mouseState, KeyboardState keyboardState, InputHandler input)
        {
            if (input.WasPressed(0, InputHandler.ButtonType.A, Keys.Space))
            {
                GoToMenu(screen);
            }

            Sound.StopThrust(); //Just in case thrust continues to play.

            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(5))
            {
                GoToMenu(screen);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Studio Screen");
            Vector2 screenTitlePos = GlobalValues.GetWordCenterPos("Studio Screen", true);

            spriteBatch.Begin();

            spriteBatch.Draw(myImage, new Vector2(275, 0));

            spriteBatch.End();

            pb.Begin(PrimitiveType.LineList);

            //foreach (Vector2 v2 in screenTitle)
            //{
            //    pb.AddVertex(v2 + screenTitlePos, Color.White);
            //}

          

            DisplayKeys();

            pb.End();
        }

        private void DisplayKeys()
        {
            LinkedList<Vector2> display = MyFont.GetWord(GlobalValues.FontScaleSize, $"Press Space to continue or wait {5 - elapsedTime.Seconds} seconds");
            Vector2 displayPos = GlobalValues.GetWordCenterPos("Press Space to continue or wait 5 seconds", false) + new Vector2(-50, 300);

            foreach (Vector2 v2 in display)
            {
                pb.AddVertex(v2 + displayPos, Color.White);
            }
        }

        private void GoToMenu(StateManager screen)
        {
            MenuScreen menu = new MenuScreen();
            screen.Pop();
            screen.Push(menu);
        }
    }
}
