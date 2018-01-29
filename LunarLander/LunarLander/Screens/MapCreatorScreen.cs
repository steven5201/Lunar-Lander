#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameLibrary;
using GameLibrary.GUIManagement;
#endregion

namespace LunarLander.Screens
{
    class MapCreatorScreen : GameScreen
    {
        #region Variables

        private PrimitiveBatch pb;
        private string levelName = "";

        #region Line Draw
        private Point lineStartLocation;
        #endregion

        #region Input States
        private MouseState newMouseState;
        private MouseState oldMouseState;
        private KeyboardState newKeyboardState;
        private KeyboardState oldKeyboardState;
        #endregion

        #region List Data
        private LinkedList<Vector2> mapTerrain;
        private LinkedList<Vector2> mapPads;
        private List<Tuple<int, int, int, int>> correctFormatTerrain;
        private List<Tuple<int, int, int, int>> correctFormatPads;
        #endregion

        #region Bools
        private bool makingTerrain = true;
        private bool makingPads = false;
        private bool isSaved = false;
        private bool isNamed = false;
        #endregion

        #endregion

        #region Constructor and Load
        public MapCreatorScreen()
        {
            this.pb = GlobalValues.PrimitiveBatch;
            lineStartLocation = new Point(0, 720);
            oldMouseState = new MouseState();
            newMouseState = new MouseState();
            newKeyboardState = new KeyboardState();
            oldKeyboardState = new KeyboardState();
            mapTerrain = new LinkedList<Vector2>();
            mapPads = new LinkedList<Vector2>();
            correctFormatTerrain = new List<Tuple<int, int, int, int>>();
            correctFormatPads = new List<Tuple<int, int, int, int>>();

            LoadContent();
        }

        public override void LoadContent()
        {
            StateManager.game.Window.Title = "Lunar Lander";
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime, StateManager screen, GamePadState gamePadState, MouseState mouseState, KeyboardState keyboardState, InputHandler input)
        {
            if (input.KeyboardState.WasKeyPressed(Keys.Escape))
            {
                screen.Pop();
            }

            #region After Named
            if (isNamed)
            {
                #region Mouse Click
                newMouseState = Mouse.GetState();

                if (newMouseState != oldMouseState)
                {
                    if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        isSaved = false;

                        if (makingTerrain)
                        {
                            mapTerrain.AddLast(new Vector2(lineStartLocation.X, lineStartLocation.Y));
                            mapTerrain.AddLast(new Vector2(newMouseState.X, newMouseState.Y));

                            correctFormatTerrain.Add(new Tuple<int, int, int, int>(lineStartLocation.X, lineStartLocation.Y, newMouseState.X, newMouseState.Y));

                            lineStartLocation = new Point(newMouseState.X, newMouseState.Y);
                        }

                        if (makingPads)
                        {
                            mapPads.AddLast(new Vector2(lineStartLocation.X, lineStartLocation.Y));
                            mapPads.AddLast(new Vector2(newMouseState.X, lineStartLocation.Y));

                            correctFormatPads.Add(new Tuple<int, int, int, int>(lineStartLocation.X, lineStartLocation.Y, newMouseState.X, lineStartLocation.Y));

                            lineStartLocation = new Point(newMouseState.X, lineStartLocation.Y);
                        }

                       
                    }
                }

                oldMouseState = newMouseState;
                #endregion

                #region Keyboard Presses
                newKeyboardState = Keyboard.GetState();

                if (newKeyboardState != oldKeyboardState)
                {
                    if (newKeyboardState.IsKeyDown(Keys.Z) && (newKeyboardState.IsKeyDown(Keys.LeftControl) || newKeyboardState.IsKeyDown(Keys.RightControl)))
                    {
                        //Undo last thing

                    }

                    if (newKeyboardState.IsKeyDown(Keys.P))
                    {
                        makingPads = !makingPads;
                        makingTerrain = !makingTerrain;
                    }

                    if (input.KeyboardState.WasKeyPressed(Keys.S))
                    {
                        SaveMap();
                    }
                }

                oldKeyboardState = newKeyboardState;

                #endregion
            }//End isNamed IF
            #endregion

