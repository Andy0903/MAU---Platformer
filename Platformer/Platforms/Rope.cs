using Microsoft.Xna.Framework;

namespace Platformer
{
    class Rope : Platform
    {
        #region Constructors
        public Rope(Vector2 aPosition, int aWidth, int aHeight)
            :base("Rope",aPosition, aWidth, aHeight)
        {
        }
        #endregion

        #region Protected methods
        override protected void PlatformTopCollisionHandle(Character aCharacter)
        {
            Climb(aCharacter);
        }

        override protected void PlatformLeftCollisionHandle(Character aCharacter)
        {
            Climb(aCharacter);
        }

        override protected void PlatformBottomCollisionHandle(Character aCharacter)
        {
            Climb(aCharacter);
        }

        override protected void PlatformRightCollisionHandle(Character aCharacter)
        {
            Climb(aCharacter);
        }

        #endregion

        #region Private Methods
        private void Climb(Character aCharacter)
        {
            const int speed = 3;
            aCharacter.Speed = new Vector2(0, 0);

            if (aCharacter is Player && UserInputManager.UserInputUp)
            {
                aCharacter.Speed = new Vector2(0, -speed);
            }
            else if (aCharacter is Player && UserInputManager.UserInputDown)
            {
                aCharacter.Speed = new Vector2(0, speed);
            }

        }
        #endregion
    }
}
