using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Glitch
{
    public static class GlobalVar
    {
        public static bool mouseReleased;
        public static int battleTurn;
        public static int clientBoundsWidth;
        public static int clientBoundsHeight;
        public static ContentManager content;
        public static bool showScreen;

        #region Game States
        public enum GameState
        {
            Menu,
            Credits,
            Plot,
            HowTo,
            Playing,
            Battle,
            GameOver,
            Win
        }

        public static GameState state;
        #endregion
    }
}
