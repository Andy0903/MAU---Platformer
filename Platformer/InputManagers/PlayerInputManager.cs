using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    static class UserInputManager
    {
        #region Properties

        static public bool UserInputActionClick
        {
            get
            {
                return (Utilities.KeyboardUtility.WasClicked(Keys.Space)
                    || Utilities.XboxControllerUtility.WasClicked(PlayerIndex.One, Buttons.A));
            }
        }

        static public bool UserInputUpClick
        {
            get
            {
                return (Utilities.KeyboardUtility.WasClicked(Keys.W)
                     || Utilities.KeyboardUtility.WasClicked(Keys.Up)
                     || Utilities.XboxControllerUtility.WasLeftThumbStickYMovedUp(PlayerIndex.One));
            }
        }

        static public bool UserInputUp
        {
            get
            {
                return (Utilities.KeyboardUtility.IsHeldDown(Keys.W)
                     || Utilities.KeyboardUtility.IsHeldDown(Keys.Up)
                     || Utilities.XboxControllerUtility.GetLeftThumbStickY(PlayerIndex.One) >= Utilities.XboxControllerUtility.ThumbStickSensitivity);
            }
        }

        static public bool UserInputLeft
        {
            get
            {
                return (Utilities.KeyboardUtility.IsHeldDown(Keys.A)
                     || Utilities.KeyboardUtility.IsHeldDown(Keys.Left)
                     || Utilities.XboxControllerUtility.GetLeftThumbStickX(PlayerIndex.One) <= -Utilities.XboxControllerUtility.ThumbStickSensitivity);
            }
        }

        static public bool UserInputDown
        {
            get
            {
                return (Utilities.KeyboardUtility.IsHeldDown(Keys.S)
                    || Utilities.KeyboardUtility.IsHeldDown(Keys.Down)
                    || Utilities.XboxControllerUtility.GetLeftThumbStickY(PlayerIndex.One) <= -Utilities.XboxControllerUtility.ThumbStickSensitivity);
            }
        }

        static public bool UserInputRight
        {
            get
            {
                return (Utilities.KeyboardUtility.IsHeldDown(Keys.D)
                    || Utilities.KeyboardUtility.IsHeldDown(Keys.Right)
                    || Utilities.XboxControllerUtility.GetLeftThumbStickX(PlayerIndex.One) >= Utilities.XboxControllerUtility.ThumbStickSensitivity);
            }
        }
        #endregion
    }
}
