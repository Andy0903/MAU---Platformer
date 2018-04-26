using Microsoft.Xna.Framework;

namespace Platformer
{
    class JumpingEnemy : Enemy
    {
        #region Member variables
        float myJumpTimer;
        #endregion

        #region Properties
        protected override Color Color
        {
            get { return Color.Red; }
        }
        #endregion

        #region Constructors
        public JumpingEnemy(Vector2 aPosition)
            : base(aPosition)
        {
            InitializeMemberVariables();
        }
        #endregion

        #region Protected methods
        protected override void Movement(GameTime aGameTime)
        {
            TryToJump(aGameTime);
            base.Movement(aGameTime);
        }
        #endregion

        #region Private methods
        private void TryToJump(GameTime aGameTime)
        {
            myJumpTimer += aGameTime.ElapsedGameTime.Milliseconds;

            if (ReadyToJump())
            {
                Jump();
            }
        }

        private void Jump()
        {
            const float JumpForce = 8f;
            Speed = new Vector2(Speed.X, -JumpForce);
            myJumpTimer = 0;
        }

        private bool ReadyToJump()
        {
            const float JumpCooldown = 1200f;
            if (Platform != null && myJumpTimer > JumpCooldown)
            {
                return true;
            }
            return false;
        }

        private void InitializeMemberVariables()
        {
            myJumpTimer = 0;
        }
        #endregion
    }
}
