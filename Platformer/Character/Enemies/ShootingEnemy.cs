using Microsoft.Xna.Framework;
using System;

namespace Platformer
{
    class ShootingEnemy : Enemy
    {
        #region Member variables
        float myTimeSinceLastShot;
        #endregion

        #region Properties
        protected override Color Color
        {
            get { return Color.Blue; }
        }
        #endregion

        #region Constructors
        public ShootingEnemy(Vector2 aPosition)
            : base(aPosition)
        {
            InitializeMemberVariables();
        }
        #endregion

        #region Events
        public event EventHandler ShootProjectile;
        #endregion

        #region Public methods
        public override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);
            UpdateShotCooldown(aGameTime);
            Shoot();
        }
        #endregion

        #region Protected methods
        protected override void Movement(GameTime aGameTime)
        {
            base.Movement(aGameTime);
        }
        #endregion

        #region Private methods
        private void Shoot()
        {
            if (ShotOnCooldown() == false)
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
            const float ShootCooldown = 2000f;
            if (myTimeSinceLastShot > ShootCooldown)
            {
                return false;
            }

            return true;
        }

        private void InitializeMemberVariables()
        {
            myTimeSinceLastShot = 0;
        }
        #endregion
    }
}
