using Microsoft.Xna.Framework;

namespace Platformer
{
    class Trampoline : Platform
    {
        #region Constructors
        public Trampoline(Vector2 aPosition, int aWidth, int aHeight)
            : base("Trampoline", aPosition, aWidth, aHeight)
        {
        }
        #endregion

        #region Protected methods
        override protected void PlatformTopCollisionHandle(Character aCharacter)
        {
            const int SpringForce = -15;

            base.PlatformTopCollisionHandle(aCharacter);
            aCharacter.Speed = new Vector2(0, SpringForce);
        }
        #endregion
    }
}
