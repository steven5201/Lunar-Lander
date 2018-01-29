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
    class MenuScreen : GameScreen
    {
        PrimitiveBatch pb;
        int currentSelectedItem = 0;

        public MenuScreen()
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
            if (GlobalValues.GoToMainMenu)
            {
                GlobalValues.GoToMainMenu = false;
            }

            if (input.WasPressed(0, InputHandler.ButtonType.B, Keys.Escape))
            {
                StateManager.game.Exit();
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
                if (currentSelectedItem != 4)
                {
                    currentSelectedItem++;
                }
            }

            if (input.KeyboardState.WasKeyPressed(Keys.Space) || input.WasPressed(0, InputHandler.ButtonType.X, Keys.Enter))
            {
                switch (currentSelectedItem)
                {
                    case 0:
                        MapSelectorScreen mapSelect = new MapSelectorScreen();
                        screen.Push(mapSelect);
                        break;
                    case 1:
                        OptionsScreen options = new OptionsScreen();
                        screen.Push(options);
                        break;
                    case 2:
                        MapCreatorScreen mapCreator = new MapCreatorScreen();
                        screen.Push(mapCreator);
                        break;
                    case 3:
                        CreditsScreen credits = new CreditsScreen();
                        screen.Push(credits);
                        break;
                    case 4:
                        StateManager.game.Exit();
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
            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Main Menu");
            LinkedList<Vector2> playOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Play");
            LinkedList<Vector2> optionsOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Options");
            LinkedList<Vector2> creatorOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Map Creator");
            LinkedList<Vector2> creditsOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Credits");
            LinkedList<Vector2> exitOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Exit");

            Vector2 screenTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Main Menu", true).X, 100);
            Vector2 playOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Play", false).X, GlobalValues.ScreenCenter.Y - GlobalValues.FontYSpacer);
            Vector2 optionsOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Options", false).X, GlobalValues.ScreenCenter.Y);
            Vector2 creatorOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Map Creator", false).X, GlobalValues.ScreenCenter.Y + GlobalValues.FontYSpacer);
            Vector2 creditsOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Credits", false).X, GlobalValues.ScreenCenter.Y + (GlobalValues.FontYSpacer * 2));
            Vector2 exitOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Exit", false).X, GlobalValues.ScreenCenter.Y + (GlobalValues.FontYSpacer * 3));

            List<Vector2> positions = new List<Vector2>() { playOptionPos, optionsOptionPos, creatorOptionPos, creditsOptionPos, exitOptionPos };

            ShowSelectedIcon(positions);

            #region Foreach
            foreach (Vector2 v2 in screenTitle)
            {
                pb.AddVertex(v2 + screenTitlePos, Color.White);
            }

            foreach (Vector2 v2 in playOption)
            {
                pb.AddVertex(v2 + playOptionPos, Color.White);
            }

            foreach (Vector2 v2 in optionsOption)
            {
                pb.AddVertex(v2 + optionsOptionPos, Color.White);
            }

            foreach (Vector2 v2 in creatorOption)
            {
                pb.AddVertex(v2 + creatorOptionPos, Color.White);
            }

            foreach (Vector2 v2 in creditsOption)
            {
                pb.AddVertex(v2 + creditsOptionPos, Color.White);
            }

            foreach (Vector2 v2 in exitOption)
            {
                pb.AddVertex(v2 + exitOptionPos, Color.White);
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
