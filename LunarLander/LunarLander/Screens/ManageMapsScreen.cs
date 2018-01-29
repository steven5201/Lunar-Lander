#region Usings
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameLibrary;
using GameLibrary.GUIManagement;
#endregion

namespace LunarLander.Screens
{
    class ManageMapsScreen : GameScreen
    {
        #region Variables
        PrimitiveBatch pb;
        int currentSelectedItem = 0;
        int oldSelectedItem = 0;
        List<string> mapNames;
        private bool showConfirmation = false;
        #endregion

        #region Initialization
        public ManageMapsScreen()
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            StateManager.game.Window.Title = "Lunar Lander";

            pb = GlobalValues.PrimitiveBatch;

            mapNames = Terrain.LoadMapsNames();

            if (mapNames.Contains("Random"))
            {
                mapNames.Remove("Random");
            }
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime, StateManager screen, GamePadState gamePadState, MouseState mouseState, KeyboardState keyboardState, InputHandler input)
        {
            #region Button Presses
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
                if (!showConfirmation)
                {
                    if (currentSelectedItem != mapNames.Count)
                    {
                        currentSelectedItem++;
                    }
                }
                else
                {
                    if (currentSelectedItem != 1)
                    {
                        currentSelectedItem++;
                    }
                }
            }

            if (input.KeyboardState.WasKeyPressed(Keys.Space) || input.WasPressed(0, InputHandler.ButtonType.X, Keys.Enter))
            {
                if (!showConfirmation)
                {
                    if (currentSelectedItem == 0)
                    {
                        screen.Pop();
                    }
                    else
                    {
                        oldSelectedItem = currentSelectedItem;
                        currentSelectedItem = 0;
                        showConfirmation = true;
                    }
                }
                else
                {
                    if (currentSelectedItem == 0)
                    {
                        showConfirmation = false;
                        currentSelectedItem = oldSelectedItem;
                        UpdateFiles(mapNames[currentSelectedItem - 1]);
                    }
                    else
                    {
                        showConfirmation = false;
                        currentSelectedItem = oldSelectedItem;
                    }
                }
            }
            #endregion
        }
        #endregion

        #region Draw
        public override void Draw(GameTime gameTime)
        {
            pb.Begin(PrimitiveType.LineList);

            if (!showConfirmation)
            {
                ShowMenuOptions();
            }
            else
            {
                ShowConfirmationOptions();
            }

            pb.End();
        }

        private void ShowMenuOptions()
        {
            float yStart = (GlobalValues.ScreenCenter.Y - (GlobalValues.FontYSpacer * 2));

            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Map Manager");
            LinkedList<Vector2> mainMenuOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Main Menu");

            Vector2 screenTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Map Manager", true).X, 100);
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

        private void ShowConfirmationOptions()
        {
            float yStart = (GlobalValues.ScreenCenter.Y - (GlobalValues.FontYSpacer * 5));

            LinkedList<Vector2> areYouSure = MyFont.GetWord(GlobalValues.FontScaleSize, $"Are you sure you want to delete {mapNames[oldSelectedItem - 1]}");
            LinkedList<Vector2> yesOption = MyFont.GetWord(GlobalValues.FontScaleSize, "Yes");
            LinkedList<Vector2> noOption = MyFont.GetWord(GlobalValues.FontScaleSize, "No");

            Vector2 areYouSurePos = new Vector2(GlobalValues.GetWordCenterPos($"Are you sure you want to delete {mapNames[oldSelectedItem - 1]}", false).X, yStart);
            Vector2 yesOptionPos = new Vector2(GlobalValues.GetWordCenterPos("Yes", false).X, yStart + (GlobalValues.FontYSpacer * 5));
            Vector2 noOptionPos = new Vector2(GlobalValues.GetWordCenterPos("No", false).X, yStart + (GlobalValues.FontYSpacer * 6));

            foreach (Vector2 v2 in areYouSure)
            {
                pb.AddVertex(v2 + areYouSurePos, Color.White);
            }

            foreach (Vector2 v2 in yesOption)
            {
                pb.AddVertex(v2 + yesOptionPos, Color.White);
            }

            foreach (Vector2 v2 in noOption)
            {
                pb.AddVertex(v2 + noOptionPos, Color.White);
            }

            List<Vector2> positions = new List<Vector2>() { yesOptionPos, noOptionPos };

            ShowSelectedIcon(positions);
        }
        #endregion

        private void UpdateFiles(string levelName)
        {
            if (File.Exists($@".\Content\{levelName}_terrain.txt"))
            {
                File.Delete($@".\Content\{levelName}_terrain.txt");
            }

            if (File.Exists($@".\Content\{levelName}_pads.txt"))
            {
                File.Delete($@".\Content\{levelName}_pads.txt");
            }

            //Loads levelnames file and adds the new level to it
            List<string> currentLevelNames = Terrain.LoadMapsNames();
            currentLevelNames.Remove(levelName);
            int count = 0;

            using (StreamWriter writer = new StreamWriter($@".\Content\LevelNames.txt"))
            {
                foreach (string item in currentLevelNames)
                {
                    if (count == 0)
                    {
                        writer.Write(item);
                    }
                    else
                    {
                        writer.Write($",{item}");
                    }
                    count++;
                }
            }

            mapNames = Terrain.LoadMapsNames();

            if (mapNames.Contains("Random"))
            {
                mapNames.Remove("Random");
            }

            if (currentSelectedItem > mapNames.Count)
            {
                currentSelectedItem--;
            }
        }
    }
}
