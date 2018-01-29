using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameLibrary.GUIManagement
{
    public class GUIItem
    {
        private bool isTitle;
        private string textToDisplay;
        private bool menuItem;
        private Vector2 position;
        private Color color;

        public GUIItem(bool isTitle, string textToDisplay, bool menuItem, Vector2 position, Color color)
        {
            this.isTitle = isTitle;
            this.textToDisplay = textToDisplay;
            this.menuItem = menuItem;
            this.position = position;
            this.color = color;
        }

        public bool IsTitle { get => isTitle; }
        public string TextToDisplay { get => textToDisplay; }
        public bool MenuItem { get => menuItem; }
        public Vector2 Position { get => position; }
        public Color Color { get => color; }
    }
}
