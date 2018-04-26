using Microsoft.Xna.Framework;

namespace Platformer
{
    class Block : Platform
    {
        #region Constructors
        public Block(Vector2 aPosition, int aWidth, int aHeight)
            :base("Block",aPosition, aWidth, aHeight)
        {
        }
        #endregion

        #region Protected methods
        override protected void PlatformLeftCollisionHandle(Character aCharacter)
        {
        }

        override protected void PlatformBottomCollisionHandle(Character aCharacter)
        {
        }

        override protected void PlatformRightCollisionHandle(Character aCharacter)
        {
        }

        #endregion
    }
}
