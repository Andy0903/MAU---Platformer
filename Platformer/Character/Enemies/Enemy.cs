using Microsoft.Xna.Framework;

namespace Platformer
{
    class Enemy : Character
    {
        #region Properties
        protected override Color Color
        {
            get { return Color.Yellow; }
        }

        protected Platform Platform
        {
            get;
            private set;
        }

        public bool IsDead
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        public Enemy(Vector2 aPosition)
            : base("Enemy", aPosition, 32, 3, 2, 2, 0, 200)
        {
            InitializeMemberVariables();
        }
        #endregion

        #region Public methods
        public override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);
        }

        public void Collision(Platform aPlatform)
        {
            Platform = aPlatform;
        }
        #endregion

        #region Protected methods
        protected override void ChangeFrameIndex()
        {
            switch (Direction)
            {
                case Direction.Up:
                case Direction.Down:
                    break;
                case Direction.Left:
                case Direction.Right:
                    myFrameXIndex = (myFrameXIndex + 1) % 3;
                    break;
            }
        }

        protected override void Death()
        {
            IsDead = true;
        }

        protected override void Movement(GameTime aGameTime)
        {
            TurnAround();
            UpdateSpeed();
            base.Movement(aGameTime);
        }
        #endregion

        #region Private methods

        private bool WalkingLeftAtLeftPlatformEdge()
        {
            if (Hitbox.X <= Platform.Hitbox.X && Direction == Direction.Left)
            {
                return true;
            }
            return false;
        }

        private bool WalkingRightAtRightPlatformEdge()
        {
            if (Hitbox.X + Size >= Platform.Hitbox.X + Platform.Width && Direction == Direction.Right)
            {
                return true;
            }
            return false;
        }

        private void TurnAround()
        {
            if (Platform != null)
            {
                if (WalkingLeftAtLeftPlatformEdge())
                {
                    Direction = Direction.Right;
                }
                else if (WalkingRightAtRightPlatformEdge())
                {
                    Direction = Direction.Left;
                }
            }
        }

        private void UpdateSpeed()
        {
            switch (Direction)
            {
                case Direction.Up:
                    break;
                case Direction.Left:
                    Speed = new Vector2(-1.2f, Speed.Y);
                    break;
                case Direction.Down:
                    break;
                case Direction.Right:
                    Speed = new Vector2(1.2f, Speed.Y);
                    break;
            }
        }
        
        private void InitializeMemberVariables()
        {
            Lives = 1;
            IsDead = false;
        }
        #endregion
    }
}
