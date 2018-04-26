using Microsoft.Xna.Framework;

namespace Platformer
{
    class MovingPlatform : Platform
    {
        #region Member variables
        float mySpeed;
        Direction myDirection;
        MovingPlatformType myType;

        readonly float MaxPosition;
        readonly float MinPosition;
        #endregion

        #region Constructors
        public MovingPlatform(MovingPlatformType aType, Vector2 aPosition, int aWidth, int aHeight)
            : base("MovingPlatform", aPosition, aWidth, aHeight)
        {
            const int MovementSpan = 400;

            myType = aType;

            if (myType == MovingPlatformType.Vertical)
            {
                MaxPosition = Position.Y + MovementSpan;
                MinPosition = Position.Y;
            }
            else
            {
                MaxPosition = Position.X + MovementSpan;
                MinPosition = Position.X;
            }

        }
        #endregion

        #region Public methods
        public override void Update(GameTime aGameTime)
        {
            UpdateDirection();
            UpdateSpeed(myDirection);
            Movement(aGameTime);
        }
        #endregion

        #region Protected methods
        override protected void PlatformTopCollisionHandle(Character aCharacter)
        {
            base.PlatformTopCollisionHandle(aCharacter);

            if (myType == MovingPlatformType.Vertical)
            {
                const float YPadding = 2f;
                aCharacter.Position = new Vector2(aCharacter.Position.X, Position.Y - aCharacter.Size - YPadding);
            }
            else
            {
                aCharacter.RelativeSpeed = new Vector2(mySpeed, aCharacter.RelativeSpeed.Y);
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

        #region Private methods
        private void Movement(GameTime aGameTime)
        {
            if (myType == MovingPlatformType.Vertical)
            {
                Position = new Vector2(Position.X, Position.Y + mySpeed);
            }
            else
            {
                Position = new Vector2(Position.X + mySpeed, Position.Y);
            }
        }

        private void UpdateDirection()
        {
            if (myType == MovingPlatformType.Vertical)
            {
                if (Position.Y <= MinPosition)
                {
                    myDirection = Direction.Down;
                }
                else if (Position.Y >= MaxPosition)
                {
                    myDirection = Direction.Up;
                }
            }
            else
            {
                if (Position.X <= MinPosition)
                {
                    myDirection = Direction.Left;
                }
                else if (Position.X >= MaxPosition)
                {
                    myDirection = Direction.Right;
                }
            }
        }

        private void UpdateSpeed(Direction myDirection)
        {
            const int Speed = 3;
            switch (myDirection)
            {
                case Direction.Up:
                case Direction.Right:
                    mySpeed = -Speed;
                    break;
                case Direction.Down:
                case Direction.Left:
                    mySpeed = Speed;
                    break;
            }
        }
        
        #endregion
    }
}
