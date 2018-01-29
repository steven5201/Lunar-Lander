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
    class ChangeGravityScreen : GameScreen
    {
        PrimitiveBatch pb;

        public ChangeGravityScreen()
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
            pb.Begin(PrimitiveType.LineList);

            ShowMenu();
            ShowControls();

            pb.End();
        }

        private void ShowMenu()
        {
            LinkedList<Vector2> screenTitle = MyFont.GetWord(GlobalValues.TitleFontScaleSize, "Gravity");
            LinkedList<Vector2> notImplemented = MyFont.GetWord(GlobalValues.FontScaleSize, "Not implemented");

            Vector2 screenTitlePos = new Vector2(GlobalValues.GetWordCenterPos("Gravity", true).X, 100);
            Vector2 notImplementedPos = new Vector2(GlobalValues.GetWordCenterPos("Not implemented", false).X, GlobalValues.ScreenCenter.Y);

            #region Foreach
            foreach (Vector2 v2 in screenTitle)
            {
                pb.AddVertex(v2 + screenTitlePos, Color.White);
            }

            foreach (Vector2 v2 in notImplemented)
            {
                pb.AddVertex(v2 + notImplementedPos, Color.White);
            }
            #endregion
        }

        private void ShowControls()
        {
            float yStart = 500;

            LinkedList<Vector2> leave = MyFont.GetWord(GlobalValues.FontScaleSize, "Press escape to return to options screen.");

            Vector2 leavePos = new Vector2(GlobalValues.GetWordCenterPos("Press escape to return to options screen.", false).X, yStart);

            #region Foreach
            foreach (Vector2 v2 in leave)
            {
                pb.AddVertex(v2 + leavePos, Color.White);
            }
            #endregion
        }
    }
}
