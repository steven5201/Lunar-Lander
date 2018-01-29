#region Usings
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
#endregion

namespace LunarLander.Screens
{
    class PlayScreen : GameScreen
    {
        #region Variables
        private Lander lander;
        private PrimitiveBatch pb;

        private bool debugMode = false;

        private Vector2 mapShiftPosition = new Vector2();

        private TimeSpan shiftTime = TimeSpan.Zero;

        private bool isFirstLoad = true;
        #endregion

        #region Initialization
        public PlayScreen()
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            StateManager.game.Window.Title = "Lunar Lander";

            pb = GlobalValues.PrimitiveBatch;

            #region Lander Calls
            lander = new Lander(StateManager.game);
            lander.Initialize();

            if (isFirstLoad)
            {
                ResetValues();
                isFirstLoad = false;
            }
            #endregion
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime, StateManager screen, GamePadState gamePadState, MouseState mouseState, KeyboardState keyboardState, InputHandler input)
        {
            #region Key Presses
            if (input.KeyboardState.WasKeyPressed(Keys.Escape) || input.WasPressed(0, InputHandler.ButtonType.A, Keys.P))
            {
                screen.Push(new PauseScreen());
            }
            #endregion

            #region Global Checks [Reset Game, Main Menu]
            if (GlobalValues.ResetGame)
            {
                ResetValues();
            }

            if (GlobalValues.GoToMainMenu)
            {
                screen.Pop();
            }
            else
            {
                lander.Update(gameTime);
            }
            #endregion

            #region Scroll Map

            if (GlobalValues.LevelLoaded == "Random")
            {
                shiftTime += gameTime.ElapsedGameTime;

                float xVelocity = LanderValues.Velocity.X;

                if (xVelocity < 0)
                {
                    xVelocity *= -1;
                }

                float speed = (MathHelper.Lerp(800, 75, (xVelocity / 5f)));

                if (speed < 0)
                {
                    speed *= -1;
                }

                if (shiftTime > TimeSpan.FromMilliseconds(speed))
                {
                    shiftTime -= TimeSpan.FromMilliseconds(speed);

                    if (LanderValues.RightBoundHit)
                    {
                        mapShiftPosition -= new Vector2(GlobalValues.ScreenBuffer, 0);
                        Terrain.NewRandomLine(false, mapShiftPosition.X);
                    }

                    if (LanderValues.LeftBoundHit)
                    {
                        mapShiftPosition += new Vector2(GlobalValues.ScreenBuffer, 0);
                        Terrain.NewRandomLine(true, mapShiftPosition.X);
                    }

                    Terrain.FixTerrainHitboxes(mapShiftPosition);
                    Terrain.FixPadHitboxes(mapShiftPosition);
                }
            }

            #endregion

            #region Win Loss Check
            if (lander.DidLose())
            {
                LoadContent();
                ResetValues();
                screen.Push(new LoseScreen());
            }

            if (lander.DidWin())
            {
                TimeSpan landerTime = lander.GetGameTime();
                LoadContent();
                lander.SetGameTime(landerTime);
                screen.Push(new WinScreen());
            }
            #endregion

            #region Debug
            //Show debug info
            if (input.KeyboardState.WasKeyPressed(Keys.OemTilde))
            {
                //Switches debug mode on or off
                debugMode = !debugMode;
            }
            #endregion
        }
        #endregion

        #region Draw
        public override void Draw(GameTime gameTime)
        {
            if (!GlobalValues.GoToMainMenu)
            {
                if (GlobalValues.LevelLoaded == "Random")
                {
                    Terrain.BuildTerrain(mapShiftPosition);
                    Terrain.BuildPads(mapShiftPosition);
                }
                else
                {
                    Terrain.BuildTerrain(Vector2.Zero);
                    Terrain.BuildPads(Vector2.Zero);
                }

                pb.Begin(PrimitiveType.LineList);

                ShowGUIWords();
                ShowGUINumbers();
                ShowControls();
                ShowTitle();

                pb.End();

                lander.Draw(gameTime);
            }
        }

        #region GUI

        #region Controls and Title
        private void ShowTitle()
        {
            LinkedList<Vector2> title = MyFont.GetWord(GlobalValues.FontScaleSize, "Lunar Lander");
            Vector2 titlePosition = new Vector2(GlobalValues.GetWordCenterPos("Lunar Lander", false).X, 10);

            foreach (Vector2 v2 in title)
            {
                pb.AddVertex(v2 + titlePosition, Color.White);
            }
        }

