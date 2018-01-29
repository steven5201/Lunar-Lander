using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StateManager : DrawableGameComponent
    {
        //The stack which contains the screens.
        private Stack<GameScreen> screens;

        //Hold the commonly used objects in a central location that is available to all of the screens in the State Manager.
        private static ContentManager m_content;
        private static GraphicsDevice m_graphicsDevice;
        private static InputHandler m_input;
        private static Game m_game;

        public static Game game
        {
            get { return m_game; }
            set { m_game = value; }
        }

        //All gamestates can access the Game Content Manager from the static context
        public static ContentManager content
        {
            get { return m_content; }
        }

        //All gamestates can access the Graphics Device from the static context
        public static GraphicsDevice graphicsDevice
        {
            get { return m_graphicsDevice; }
        }


        public StateManager(Game game) : base(game)
        {
            screens = new Stack<GameScreen>();
            m_content = game.Content;
            m_graphicsDevice = game.GraphicsDevice;
            m_input = new InputHandler(game);
            m_game = game;
        }

        //Insert a screen into the stack so that it becomes the screen that will be displayed
        public void Push(GameScreen screen)
        {
            screens.Push(screen);
        }

        //Remove the currently displayed screen
        public GameScreen Pop()
        {
            return screens.Pop();
        }

        protected override void LoadContent()
        {
            ContentManager content = game.Content;
        }

        //Return the top screen in the stack
        public GameScreen Top()
        {
            return screens.Peek();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            MouseState mouseState = Mouse.GetState();
            KeyboardState keyState = Keyboard.GetState();
            m_input.Update(gameTime);

            //Update the current gamestate.
            Top().Update(gameTime, this, gamePadState, mouseState, keyState, m_input);
        }

        public override void Draw(GameTime gameTime)
        {
            //Draw the current gamestate.
            Top().Draw(gameTime);
        }
    }
}
