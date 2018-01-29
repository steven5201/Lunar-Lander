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
    class CreditsScreen : GameScreen
    {
        PrimitiveBatch pb;

        public CreditsScreen()
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
        }

        public override void Draw(GameTime gameTime)
        {
            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Credits");
            LinkedList<Vector2> credits = MyFont.GetWord(GlobalValues.FontScaleSize, "Created by: Steven Endres");

            Vector2 screenTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Credits", true).X, 100);
            Vector2 creditsPos = new Vector2(GlobalValues.GetWordCenterPos("Created By: Steven Endres", false).X, GlobalValues.ScreenCenter.Y);

            pb.Begin(PrimitiveType.LineList);

            foreach (Vector2 v2 in screenTitle)
            {
                pb.AddVertex(v2 + screenTitlePos, Color.White);
            }

            foreach (Vector2 v2 in credits)
            {
                pb.AddVertex(v2 + creditsPos, Color.White);
            }

            ShowKeys();

            pb.End();
        }

        private void ShowKeys()
        {
            LinkedList<Vector2> display = MyFont.GetWord(GlobalValues.FontScaleSize, "Press ESC to return to Main Menu");
            Vector2 displayPos = GlobalValues.GetWordCenterPos("Press ESC to return to Main Menu", false) + new Vector2(0, 325);

            foreach (Vector2 v2 in display)
            {
                pb.AddVertex(v2 + displayPos, Color.White);
            }
        }
    }
}