        private void ShowControls()
        {
            int yStart = 10 + GlobalValues.FontYSpacer;

            LinkedList<Vector2> turn = MyFont.GetWord(GlobalValues.FontScaleSize, "Arrow keys or A:D to rotate");
            LinkedList<Vector2> thrust = MyFont.GetWord(GlobalValues.FontScaleSize, "W:Space:Up Arrow to thrust");
            LinkedList<Vector2> pause = MyFont.GetWord(GlobalValues.FontScaleSize, "P:ESC to pause");

            Vector2 turnPosition = new Vector2(GlobalValues.GetWordCenterPos("Arrow keys or A:D to rotate", false).X, yStart);
            Vector2 thrustPosition = new Vector2(GlobalValues.GetWordCenterPos("W:Space:Up Arrow to thrust", false).X, yStart + GlobalValues.FontYSpacer);
            Vector2 pausePosition = new Vector2(GlobalValues.GetWordCenterPos("P:ESC to pause", false).X, yStart + (GlobalValues.FontYSpacer * 2));

            foreach (Vector2 v2 in turn)
            {
                pb.AddVertex(v2 + turnPosition, Color.White);
            }

            foreach (Vector2 v2 in thrust)
            {
                pb.AddVertex(v2 + thrustPosition, Color.White);
            }

            foreach (Vector2 v2 in pause)
            {
                pb.AddVertex(v2 + pausePosition, Color.White);
            }
        }
        #endregion

        #region GUI Words
        private void ShowGUIWords()
        {
            int xLeftSide = 10;
            int xRightSide = 1000;
            int yStart = 20;

            LinkedList<Vector2> score = MyFont.GetWord(GlobalValues.FontScaleSize, "Score");
            LinkedList<Vector2> time = MyFont.GetWord(GlobalValues.FontScaleSize, "Time");
            LinkedList<Vector2> fuel = MyFont.GetWord(GlobalValues.FontScaleSize, "Fuel");
            LinkedList<Vector2> rotation = MyFont.GetWord(GlobalValues.FontScaleSize, "Rotation");
            LinkedList<Vector2> xVelocity = MyFont.GetWord(GlobalValues.FontScaleSize, "X Velocity");
            LinkedList<Vector2> yVelocity = MyFont.GetWord(GlobalValues.FontScaleSize, "Y Velocity");

            Vector2 scorePosition = new Vector2(xLeftSide, yStart);
            Vector2 timePosition = new Vector2(xLeftSide, yStart + GlobalValues.FontYSpacer);
            Vector2 fuelPosition = new Vector2(xLeftSide, yStart + (GlobalValues.FontYSpacer * 2));
            Vector2 rotationPosition = new Vector2(xRightSide, yStart);
            Vector2 xVelocityPosition = new Vector2(xRightSide, yStart + GlobalValues.FontYSpacer);
            Vector2 yVelocityPosition = new Vector2(xRightSide, yStart + (GlobalValues.FontYSpacer * 2));

            foreach (Vector2 v2 in score)
            {
                pb.AddVertex(v2 + scorePosition, Color.White);
            }

            foreach (Vector2 v2 in time)
            {
                pb.AddVertex(v2 + timePosition, Color.White);
            }

            foreach (Vector2 v2 in fuel)
            {
                pb.AddVertex(v2 + fuelPosition, Color.White);
            }

            foreach (Vector2 v2 in xVelocity)
            {
                pb.AddVertex(v2 + xVelocityPosition, Color.White);
            }

            foreach (Vector2 v2 in rotation)
            {
                pb.AddVertex(v2 + rotationPosition, Color.White);
            }

            foreach (Vector2 v2 in yVelocity)
            {
                pb.AddVertex(v2 + yVelocityPosition, Color.White);
            }
        }
        #endregion

