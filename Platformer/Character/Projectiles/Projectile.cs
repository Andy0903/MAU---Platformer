using Microsoft.Xna.Framework;

namespace Platformer
{
    abstract class Projectile : Character
    {
        #region Member variables
        float myLifeTimeTimer;

        protected const float TravelSpeed = 6;
        readonly float LifeTime;
        #endregion

        #region Constructors
        public Projectile(string aFileName, Vector2 aPosition, int aLifeNumber, float aLifeTime, int aSize)
            : base(aFileName, aPosition,
                 aSize, 2, 2, 0, 0, 100)
        {
            LifeTime = aLifeTime;
            InitializeMemberVariables(aLifeNumber);
        }
        #endregion

        #region Properties
        public bool HasCollided
        {
            get;
            private set;
        }
        #endregion

        #region Public methods
        public override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);
            UpdateLifeTime(aGameTime);
            UpdateDirection();
        }
        #endregion

        #region Protected methods
        protected void InitializeSpeed(Direction aDirection, Vector2 aSpeed)
        {
            if (aDirection == Direction.Left)
            {
                Speed = new Vector2(aSpeed.X - TravelSpeed, 0);
            }
            else if (aDirection == Direction.Right)
            {
                Speed = new Vector2(aSpeed.X + TravelSpeed, 0);
            }
            else if (aDirection == Direction.Up)
            {
                Speed = new Vector2(0, -TravelSpeed);
            }
            else if (aDirection == Direction.Down)
            {
                Speed = new Vector2(0, TravelSpeed);
            }
        }

        protected override void ChangeFrameIndex()
        {
            myFrameXIndex = (myFrameXIndex + 1) % 2;
        }

        protected override void Death()
        {
            HasCollided = true;
        }

        #endregion

        #region Private methods

        private void UpdateLifeTime(GameTime aGameTime)
        {
            myLifeTimeTimer += aGameTime.ElapsedGameTime.Milliseconds;
            if (myLifeTimeTimer >= LifeTime)
            {
                HasCollided = true;
            }
        }

        private void UpdateDirection()
        {
            if (Speed.X > 0)
            {
                Direction = Direction.Right;
            }
            else if (Speed.X < 0)
            {
                Direction = Direction.Left;
            }
            else if (Speed.Y > 0)
            {
                Direction = Direction.Down;
            }
            else if (Speed.Y < 0)
            {
                Direction = Direction.Up;
            }
        }

        private void InitializeMemberVariables(int aLifeNumber)
        {
            Lives = aLifeNumber;
            HasCollided = false;
            myLifeTimeTimer = 0;
        }
        #endregion
    }
}