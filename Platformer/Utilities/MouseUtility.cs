using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Utilities
{
    static class MouseUtility
    {
        #region Member variables
        static MouseState myNewMouseState;
        static MouseState myOldMouseState;
        #endregion

        #region Properties
        static public bool WasLeftClicked
        {
            get { return myOldMouseState.LeftButton == ButtonState.Released && myNewMouseState.LeftButton == ButtonState.Pressed; }
        }

        static public bool WasRightClicked
        {
            get { return myOldMouseState.RightButton == ButtonState.Released && myNewMouseState.RightButton == ButtonState.Pressed; }
        }
        
        static public Point Position
        {
            get { return myNewMouseState.Position; }
        }
        #endregion

        #region Public methods
        static public void Update()
        {
            myOldMouseState = myNewMouseState;
            myNewMouseState = Mouse.GetState();
        }

        #endregion
    }
}