        #region GUI Numbers
        private void ShowGUINumbers()
        {
            int xLeftSide = 10 + ((("Score".Length + 1) * 6) * (GlobalValues.FontScaleSize));
            int xRightSide = 1025 + ((("Rotation".Length + 1) * 6) * (GlobalValues.FontScaleSize));
            int yStart = 20;

            #region Score
            string scoreDisplay = "";
            for (int i = 1; i <= (4 - LanderValues.PlayerScore.ToString().Length); i++)
            {
                scoreDisplay += "0";
            }

            scoreDisplay += LanderValues.PlayerScore.ToString();
            LinkedList<Vector2> score = MyFont.GetWord(GlobalValues.FontScaleSize, scoreDisplay);
            Vector2 scorePosition = new Vector2(xLeftSide, yStart);

            foreach (Vector2 v2 in score)
            {
                pb.AddVertex(v2 + scorePosition, Color.White);
            }
            #endregion

            #region Time
            string timeDisplay = "";

            for (int i = 1; i <= (2 - LanderValues.GameTime.Minutes.ToString().Length); i++)
            {
                timeDisplay += "0";
            }

            timeDisplay += LanderValues.GameTime.Minutes.ToString() + ":";

            for (int i = 1; i <= (2 - LanderValues.GameTime.Seconds.ToString().Length); i++)
            {
                timeDisplay += "0";
            }

            timeDisplay += LanderValues.GameTime.Seconds.ToString();
            LinkedList<Vector2> time = MyFont.GetWord(GlobalValues.FontScaleSize, timeDisplay);
            Vector2 timePosition = new Vector2(xLeftSide, yStart + GlobalValues.FontYSpacer);

            foreach (Vector2 v2 in time)
            {
                pb.AddVertex(v2 + timePosition, Color.White);
            }
            #endregion

            #region Fuel
            string fuelDisplay = "";

            for (int i = 1; i <= (3 - LanderValues.FuelRemaining.ToString().Length); i++)
            {
                fuelDisplay += "0";
            }

            fuelDisplay += LanderValues.FuelRemaining.ToString();

            LinkedList<Vector2> fuel = MyFont.GetWord(GlobalValues.FontScaleSize, fuelDisplay);
            Vector2 fuelPosition = new Vector2(xLeftSide, yStart + (GlobalValues.FontYSpacer * 2));

            foreach (Vector2 v2 in fuel)
            {
                pb.AddVertex(v2 + fuelPosition, Color.White);
            }
            #endregion

            #region Rotation
            string rotationDisplay = "";
            float degreesRotation = LanderValues.Rotation;

            if (degreesRotation < 0)
            {
                degreesRotation *= -1;
            }

            degreesRotation = (float)Math.Round(MathHelper.ToDegrees(degreesRotation));

            if (degreesRotation >= 360)
            {
                int numOf360 = (int)degreesRotation / 360;
                degreesRotation -= (float)360 * numOf360;
            }

            if (degreesRotation <= 9)
            {
                rotationDisplay = "00";
                rotationDisplay += degreesRotation.ToString();
            }
            else if (degreesRotation <= 99 && degreesRotation > 9)
            {
                rotationDisplay = "0";
                rotationDisplay += degreesRotation.ToString();
            }
            else
            {
                rotationDisplay = degreesRotation.ToString();
            }

            LinkedList<Vector2> rotation = MyFont.GetWord(GlobalValues.FontScaleSize, rotationDisplay);
            Vector2 rotationPosition = new Vector2(xRightSide, yStart);

            foreach (Vector2 v2 in rotation)
            {
                pb.AddVertex(v2 + rotationPosition, Color.White);
            }
            #endregion

            #region X Velocity
            float xVelocityNum = (float)Math.Round(LanderValues.Velocity.X * LanderValues.VelocityScale);

            if (xVelocityNum < 0)
            {
                xVelocityNum *= -1;
            }

            string xVelocityDisplay = "";

            for (int i = 1; i <= (3 - xVelocityNum.ToString().Length); i++)
            {
                xVelocityDisplay += "0";
            }

            xVelocityDisplay += xVelocityNum.ToString();

            LinkedList<Vector2> xVelocity = MyFont.GetWord(GlobalValues.FontScaleSize, xVelocityDisplay);
            Vector2 xVelocityPosition = new Vector2(xRightSide, yStart + GlobalValues.FontYSpacer);

            foreach (Vector2 v2 in xVelocity)
            {
                pb.AddVertex(v2 + xVelocityPosition, Color.White);
            }
            #endregion

            #region Y Velocity
            float yVelocityNum = (float)Math.Round(LanderValues.Velocity.Y * LanderValues.VelocityScale);

            if (yVelocityNum < 0)
            {
                yVelocityNum *= -1;
            }

            string yVelocityDisplay = "";

            for (int i = 1; i <= (3 - yVelocityNum.ToString().Length); i++)
            {
                yVelocityDisplay += "0";
            }

            yVelocityDisplay += yVelocityNum.ToString();

            LinkedList<Vector2> yVelocity = MyFont.GetWord(GlobalValues.FontScaleSize, yVelocityDisplay);
            Vector2 yVelocityPosition = new Vector2(xRightSide, yStart + (GlobalValues.FontYSpacer * 2));

            foreach (Vector2 v2 in yVelocity)
            {
                pb.AddVertex(v2 + yVelocityPosition, Color.White);
            }
            #endregion

        }
        #endregion

        #endregion

        #endregion

        #region Value Management Methods
        private void ResetValues()
        {
            lander.RandomLanderPos();
            LanderValues.PlayerScore = 0;
            LanderValues.GameTime = TimeSpan.Zero;
            lander.SetGameTime(TimeSpan.Zero);
            LanderValues.FuelRemaining = 100;
            LanderValues.Rotation = 0;
            LanderValues.Velocity = new Vector2();
            GlobalValues.ResetGame = false;
            mapShiftPosition = Vector2.Zero;
            LanderValues.LandedLines = new List<Line2D>();
        }
        #endregion
    }
}
