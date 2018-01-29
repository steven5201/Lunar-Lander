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

namespace GameLibrary
{
    public abstract class GameScreen
    {
        //Force all GameScreen components to call LoadContent() automatically.
        //Why not make all screens drawable game components?
        //Component managers are typically game components and the elements managed by a component should be managed by the manager (ie. not updated() or drawn() automatically)
        public GameScreen()
        {

        }

        //Force all derived classes to implement these methods.
        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime, StateManager screen, GamePadState gamePadState, MouseState mouseState, KeyboardState keyboardState, InputHandler input);

        public abstract void Draw(GameTime gameTime);

    }
}
