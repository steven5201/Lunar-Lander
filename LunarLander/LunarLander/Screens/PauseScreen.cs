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
    class PauseScreen : GameScreen
    {
        PrimitiveBatch pb;
        int currentSelectedItem = 0;

        public PauseScreen()
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
            if (input.KeyboardState.WasKeyPressed(Keys.Escape) || input.WasPressed(0, InputHandler.ButtonType.B, Keys.P))
            {
                screen.Pop();
            }

            if (input.WasPressed(0, InputHandler.ButtonType.B, Keys.R))
            {
                screen.Pop();
                GlobalValues.ResetGame = true;
            }

            if (input.KeyboardState.WasKeyPressed(Keys.W) || input.WasPressed(0, InputHandler.ButtonType.X, Keys.Up))
            {
                if (currentSelectedItem != 0)
                {
                    currentSelectedItem--;
                }
            }

            if (input.KeyboardState.WasKeyPressed(Keys.S) || input.WasPressed(0, InputHandler.ButtonType.X, Keys.Down))
            {
                if (currentSelectedItem != 2)
                {
                    currentSelectedItem++;
                }
            }

            if (input.KeyboardState.WasKeyPressed(Keys.Space) || input.WasPressed(0, InputHandler.ButtonType.X, Keys.Enter))
            {
                switch (currentSelectedItem)
                {
                    case 0:
                        screen.Pop();
                        break;
                    case 1:
                        screen.Pop();
                        GlobalValues.ResetGame = true;
                        break;
                    case 2:
                        //Close play and this to go back to main menu
                        GlobalValues.GoToMainMenu = true;
                        screen.Pop();
                        break;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            pb.Begin(PrimitiveType.LineList);

            ShowMenuOptions();

            pb.End();
        }

        private void ShowMenuOptions()
        {
            float xPos = GlobalValues.ScreenCenter.X - ("Restart".Length * (GlobalValues.FontScaleSize * 2));
            float yStart = GlobalValues.ScreenCenter.Y;

            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Pause");
            LinkedList<Vector2> continueOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Continue");
            LinkedList<Vector2> restartOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Restart");
            LinkedList<Vector2> mainMenuOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Main Menu");

            Vector2 screenTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Pause", true).X, 100);
            Vector2 continueOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Continue", false).X, GlobalValues.ScreenCenter.Y - GlobalValues.FontYSpacer);
            Vector2 restartOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Restart", false).X, GlobalValues.ScreenCenter.Y);
            Vector2 mainMenuOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Main Menu", false).X, GlobalValues.ScreenCenter.Y + GlobalValues.FontYSpacer);

            List<Vector2> positions = new List<Vector2>() { continueOptionPos, restartOptionPos, mainMenuOptionPos };

            ShowSelectedIcon(positions);

            #region Foreach
            foreach (Vector2 v2 in screenTitle)
            {
                pb.AddVertex(v2 + screenTitlePos, Color.White);
            }

            foreach (Vector2 v2 in continueOption)
            {
                pb.AddVertex(v2 + continueOptionPos, Color.White);
            }

            foreach (Vector2 v2 in restartOption)
            {
                pb.AddVertex(v2 + restartOptionPos, Color.White);
            }

            foreach (Vector2 v2 in mainMenuOption)
            {
                pb.AddVertex(v2 + mainMenuOptionPos, Color.White);
            }
            #endregion
        }

        private void ShowSelectedIcon(List<Vector2> itemPositionList)
        {
            LinkedList<Vector2> icon = GlobalValues.GetSelectedIcon();

            foreach (Vector2 v2 in icon)
            {
                pb.AddVertex(v2 + (itemPositionList[currentSelectedItem] - new Vector2(20, 0)), Color.White);
            }
        }
    }
}
