using Microsoft.Xna.Framework;

namespace Platformer
{
    class Spike : Platform
    {
        #region Constructors
        public Spike(Vector2 aPosition)
            : base("Spike", aPosition, 32, 32)
        {
        }
        #endregion

        #region Protected methods
        override protected void PlatformTopCollisionHandle(Character aCharacter)
        {
            if (aCharacter is Player && (aCharacter as Player).DamagedState == true)
            {
                return;
            }

            if (aCharacter.PixelCollision(this))
            {
                base.PlatformTopCollisionHandle(aCharacter);
                Prickle(aCharacter);
            }
        }

        override protected void PlatformLeftCollisionHandle(Character aCharacter)
        {
            if (aCharacter.PixelCollision(this))
            {
                base.PlatformLeftCollisionHandle(aCharacter);
                Prickle(aCharacter);
            }
        }

        override protected void PlatformBottomCollisionHandle(Character aCharacter)
        {
            if (aCharacter.PixelCollision(this))
            {
                base.PlatformBottomCollisionHandle(aCharacter);
            }
        }

        override protected void PlatformRightCollisionHandle(Character aCharacter)
        {
            if (aCharacter.PixelCollision(this))
            {
                base.PlatformRightCollisionHandle(aCharacter);
                Prickle(aCharacter);
            }
        }
        #endregion

        #region Private method
        private void Prickle(Character aCharacter)
        {
            const int PushForce = -8;

            aCharacter.Speed = new Vector2(0, PushForce);
            aCharacter.TakeDamage();
        }
        #endregion
    }
}