            #region Before Named
            else
            {
                if (input.KeyboardState.WasKeyPressed(Keys.Enter))
                {
                    if (levelName != "")
                    {
                        isNamed = true;
                    }
                    else
                    {
                        levelName = "DefaultName";
                    }
                }
                else
                {
                    Type(input);
                }
            }
            #endregion
        }
        #endregion

        #region Draw [Draw, Display]
        public override void Draw(GameTime gameTime)
        {
            pb.Begin(PrimitiveType.LineList);

            #region After Named
            if (isNamed)
            {
                Display();

                #region Show Old Lines
                if (mapTerrain.Count != 0)
                {
                    foreach (Vector2 v2 in mapTerrain)
                    {
                        pb.AddVertex(v2, Color.White);
                    }
                }

                if (mapPads.Count != 0)
                {
                    foreach (Vector2 v2 in mapPads)
                    {
                        pb.AddVertex(v2, Color.Green);
                    }
                }
                #endregion

                #region Display Line
                if (makingTerrain)
                {
                    pb.AddVertex(new Vector2(lineStartLocation.X, lineStartLocation.Y), Color.White);
                    pb.AddVertex(new Vector2(newMouseState.X, newMouseState.Y), Color.White);
                }

                if (makingPads)
                {
                    pb.AddVertex(new Vector2(lineStartLocation.X, lineStartLocation.Y), Color.Green);
                    pb.AddVertex(new Vector2(newMouseState.X, lineStartLocation.Y), Color.Green);
                }
                #endregion
            }
            #endregion

            #region Before Named
            else
            {
                LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Map Creator");
                Vector2 screenTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Map Creator", true).X, 100);

                LinkedList<Vector2> mapTitle = MyFont.GetWord(GlobalValues.FontScaleSize, "Map Title");
                Vector2 mapTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Map Title", false).X, GlobalValues.GetWordCenterPos(levelName, false).Y - 75);

                LinkedList<Vector2> userWord = MyFont.GetWord(GlobalValues.FontScaleSize, levelName);
                Vector2 userWordPos = GlobalValues.GetWordCenterPos(levelName, false);

                LinkedList<Vector2> continueWord = MyFont.GetWord(GlobalValues.FontScaleSize, "Press Enter to Continue");
                Vector2 continueWordPos = new Vector2(GlobalValues.GetWordCenterPos("Press Enter to Continue", false).X, GlobalValues.GetWordCenterPos(levelName, false).Y + 75);

                #region Foreach
                foreach (Vector2 v2 in screenTitle)
                {
                    pb.AddVertex(v2 + screenTitlePos, Color.White);
                }

                foreach (Vector2 v2 in mapTitle)
                {
                    pb.AddVertex(v2 + mapTitlePos, Color.White);
                }

                foreach (Vector2 v2 in userWord)
                {
                    pb.AddVertex(v2 + userWordPos, Color.White);
                }

                foreach (Vector2 v2 in continueWord)
                {
                    pb.AddVertex(v2 + continueWordPos, Color.White);
                }
                #endregion
            }
            #endregion

            pb.End();
        }

        private void Display()
        {
            float xLeft = 25;
            int yStart = 25;

            #region Linked Lists
            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.FontScaleSize, "Map Creator");
            LinkedList<Vector2> togglePads = MyFont.GetWord(GlobalValues.FontScaleSize, "Toggle pads: P");
            LinkedList<Vector2> saveMap = MyFont.GetWord(GlobalValues.FontScaleSize, "Save Map: S");
            LinkedList<Vector2> exit = MyFont.GetWord(GlobalValues.FontScaleSize, "Exit: ESC");
            LinkedList<Vector2> arePadsToggled = MyFont.GetWord(GlobalValues.FontScaleSize, $"Pads Toggled: {makingPads}");
            LinkedList<Vector2> isMapSaved = MyFont.GetWord(GlobalValues.FontScaleSize, $"Map Saved: {isSaved}");
            LinkedList<Vector2> currentX = MyFont.GetWord(GlobalValues.FontScaleSize, $"Current X Pos: {newMouseState.X}");
            LinkedList<Vector2> currentY = MyFont.GetWord(GlobalValues.FontScaleSize, $"Current Y Pos: {newMouseState.Y}");
            #endregion

