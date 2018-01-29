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
    class ChangeVolumeScreen : GameScreen
    {
        PrimitiveBatch pb;
        float volume = Sound.CurrentVolume();

        public ChangeVolumeScreen()
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            StateManager.game.Window.Title = "Lunar Lander";

            pb = GlobalValues.PrimitiveBatch;
        }

        public override void Update(GameTime gameTime, StateManager screen, GamePadState gamePadState, MouseState mouseState, KeyboardState keyboardState, InputHandler input)
        {
            if (input.WasPressed(0, InputHandler.ButtonType.B, Keys.Escape))
            {
                screen.Pop();
            }

            if (input.KeyboardState.WasKeyPressed(Keys.A) || input.WasPressed(0, InputHandler.ButtonType.LeftShoulder, Keys.Left))
            {
                if (input.KeyboardState.IsHoldingKey(Keys.RightControl) || input.KeyboardState.IsHoldingKey(Keys.LeftControl))
                {
                    volume -= .1f;
                }
                else if (input.KeyboardState.IsHoldingKey(Keys.RightShift) || input.KeyboardState.IsHoldingKey(Keys.LeftShift))
                {
                    volume -= .05f;
                }
                else
                {
                    volume -= .01f;
                }
            }

            if (input.KeyboardState.WasKeyPressed(Keys.D) || input.WasPressed(0, InputHandler.ButtonType.LeftShoulder, Keys.Right))
            {
                if (input.KeyboardState.IsHoldingKey(Keys.RightControl) || input.KeyboardState.IsHoldingKey(Keys.LeftControl))
                {
                    volume += .1f;
                }
                else if (input.KeyboardState.IsHoldingKey(Keys.RightShift) || input.KeyboardState.IsHoldingKey(Keys.LeftShift))
                {
                    volume += .05f;
                }
                else
                {
                    volume += .01f;
                }
            }

            if (volume < 0)
            {
                volume = 0;
            }

            if (volume > 1)
            {
                volume = 1;
            }

            volume = (float)Math.Round(volume, 2);

            if (input.WasPressed(0, InputHandler.ButtonType.X, Keys.Enter))
            {
                Sound.ChangeVolume(volume);
                screen.Pop();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            pb.Begin(PrimitiveType.LineList);

            ShowMenu();
            ShowControls();

            pb.End();
        }

        private void ShowMenu()
        {
            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Volume");
            LinkedList<Vector2> currentVolume = MyFont.GetWord(GlobalValues.FontScaleSize, (volume * 100).ToString());

            Vector2 screenTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Volume", true).X, 100);
            Vector2 currentVolumePos = new Vector2(GlobalValues.GetWordCenterPos((volume * 100).ToString(), false).X, GlobalValues.ScreenCenter.Y);

            #region Foreach
            foreach (Vector2 v2 in screenTitle)
            {
                pb.AddVertex(v2 + screenTitlePos, Color.White);
            }

            foreach (Vector2 v2 in currentVolume)
            {
                pb.AddVertex(v2 + currentVolumePos, Color.White);
            }
            #endregion
        }

        private void ShowControls()
        {
            float yStart = 500;

            LinkedList<Vector2> leftRight = MyFont.GetWord(GlobalValues.FontScaleSize, "Use left:right arrow keys or a:d to change volume");
            LinkedList<Vector2> amounts = MyFont.GetWord(GlobalValues.FontScaleSize, "hold control to change by 10 or shift to change by 5");
            LinkedList<Vector2> leave = MyFont.GetWord(GlobalValues.FontScaleSize, "press escape to leave without saving changes");
            LinkedList<Vector2> confirm = MyFont.GetWord(GlobalValues.FontScaleSize, "press enter to update volume");

            Vector2 leftRightPos = new Vector2(GlobalValues.GetWordCenterPos("Use left:right arrow keys or a:d to change volume", false).X, yStart);
            Vector2 amountsPos = new Vector2(GlobalValues.GetWordCenterPos("hold control to change by 10 or shift to change by 5", false).X, yStart + GlobalValues.FontYSpacer);
            Vector2 leavePos = new Vector2(GlobalValues.GetWordCenterPos("press escape to leave without saving changes", false).X, yStart + (GlobalValues.FontYSpacer * 2));
            Vector2 confirmPos = new Vector2(GlobalValues.GetWordCenterPos("press enter to update volume", false).X, yStart + (GlobalValues.FontYSpacer * 3));

            #region Foreach
            foreach (Vector2 v2 in leftRight)
            {
                pb.AddVertex(v2 + leftRightPos, Color.White);
            }

            foreach (Vector2 v2 in amounts)
            {
                pb.AddVertex(v2 + amountsPos, Color.White);
            }

            foreach (Vector2 v2 in leave)
            {
                pb.AddVertex(v2 + leavePos, Color.White);
            }

            foreach (Vector2 v2 in confirm)
            {
                pb.AddVertex(v2 + confirmPos, Color.White);
            }
            #endregion
        }
    }
}
