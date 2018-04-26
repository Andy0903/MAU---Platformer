using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Utilities;

namespace Platformer
{
    class HighscoreMenu : Menu
    {
        #region Constructors
        public HighscoreMenu(SpriteFont aFont)
            : base(aFont)
        {
        }

        #endregion

        #region Events
        public event EventHandler MenuSelected;
        #endregion

        #region Protected methods
        override protected void KeyPressing()
        {
            if (MenuInputManager.BackInput)
            {
                MenuSelected(this, EventArgs.Empty);
            }
        }

        override protected void DrawUnchangingText(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 2f, "Highscores (Speed run)", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.1f, Color.DeepPink);
            DrawHighscores(aSpriteBatch);
        }

        override protected void DrawXboxControllerInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press B to return to menu", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Yellow);
        }

        override protected void DrawKeyboardInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ESCAPE to return to menu", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Red);
        }
        #endregion

        #region Private methods
        private void DrawHighscores(SpriteBatch aSpriteBatch)
        {
            List<string> strings = Highscore.ReadFromFile();

            for (int i = 0; i < strings.Count; i++)
            {
                strings[i] = TimeSpan.FromMilliseconds(int.Parse(strings[i])).ToString(@"hh\:mm\:ss\.fff");
            }

            for (int i = 0; i < strings.Count; i++)
            {
                OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, strings[i],
                    WindowManager.WindowHeight / 6.4f + (i * WindowManager.WindowHeight / 19.2f),
                    Color.White);
            }
        }
        #endregion
    }
}
