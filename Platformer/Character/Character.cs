using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
    abstract class Character : Entity
    {
        #region Member variables
        protected Rectangle mySourceRectangle;

        protected int myFrameXIndex;
        protected int myFrameYIndex;
        protected float myFrameTimeCounterMilliseconds;
        protected float myTimePerFrameMilliseconds;

        protected readonly int XPadding;
        protected readonly int YPadding;
        protected readonly int NumberOfXFrames;
        protected readonly int NumberOfYFrames;
        #endregion

        #region Properties
        public Vector2 Speed
        {
            get;
            set ;
        }

        public Vector2 RelativeSpeed
        {
            get;
            set;
        }

        public int Size
        {
            get;
            private set;
        }

        protected float Rotation
        {
            get;
            private set;
        }

        override public Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Size, Size); }
        }

        public Vector2 OldPosition
        {
            get;
            private set;
        }

        public int Lives
        {
            get;
            protected set;
        }

        protected SpriteEffects SpriteEffect
        {
            get;
            set;
        }

        public Direction Direction
        {
            get;
            protected set;
        }
        #endregion

        #region Constructors
        public Character(string aFileName, Vector2 aPosition, int aSize,
            int aNumberOfXFrames, int aNumberOfYFrames, int aXPaddingNumber, int aYPaddingNumber,
            int aTimePerFrameMilliseconds)
            : base(aFileName, aPosition)
        {
            XPadding = aXPaddingNumber;
            YPadding = aYPaddingNumber;
            NumberOfXFrames = aNumberOfXFrames;
            NumberOfYFrames = aNumberOfYFrames;

            InitializeMemberVariables(aSize, aTimePerFrameMilliseconds);
        }
        #endregion

        #region Public methods
        public bool PixelCollision(Entity aEntity)
        {
            Color[] dataA = new Color[Texture.Width * Texture.Height];
            Texture.GetData(dataA);
            Color[] dataB = new Color[aEntity.Texture.Width * aEntity.Texture.Height];
            aEntity.Texture.GetData(dataB);

            int top = Math.Max(Hitbox.Top, aEntity.Hitbox.Top);
            int bottom = Math.Min(Hitbox.Bottom, aEntity.Hitbox.Bottom);
            int left = Math.Max(Hitbox.Left, aEntity.Hitbox.Left);
            int right = Math.Min(Hitbox.Right, aEntity.Hitbox.Right);

            Rectangle overlap = new Rectangle(left, top, right - left, bottom - top);

            for (int y = overlap.Top; y < overlap.Bottom; y++)
            {
                for (int x = overlap.Left; x < overlap.Right; x++)
                {
                    Color colorA = dataA[(x - Hitbox.Left) + (y - Hitbox.Top) * Hitbox.Width];

                    Color colorB = dataB[(x - aEntity.Hitbox.Left) + (y - aEntity.Hitbox.Top) * aEntity.Hitbox.Width];

                    if (colorA.A + colorB.A > 350)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        override public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Texture, Position + new Vector2((Texture.Width / NumberOfXFrames) / 2, Texture.Height / NumberOfYFrames),
                mySourceRectangle, Color, Rotation,
                new Vector2((Texture.Width / NumberOfXFrames) / 2, Texture.Height / NumberOfYFrames), 1f, SpriteEffect, 0f);

        }

        virtual public void Update(GameTime aGameTime)
        {
            Direction oldDirection = Direction;
            Movement(aGameTime);
            UpdateAnimation(aGameTime, oldDirection);
        }

        virtual public void TakeDamage()
        {
            Lives--;
            if (Lives <= 0)
            {
                Death();
            }
        }

        virtual public void PlatformTopCollisionHandle()
        {
            PlatformYCollision();
        }

        virtual public void PlatformLeftCollisionHandle()
        {
            PlatformXCollision();
        }

        virtual public void PlatformBottomCollisionHandle()
        {
            PlatformYCollision();
        }

        virtual public void PlatformRightCollisionHandle()
        {
            PlatformXCollision();
        }
        #endregion

        #region Protected methods
        abstract protected void Death();

        virtual protected void Movement(GameTime aGameTime)
        {
            OldPosition = Position;
            Position = new Vector2(Position.X + (Speed.X + RelativeSpeed.X), Position.Y + (Speed.Y + RelativeSpeed.Y));

            RelativeSpeed = Vector2.Zero;
        }

        protected void CalculateSourceRectangle(GameTime aGameTime)
        {
            myFrameTimeCounterMilliseconds -= aGameTime.ElapsedGameTime.Milliseconds;

            if (myFrameTimeCounterMilliseconds <= 0)
            {
                myFrameTimeCounterMilliseconds = myTimePerFrameMilliseconds;
                
                ChangeFrameIndex();
            }
        }

        protected void MoveSourceRectangle(GameTime aGameTime)
        {
            CalculateSourceRectangle(aGameTime);

            mySourceRectangle = new Rectangle(
                (Size * myFrameXIndex) + (XPadding * myFrameXIndex),
                (Size * myFrameYIndex) + (YPadding * myFrameYIndex),
                Size,
                Size);
        }

        abstract protected void ChangeFrameIndex();

        virtual protected void RotateAnimation()
        {
            switch (Direction)
            {
                case Direction.Up:
                    SpriteEffect = SpriteEffects.None;
                    Rotation = (float)Math.PI / 2.0f;
                    break;
                case Direction.Left:
                    SpriteEffect = SpriteEffects.None;
                    Rotation = 0;
                    break;
                case Direction.Down:
                    SpriteEffect = SpriteEffects.None;
                    Rotation = (float)Math.PI / -2f;
                    break;
                case Direction.Right:
                    SpriteEffect = SpriteEffects.FlipHorizontally;
                    Rotation = 0;
                    break;
            }
        }
        #endregion

        #region Private method

        private void PlatformXCollision()
        {
            Position = new Vector2(OldPosition.X, Position.Y);
            Speed = new Vector2(0, Speed.Y);
        }

        private void PlatformYCollision()
        {
            Position = new Vector2(Position.X, OldPosition.Y);
            Speed = new Vector2(Speed.X, 0);
        }

        private void UpdateAnimation(GameTime aGameTime, Direction aOldDirection)
        {
            ForceMoveSourceRectangle(aOldDirection);
            MoveSourceRectangle(aGameTime);
            RotateAnimation();
        }

        private void ForceMoveSourceRectangle(Direction aOldDirection)
        {
            if (aOldDirection != Direction)
            {
                ChangeFrameIndex();
            }
        }

        private void InitializeMemberVariables(int aSize, int aTimePerFrameMilliseconds)
        {
            Size = aSize;
            myTimePerFrameMilliseconds = aTimePerFrameMilliseconds;
            myFrameTimeCounterMilliseconds = myTimePerFrameMilliseconds;
            Speed = Vector2.Zero;
            RelativeSpeed = Vector2.Zero;

            Rotation = 0;
            SpriteEffect = SpriteEffects.None;
            Direction = Direction.Left;
            OldPosition = Position;
        }
        #endregion
    }
}
