using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace GameLibrary.GUIManagement
{
    public class GUIManager
    {
        private int titleFontScaleSize;
        private int regularFontScaleSize;
        private int ySpacingSize;
        private PrimitiveBatch pb;
        private Vector2 screenCenter;
        private List<Vector2> itemPositionList;

        #region Constructor
        public GUIManager(int titleFontScaleSize, int regularFontScaleSize, int ySpacingSize, PrimitiveBatch primitiveBatch, Vector2 screenCenter)
        {
            this.titleFontScaleSize = titleFontScaleSize;
            this.regularFontScaleSize = regularFontScaleSize;
            this.ySpacingSize = ySpacingSize;
            pb = primitiveBatch;
            this.screenCenter = screenCenter;
            itemPositionList = new List<Vector2>();
        }
        #endregion

        #region Show GUI
        public void ShowGUI(List<GUIItem> GUIItems)
        {
            foreach (var option in GUIItems)
            {
                LinkedList<Vector2> optionDisplay;

                //Figures out if it is a title or not then makes the display item
                if (option.IsTitle)
                {
                    optionDisplay = MyFont.GetWord(titleFontScaleSize, option.TextToDisplay);
                }
                else
                {
                    optionDisplay = MyFont.GetWord(regularFontScaleSize, option.TextToDisplay);
                }

                foreach (Vector2 v2 in optionDisplay)
                {
                    pb.AddVertex(v2 + option.Position, option.Color);
                }

                if (option.MenuItem)
                {
                    itemPositionList.Add(option.Position);
                }
            }
        }
        #endregion

        #region Selecter Icon Draw
        private LinkedList<Vector2> GetSelectedIcon()
        {
            LinkedList<Vector2> icon = new LinkedList<Vector2>();

            icon.AddLast(new Vector2(0 * regularFontScaleSize, 1 * regularFontScaleSize));
            icon.AddLast(new Vector2(3 * regularFontScaleSize, 3 * regularFontScaleSize));

            icon.AddLast(new Vector2(3 * regularFontScaleSize, 3 * regularFontScaleSize));
            icon.AddLast(new Vector2(0 * regularFontScaleSize, 5 * regularFontScaleSize));

            icon.AddLast(new Vector2(0 * regularFontScaleSize, 5 * regularFontScaleSize));
            icon.AddLast(new Vector2(0 * regularFontScaleSize, 1 * regularFontScaleSize));

            return icon;
        }

        public void ShowSelectedIcon(int currentSelectedItem)
        {
            LinkedList<Vector2> icon = GetSelectedIcon();

            foreach (Vector2 v2 in icon)
            {
                pb.AddVertex(v2 + (itemPositionList[currentSelectedItem] - new Vector2(20, 0)), Color.White);
            }
        }
        #endregion

        #region Position Math
        public Vector2 GetWordCenterPos(string word, bool isTitle)
        {
            if (isTitle)
            {
                return (screenCenter - new Vector2(word.Length * ((titleFontScaleSize + (titleFontScaleSize / 2)) * 2), ((titleFontScaleSize + (titleFontScaleSize / 2)) * 2)));
            }
            else
            {
                return (screenCenter - new Vector2(word.Length * ((regularFontScaleSize + (regularFontScaleSize / 2)) * 2), ((regularFontScaleSize + (regularFontScaleSize / 2)) * 2)));
            }
        }
        #endregion
    }
}
