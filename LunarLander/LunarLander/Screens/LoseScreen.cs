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
    class LoseScreen : GameScreen
    {
        PrimitiveBatch pb;
        int currentSelectedItem = 0;
        GUIManager guiManager;
        List<GUIItem> guiItems;

        public LoseScreen()
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            StateManager.game.Window.Title = "Lunar Lander";

            pb = GlobalValues.PrimitiveBatch;

            guiManager = new GUIManager(GlobalValues.TitleFontScaleSize, GlobalValues.FontScaleSize, GlobalValues.FontYSpacer, GlobalValues.PrimitiveBatch, GlobalValues.ScreenCenter);

            guiItems = new List<GUIItem>();
            guiItems.Add(new GUIItem(true, "Game Over", false, new Vector2(guiManager.GetWordCenterPos("Game Over", true).X, 100), Color.Red));
            guiItems.Add(new GUIItem(false, "Restart", true, new Vector2(guiManager.GetWordCenterPos("Restart", false).X, GlobalValues.ScreenCenter.Y - GlobalValues.FontYSpacer), Color.White));
            guiItems.Add(new GUIItem(false, "Main Menu", true, new Vector2(guiManager.GetWordCenterPos("Main Menu", false).X, GlobalValues.ScreenCenter.Y), Color.White));
        }

        public override void Update(GameTime gameTime, StateManager screen, GamePadState gamePadState, MouseState mouseState, KeyboardState keyboardState, InputHandler input)
        {
            Sound.StopThrust();

            if (input.WasPressed(0, InputHandler.ButtonType.B, Keys.Escape))
            {
                //Pop both this screen and the play screen
                GlobalValues.GoToMainMenu = true;
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
                if (currentSelectedItem != 1)
                {
                    currentSelectedItem++;
                }
            }

            if (input.KeyboardState.WasKeyPressed(Keys.Space) || input.WasPressed(0, InputHandler.ButtonType.X, Keys.Enter))
            {
                Sound.StopExplosion();
                switch (currentSelectedItem)
                {
                    case 0:
                        screen.Pop();
                        GlobalValues.ResetGame = true;
                        break;
                    case 1:
                        //Pop both this screen and the play screen
                        GlobalValues.GoToMainMenu = true;
                        screen.Pop();
                        break;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            pb.Begin(PrimitiveType.LineList);

            guiManager.ShowGUI(guiItems);
            guiManager.ShowSelectedIcon(currentSelectedItem);

            pb.End();
        }
    }
}
