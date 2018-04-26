using Microsoft.Xna.Framework;

namespace Platformer
{
    class TrapDoor : Platform
    {
        #region Constructors
        public TrapDoor(Vector2 aPosition, int aWidth, int aHeight)
            : base("TrapDoor", aPosition, aWidth, aHeight)
        {
        }
        #endregion

        #region Protected methods
        override protected void PlatformTopCollisionHandle(Character aCharacter)
        {
            if (aCharacter is Player && UserInputManager.UserInputDown)
            {
            }
            else
            {
                base.PlatformTopCollisionHandle(aCharacter);
            }
        }
        #endregion
    }
}
