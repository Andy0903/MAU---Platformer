using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    abstract class Platform : Entity
    {
        #region Properties
        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        override public Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Width, Height); }
        }
        #endregion

        #region Constructors
        protected Platform(string aFileName, Vector2 aPosition, int aWidth, int aHeight)
            : base(aFileName, aPosition)
        {
            InitializeMemberVariables(aWidth, aHeight);
        }
        #endregion

        #region Public methods
        virtual public void Update(GameTime aGameTime)
        {
        }

        override public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Texture, Hitbox, Color);
        }

        virtual public void Collision(Character aCharacter)
        {
            const int CollisionPadding = 3;
            if (aCharacter.OldPosition.Y + aCharacter.Size <= Hitbox.Y + CollisionPadding)
            {
                PlatformTopCollisionHandle(aCharacter);
            }
            else if (aCharacter.OldPosition.X + aCharacter.Size <= Hitbox.X)
            {
                PlatformLeftCollisionHandle(aCharacter);
            }
            else if (aCharacter.OldPosition.Y >= Hitbox.Y + Height - CollisionPadding)
            {
                PlatformBottomCollisionHandle(aCharacter);
            }
            else if (aCharacter.OldPosition.X <= Hitbox.X + Width)
            {
                PlatformRightCollisionHandle(aCharacter);
            }
        }
        #endregion

        #region Protected methods
        virtual protected void PlatformTopCollisionHandle(Character aCharacter)
        {
            aCharacter.PlatformTopCollisionHandle();
        }

        virtual protected void PlatformLeftCollisionHandle(Character aCharacter)
        {
            aCharacter.PlatformLeftCollisionHandle();
        }

        virtual protected void PlatformBottomCollisionHandle(Character aCharacter)
        {
            aCharacter.PlatformBottomCollisionHandle();
        }

        virtual protected void PlatformRightCollisionHandle(Character aCharacter)
        {
            aCharacter.PlatformRightCollisionHandle();
        }
        #endregion

        #region Private methods
        private void InitializeMemberVariables(int aWidth, int aHeight)
        {
            Width = aWidth;
            Height = aHeight;
        }
        #endregion
    }
}
