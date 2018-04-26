using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Utilities;

namespace Platformer
{
    static class MenuInputManager
    {
        #region Properties
        static public bool ForwardInput
        {
            get { return (KeyboardUtility.WasClicked(Keys.Enter) || XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.A)); }
        }

        static public bool BackInput
        {
            get { return (KeyboardUtility.WasClicked(Keys.Escape) || XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.B)); }
        }

        static public bool AlternativeInput
        {
            get { return (KeyboardUtility.WasClicked(Keys.Space) || XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.Y)); }
        }
        #endregion
    }
}