            #region Positions
            Vector2 screenTitlePos = new Vector2(xLeft, yStart);
            Vector2 togglePadsPos = new Vector2(xLeft, yStart + GlobalValues.FontYSpacer);
            Vector2 saveMapPos = new Vector2(xLeft, yStart + (GlobalValues.FontYSpacer * 2));
            Vector2 exitPos = new Vector2(xLeft, yStart + (GlobalValues.FontYSpacer * 3));
            Vector2 arePadsToggledPos = new Vector2(xLeft, yStart + (GlobalValues.FontYSpacer * 4));
            Vector2 isMapSavedPos = new Vector2(xLeft, yStart + (GlobalValues.FontYSpacer * 5));
            Vector2 currentXPos = new Vector2(xLeft, yStart + (GlobalValues.FontYSpacer * 6));
            Vector2 currentYPos = new Vector2(xLeft, yStart + (GlobalValues.FontYSpacer * 7));
            #endregion

            #region Foreach
            foreach (Vector2 v2 in screenTitle)
            {
                pb.AddVertex(v2 + screenTitlePos, Color.White);
            }

            foreach (Vector2 v2 in togglePads)
            {
                pb.AddVertex(v2 + togglePadsPos, Color.White);
            }

            foreach (Vector2 v2 in saveMap)
            {
                pb.AddVertex(v2 + saveMapPos, Color.White);
            }

            foreach (Vector2 v2 in exit)
            {
                pb.AddVertex(v2 + exitPos, Color.White);
            }

            foreach (Vector2 v2 in arePadsToggled)
            {
                pb.AddVertex(v2 + arePadsToggledPos, Color.White);
            }

            foreach (Vector2 v2 in isMapSaved)
            {
                pb.AddVertex(v2 + isMapSavedPos, Color.White);
            }

            foreach (Vector2 v2 in currentX)
            {
                pb.AddVertex(v2 + currentXPos, Color.White);
            }

            foreach (Vector2 v2 in currentY)
            {
                pb.AddVertex(v2 + currentYPos, Color.White);
            }
            #endregion
        }
        #endregion

        #region Save
        private void SaveMap()
        {
            using (StreamWriter writer = new StreamWriter($@".\Content\{levelName}_terrain.txt"))
            {
                foreach (Tuple<int, int, int, int> item in correctFormatTerrain)
                {
                    writer.WriteLine($"{item.Item1},{item.Item2},{item.Item3},{item.Item4}");
                }
            }

            using (StreamWriter writer = new StreamWriter($@".\Content\{levelName}_pads.txt"))
            {
                foreach (Tuple<int, int, int, int> item in correctFormatPads)
                {
                    writer.WriteLine($"{item.Item1},{item.Item2},{item.Item3},{item.Item4}");
                }
            }

            //Loads levelnames file and adds the new level to it
            List<string> currentLevelNames = Terrain.LoadMapsNames();

            if (!currentLevelNames.Contains(levelName))
            {
                currentLevelNames.Add(levelName);
            }

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

            isSaved = true;
        }
        #endregion

        #region Typing Events
        private void Type(InputHandler input)
        {
            newKeyboardState = Keyboard.GetState();
            bool isGoodInput = false;
            bool isNumKey = false;
            Keys[] goodInput = new Keys[] { Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.J, Keys.I, Keys.K, Keys.L, Keys.M, Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad0, Keys.NumPad9 };
            Keys[] numKeys = new Keys[] { Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad0, Keys.NumPad9 };

            if (newKeyboardState != oldKeyboardState)
            {
                Keys[] keys = newKeyboardState.GetPressedKeys();

                if (keys.Count() != 0)
                {
                    Keys key = keys[0];

                    if (key == Keys.Back)
                    {
                        if (levelName != "")
                        {
                            levelName = levelName.Remove(levelName.Length - 1, 1);
                        }
                    }

                    foreach (Keys goodKey in goodInput)
                    {
                        if (key == goodKey)
                        {
                            isGoodInput = true;
                        }
                    }

                    if (isGoodInput)
                    {
                        foreach (Keys numKey in numKeys)
                        {
                            if (key == numKey)
                            {
                                isNumKey = true;
                            }
                        }

                        if (isNumKey)
                        {
                            levelName += key.ToString().Remove(0, 6);
                        }
                        else
                        {
                            levelName += key.ToString();
                        }
                    }

                    isGoodInput = false;
                    isNumKey = false;
                }
            }

            oldKeyboardState = newKeyboardState;
        }
        #endregion
    }
}
