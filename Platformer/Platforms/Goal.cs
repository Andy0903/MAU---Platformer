using Microsoft.Xna.Framework;
using System;

namespace Platformer
{
    class Goal : Platform
    {
        #region Constructors
        public Goal(Vector2 aPosition, int aWidth, int aHeight)
            : base("Goal", aPosition, aWidth, aHeight)
        {
        }
        #endregion

        #region MyRegion
        public event EventHandler GoalReached;
        #endregion

        #region Protected methods
        override protected void PlatformTopCollisionHandle(Character aCharacter)
        {
            base.PlatformTopCollisionHandle(aCharacter);
            if (aCharacter is Player)
            {
                GoalReached(this, EventArgs.Empty);
            }
        }

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
