using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using GameLibrary;

namespace LunarLander
{
    class GlobalValues
    {
        #region Variables
        private static PrimitiveBatch primitiveBatch;
        private static int fontScaleSize = 3;
        private static int titleFontScaleSize = 10;
        private static int fontYSpacer = 25;
        private static bool valuesInitialized = false;
        private static Vector2 screenCenter;
        private static string levelLoaded = "1";
        private static bool goToMainMenu = false;
        private static bool resetGame = false;
        private static float screenBuffer = 50;
        #endregion

        #region Initialization
        public static void InitializeGlobalValues(PrimitiveBatch primitiveBatch, Vector2 screenCenter)
        {
            if (!ValuesInitialized)
            {
                PrimitiveBatch = primitiveBatch;
                ScreenCenter = screenCenter;
                valuesInitialized = true;
            }
        }
        #endregion

        #region Get and Set Methods
        public static PrimitiveBatch PrimitiveBatch
        {
            get => primitiveBatch;
            private set => primitiveBatch = value;
        }

        public static int FontScaleSize
        {
            get => fontScaleSize;
        }

        public static int FontYSpacer
        {
            get => fontYSpacer;
        }

        public static Vector2 ScreenCenter
        {
            get => screenCenter;
            private set => screenCenter = value;
        }

        public static bool ValuesInitialized
        {
            get => valuesInitialized;
        }

        public static int TitleFontScaleSize
        {
            get => titleFontScaleSize;
        }

        public static string LevelLoaded
        {
            get => levelLoaded;
            set => levelLoaded = value;
        }

        public static bool GoToMainMenu
        {
            get => goToMainMenu;
            set => goToMainMenu = value;
        }

        public static bool ResetGame
        {
            get => resetGame;
            set => resetGame = value;
        }

        public static float ScreenBuffer
        {
            get => screenBuffer;
        }
        #endregion

        #region Selecter Icon Draw
        public static LinkedList<Vector2> GetSelectedIcon()
        {
            LinkedList<Vector2> icon = new LinkedList<Vector2>();

            icon.AddLast(new Vector2(0 * FontScaleSize, 1 * FontScaleSize));
            icon.AddLast(new Vector2(3 * FontScaleSize, 3 * FontScaleSize));

            icon.AddLast(new Vector2(3 * FontScaleSize, 3 * FontScaleSize));
            icon.AddLast(new Vector2(0 * FontScaleSize, 5 * FontScaleSize));

            icon.AddLast(new Vector2(0 * FontScaleSize, 5 * FontScaleSize));
            icon.AddLast(new Vector2(0 * FontScaleSize, 1 * FontScaleSize));

            return icon;
        }
        #endregion

        #region Position Math
        public static Vector2 GetWordCenterPos(string word, bool isTitle)
        {
            if (isTitle)
            {
                return (ScreenCenter - new Vector2(word.Length * ((TitleFontScaleSize + (TitleFontScaleSize / 2)) * 2), ((TitleFontScaleSize + (TitleFontScaleSize / 2)) * 2)));
            }
            else
            {
                return (ScreenCenter - new Vector2(word.Length * ((FontScaleSize + (FontScaleSize / 2)) * 2), ((FontScaleSize + (FontScaleSize / 2)) * 2)));
            }
        }
        #endregion
    }
}
