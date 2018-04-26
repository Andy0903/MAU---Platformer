using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Utilities;

namespace Platformer
{
    class GameOverMenu : Menu
    {
        #region Constructors
        public GameOverMenu(SpriteFont aFont):
            base(aFont)
        {
        }
        #endregion

        #region Events

        public event EventHandler MenuSelected;

        public event EventHandler RestartSelected;

        #endregion

        #region Protected methods
        override protected void KeyPressing()
        {
            if (MenuInputManager.ForwardInput)
            {
                RestartSelected(this, EventArgs.Empty);
            }

            if (MenuInputManager.BackInput)
            {
                MenuSelected(this, EventArgs.Empty);
            }
        }

        virtual protected void DrawUnchangingText(SpriteBatch aSpriteBatch, string aTitle)
        {
            string astring = GameBoard.PlaythroughTime.ToString();

            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 2f, aTitle, WindowManager.WindowHeight - WindowManager.WindowHeight / 1.1f, Color.DeepPink);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Time spent: " + TimeSpan.FromMilliseconds(int.Parse(astring)).ToString(@"hh\:mm\:ss\.fff"),
                WindowManager.WindowHeight - WindowManager.WindowHeight / 1.15f, Color.White);
        }

        override protected void DrawUnchangingText(SpriteBatch aSpriteBatch)
        {
            DrawUnchangingText(aSpriteBatch, "Game Over");
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Time spent won't be saved if you do not win",
                WindowManager.WindowHeight - WindowManager.WindowHeight / 3f, Color.PapayaWhip);
        }

        override protected void DrawXboxControllerInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press A to restart", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.4f, Color.Yellow);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press B to return to menu", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Yellow);
        }

        override protected void DrawKeyboardInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ENTER to restart", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.4f, Color.Red);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ESCAPE to return to menu", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Red);
        }
        #endregion
    }
}
