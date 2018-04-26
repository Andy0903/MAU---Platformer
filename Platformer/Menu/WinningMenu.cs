using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    class WinningMenu : GameOverMenu
    {
        #region Constructors
        public WinningMenu(SpriteFont aFont): 
        base(aFont)
        {
        }
        #endregion

        #region protected methods
        override protected void DrawUnchangingText(SpriteBatch aSpriteBatch)
        {
            DrawUnchangingText(aSpriteBatch, "Congratulations! You won!");
        }
        #endregion
    }
}
