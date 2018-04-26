using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utilities;

namespace Platformer
{
    class LifeHeart : Entity
    {
        #region Member variables
        SpriteFont myFont;
        int myLives;
        #endregion

        #region Properties
        private Vector2 WindowRelativePosition
        {
            get { return new Vector2(Position.X - Camera.TranslationX, Position.Y - Camera.TranslationY); }
        }
        #endregion

        #region Constructors
        public LifeHeart(SpriteFont aFont) : base("LifeHeart", new Vector2(10, 10))
        {
            InitializeMemberVariables(aFont);
        }
        #endregion

        #region Public methods
        public void Update(Player aPlayer)
        {
            myLives = aPlayer.Lives;
        }

        override public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Texture, WindowRelativePosition, Color);
            DrawText(aSpriteBatch);
        }
        #endregion

        #region Private methods
        private void DrawText(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawCenteredText(aSpriteBatch, myFont, 2f, myLives.ToString(),
                new Vector2(WindowRelativePosition.X + Texture.Width / 2, WindowRelativePosition.Y + Texture.Height / 2));
        }

        private void InitializeMemberVariables(SpriteFont aFont)
        {
            myFont = aFont;
        }
        #endregion
    }
}
