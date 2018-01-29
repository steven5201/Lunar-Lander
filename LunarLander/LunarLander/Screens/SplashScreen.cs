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
    class SplashScreen : GameScreen
    {
        private PrimitiveBatch pb;
        private SpriteBatch spriteBatch;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private Texture2D myImage;
        private TimeSpan landerMove = TimeSpan.Zero;
        private Vector2 position = new Vector2(50, 200);
        private TimeSpan landerRocket = TimeSpan.Zero;
        private bool rocket1 = false;
        private float scale = 3.0f;
        private Vector2 center;
        float rotation = 1.5f;

        public SplashScreen()
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            StateManager.game.Window.Title = "Lunar Lander";

            myImage = StateManager.content.Load<Texture2D>("Splash_Mountain");

            pb = GlobalValues.PrimitiveBatch;

            spriteBatch = new SpriteBatch(StateManager.graphicsDevice);
        }

        public override void Update(GameTime gameTime, StateManager screen, GamePadState gamePadState, MouseState mouseState, KeyboardState keyboardState, InputHandler input)
        {
            if (input.KeyboardState.WasKeyPressed(Keys.Space) || input.WasPressed(0, InputHandler.ButtonType.A, Keys.A))
            {
                GoToStudio(screen);
            }

            elapsedTime += gameTime.ElapsedGameTime;

            landerRocket += gameTime.ElapsedGameTime;

            position += new Vector2(3.5f, 0);
            Sound.PlayThrust();

            if (landerRocket > TimeSpan.FromMilliseconds(100))
            {
                landerRocket -= TimeSpan.FromMilliseconds(100);
                rocket1 = !rocket1;
            }

            if (elapsedTime > TimeSpan.FromSeconds(5))
            {
                GoToStudio(screen);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Splash Screen");
            Vector2 screenTitlePos = GlobalValues.GetWordCenterPos("Splash Screen", true);

            if (GlobalValues.ValuesInitialized)
            {
                if (spriteBatch != null)
                {
                    spriteBatch.Begin();

                    spriteBatch.Draw(myImage, new Vector2(0, 0));

                    spriteBatch.End();
                }

                if (pb != null)
                {
                    pb.Begin(PrimitiveType.LineList);

                    DrawLander();
                    DrawLanderRocket();
                    DisplayKeys();

                    pb.End();
                }
                else
                {
                    pb = GlobalValues.PrimitiveBatch;
                }

            }
        }

        private void DrawLander()
        {
            foreach (Vector2 v2 in LanderValues.FillLanderLines())
            {
                float Xrotated = center.X + (v2.X - center.X) *
                    (float)Math.Cos(rotation) - (v2.Y - center.Y) *
                    (float)Math.Sin(rotation);

                float Yrotated = center.Y + (v2.X - center.X) *
                    (float)Math.Sin(rotation) + (v2.Y - center.Y) *
                    (float)Math.Cos(rotation);

                pb.AddVertex((new Vector2(Xrotated, Yrotated) * scale) + position, Color.White);//Color of the lander
            }
        }

        private void DrawLanderRocket()
        {
            foreach (Vector2 v2 in LanderValues.GetRocketCords(rocket1))
            {
                float Xrotated = center.X + (v2.X - center.X) *
                    (float)Math.Cos(rotation) - (v2.Y - center.Y) *
                    (float)Math.Sin(rotation);

                float Yrotated = center.Y + (v2.X - center.X) *
                    (float)Math.Sin(rotation) + (v2.Y - center.Y) *
                    (float)Math.Cos(rotation);

                pb.AddVertex((new Vector2(Xrotated, Yrotated) * scale) + position, Color.Red);
            }
        }

        private void DisplayKeys()
        {
            LinkedList<Vector2> display = MyFont.GetWord(GlobalValues.FontScaleSize, $"Press Space to continue or wait {5 - elapsedTime.Seconds} seconds");
            Vector2 displayPos = new Vector2(GlobalValues.GetWordCenterPos("Press Space to continue or wait 5 seconds", false).X, 270);

            foreach (Vector2 v2 in display)
            {
                pb.AddVertex(v2 + displayPos, Color.Red);
            }
        }

        private void GoToStudio(StateManager screen)
        {
            Sound.StopThrust();
            StudioScreen studio = new StudioScreen();
            screen.Pop();
            screen.Push(studio);
        }
    }
}
