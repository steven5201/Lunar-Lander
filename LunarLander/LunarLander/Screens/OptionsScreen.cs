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
    class OptionsScreen : GameScreen
    {
        PrimitiveBatch pb;
        int currentSelectedItem = 0;

        public OptionsScreen()
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

            if (input.KeyboardState.WasKeyPressed(Keys.W) || input.WasPressed(0, InputHandler.ButtonType.X, Keys.Up))
            {
                if (currentSelectedItem != 0)
                {
                    currentSelectedItem--;
                }
            }

            if (input.KeyboardState.WasKeyPressed(Keys.S) || input.WasPressed(0, InputHandler.ButtonType.X, Keys.Down))
            {
                if (currentSelectedItem != 5)
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
                        screen.Push(new ManageMapsScreen());
                        break;
                    case 2:
                        screen.Push(new ChangeVolumeScreen());
                        break;
                    case 3:
                        screen.Push(new ChangeResolutionScreen());
                        break;
                    case 4:
                        screen.Push(new ChangeDifficultyScreen());
                        break;
                    case 5:
                        screen.Push(new ChangeGravityScreen());
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
            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Options");
            LinkedList<Vector2> mainMenuOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Main Menu");
            LinkedList<Vector2> option1Option = MyFont.GetWord(GlobalValues.FontScaleSize, "Manage Maps");
            LinkedList<Vector2> option2Option = MyFont.GetWord(GlobalValues.FontScaleSize, "Change Volume");
            LinkedList<Vector2> option3Option = MyFont.GetWord(GlobalValues.FontScaleSize, "Change Resolution");
            LinkedList<Vector2> option4Option = MyFont.GetWord(GlobalValues.FontScaleSize, "Change Difficulty");
            LinkedList<Vector2> option5Option = MyFont.GetWord(GlobalValues.FontScaleSize, "Change Gravity");

            Vector2 screenTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Options", true).X, 100);
            Vector2 mainMenuOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Main Menu", false).X, GlobalValues.ScreenCenter.Y - (GlobalValues.FontYSpacer * 2));
            Vector2 option1OptionPos = new Vector2(GlobalValues.GetWordCenterPos("Manage Maps", false).X, GlobalValues.ScreenCenter.Y - GlobalValues.FontYSpacer);
            Vector2 option2OptionPos = new Vector2(GlobalValues.GetWordCenterPos("Change Volume", false).X, GlobalValues.ScreenCenter.Y);
            Vector2 option3OptionPos = new Vector2(GlobalValues.GetWordCenterPos("Change Resolution", false).X, GlobalValues.ScreenCenter.Y + GlobalValues.FontYSpacer);
            Vector2 option4OptionPos = new Vector2(GlobalValues.GetWordCenterPos("Change Difficulty", false).X, GlobalValues.ScreenCenter.Y + (GlobalValues.FontYSpacer * 2));
            Vector2 option5OptionPos = new Vector2(GlobalValues.GetWordCenterPos("Change Gravity", false).X, GlobalValues.ScreenCenter.Y + (GlobalValues.FontYSpacer * 3));

            List<Vector2> positions = new List<Vector2>() { mainMenuOptionPos, option1OptionPos, option2OptionPos, option3OptionPos, option4OptionPos, option5OptionPos };

            ShowSelectedIcon(positions);

            #region Foreach
            foreach (Vector2 v2 in screenTitle)
            {
                pb.AddVertex(v2 + screenTitlePos, Color.White);
            }

            foreach (Vector2 v2 in mainMenuOption)
            {
                pb.AddVertex(v2 + mainMenuOptionPos, Color.White);
            }

            foreach (Vector2 v2 in option1Option)
            {
                pb.AddVertex(v2 + option1OptionPos, Color.White);
            }

            foreach (Vector2 v2 in option2Option)
            {
                pb.AddVertex(v2 + option2OptionPos, Color.White);
            }

            foreach (Vector2 v2 in option3Option)
            {
                pb.AddVertex(v2 + option3OptionPos, Color.White);
            }

            foreach (Vector2 v2 in option4Option)
            {
                pb.AddVertex(v2 + option4OptionPos, Color.White);
            }

            foreach (Vector2 v2 in option5Option)
            {
                pb.AddVertex(v2 + option5OptionPos, Color.White);
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
