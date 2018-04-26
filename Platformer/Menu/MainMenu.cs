using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Utilities;

namespace Platformer
{
    class MainMenu : Menu
    {
        #region Constructors
        public MainMenu(SpriteFont aFont)
            :base(aFont)
        {
        }

        #endregion

        #region Events
        public event EventHandler StartSelected;

        public event EventHandler HighscoreSelected;

        public event EventHandler ExitSelected;

        #endregion

        #region Protected methods
        override protected void KeyPressing()
        {
            if (MenuInputManager.ForwardInput)
            {
                StartSelected(this, EventArgs.Empty);
            }
            else if (MenuInputManager.AlternativeInput)
            {
                HighscoreSelected(this, EventArgs.Empty);
            }
            else if (MenuInputManager.BackInput)
            {
                ExitSelected(this, EventArgs.Empty);
            }
        }

        override protected void DrawUnchangingText(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 2f, "Blinky Bites Back!", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.1f, Color.DeepPink);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.2f, "by Andreas Gustafsson", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.15f);
        }

        override protected void DrawXboxControllerInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press A to start", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.4f, Color.Yellow);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press Y for highscores", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.5f, Color.Yellow);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press B to exit", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Yellow);
        }

        override protected void DrawKeyboardInstructions(SpriteBatch aSpriteBatch)
        {
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ENTER to start", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.4f, Color.Red);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press SPACE for highscores", WindowManager.WindowHeight - WindowManager.WindowHeight / 1.5f, Color.Red);
            OutlinedText.DrawWidthCenteredText(aSpriteBatch, myFont, 1.5f, "Press ESCAPE to exit", WindowManager.WindowHeight - WindowManager.WindowHeight / 2f, Color.Red);
        }
        #endregion
    }
}
