using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
    class Turret : Platform
    {
        #region Member variables
        SpriteEffects mySpriteEffect;
        const int AimWidth = 400;
        const int AimHeight = 23;
        const int TopPadding = 6;
        float myTimeSinceLastShot;
        #endregion

        #region Properties
        public Direction Direction
        {
            get;
            private set;
        }

        public Player Target
        {
            get;
            private set;
        }

        private Rectangle AimRange
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public Turret(Vector2 aPosition, Direction aDirection, Player aPlayer) : base("Turret", aPosition, 32, 48)
        {
            InitializeMemberVariables(aDirection, aPlayer);
        }
        #endregion

        #region Events
        public event EventHandler ShootProjectile;
        #endregion

        #region Public methods
        public override void Update(GameTime aGameTime)
        {
            UpdateShotCooldown(aGameTime);
            Shoot();
            
        }

        public override void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Texture, Position, null, Color, 0f, Vector2.Zero, 1, mySpriteEffect, 0);
        }
        #endregion

        #region Private methods
        private bool InRange()
        {
            if (Target.Hitbox.Intersects(AimRange))
            {
                return true;
            }
            return false;
        }

        private void Shoot()
        {
            if (InRange() == true && ShotOnCooldown() == false)
            {
                ShootProjectile(this, EventArgs.Empty);
                myTimeSinceLastShot = 0;
            }
        }
        private void UpdateShotCooldown(GameTime aGameTime)
        {
            myTimeSinceLastShot += aGameTime.ElapsedGameTime.Milliseconds;
        }

        private bool ShotOnCooldown()
        {
            const float ShootCooldown = 500f;
            if (myTimeSinceLastShot > ShootCooldown)
            {
                return false;
            }

            return true;
        }

        private void InitializeMemberVariables(Direction aDirection, Player aPlayer)
        {
            Direction = aDirection;
            myTimeSinceLastShot = 0;
            Target = aPlayer;

            if (aDirection == Direction.Left)
            {
                mySpriteEffect = SpriteEffects.None;
                AimRange = new Rectangle((int)Position.X - AimWidth, (int)Position.Y + TopPadding, AimWidth, AimHeight);
            }
            else
            {
                mySpriteEffect = SpriteEffects.FlipHorizontally;
                AimRange = new Rectangle((int)Position.X + Texture.Width, (int)Position.Y + TopPadding, AimWidth, AimHeight);
            }
        }
        #endregion
    }
}
