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
    class MapSelectorScreen : GameScreen
    {
        PrimitiveBatch pb;
        int currentSelectedItem = 0;
        List<string> mapNames;

        public MapSelectorScreen()
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            StateManager.game.Window.Title = "Lunar Lander";

            pb = GlobalValues.PrimitiveBatch;

            mapNames = Terrain.LoadMapsNames();
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
                if (currentSelectedItem != mapNames.Count)
                {
                    currentSelectedItem++;
                }
            }

            if (input.KeyboardState.WasKeyPressed(Keys.Space) || input.WasPressed(0, InputHandler.ButtonType.X, Keys.Enter))
            {
                if (currentSelectedItem == 0)
                {
                    screen.Pop();
                }
                else
                {
                    Terrain.ClearLevelValues();
                    GlobalValues.LevelLoaded = mapNames[currentSelectedItem - 1];
                    Terrain.LoadTerrain(GlobalValues.LevelLoaded);
                    Terrain.LoadPads(GlobalValues.LevelLoaded);

                    PlayScreen play = new PlayScreen();
                    screen.Pop();
                    screen.Push(play);
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
            float yStart = (GlobalValues.ScreenCenter.Y - (GlobalValues.FontYSpacer * 2));

            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Map Select");
            LinkedList<Vector2> mainMenuOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Main Menu");

            Vector2 screenTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Map Select", true).X, 100);
            Vector2 mainMenuOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Main Menu", false).X, GlobalValues.ScreenCenter.Y - (GlobalValues.FontYSpacer * 3));

            foreach (Vector2 v2 in screenTitle)
            {
                pb.AddVertex(v2 + screenTitlePos, Color.White);
            }

            foreach (Vector2 v2 in mainMenuOption)
            {
                pb.AddVertex(v2 + mainMenuOptionPos, Color.White);
            }

            LinkedList<Vector2> option = new LinkedList<Vector2>();
            Vector2 optionPos = new Vector2();

            List<Vector2> positions = new List<Vector2>() { mainMenuOptionPos };

            int mapNum = 0;

            foreach (string map in mapNames)
            {
                option = MyFont.GetWord(GlobalValues.FontScaleSize, map);
                optionPos = new Vector2(GlobalValues.GetWordCenterPos(map, false).X, yStart + (GlobalValues.FontYSpacer * mapNum));

                foreach (Vector2 v2 in option)
                {
                    pb.AddVertex(v2 + optionPos, Color.White);
                }

                positions.Add(optionPos);

                mapNum++;
            }

            ShowSelectedIcon(positions);
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
