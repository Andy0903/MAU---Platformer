using Microsoft.Xna.Framework;
using System;

namespace Platformer
{
    class Player : Character
    {
        #region Member variables
        int myJumpStock;
        int myMaxJumpStock;
        float myTimeSinceLastShot;
        float myMovementSpeed;
        float myDamagedStateTimer;
        
        const int DefaultMovementSpeed = 5;
        const int DefaultJumpStock = 2;
        const int DefaultLifeNumber = 3;
        const int DefaultJumpForce = 8;
        const int CollisionPadding = 5;
        #endregion

        #region Properties
        public bool DamagedState
        {
            get;
            private set;
        }

        public PowerUpType PowerUp
        {
            get;
            set;
        }

        #endregion

        #region Constructors
        public Player(Vector2 aPosition)
            : base("Player", aPosition, 32, 8, 4, 2, 1, 200)
        {
            InitializeMemberVariables();
        }
        #endregion

        #region Events
        public event EventHandler GameOver;

        public event EventHandler ShootProjectile;

        public event EventHandler TookDamage;
        #endregion

        #region Public methods
        override public void TakeDamage()
        {
            if (PowerUp == PowerUpType.None)
            {
                Lives--;
                TookDamage(this, EventArgs.Empty);
                if (Lives <= 0)
                {
                    Death();
                }
            }
            else
            {
                DamagedState = true;
                PowerUp = PowerUpType.None;
            }
        }

        public void Collision(Enemy aEnemy)
        {
            if (DamagedState == false)
            {
                if (CollidesWithEnemyTop(aEnemy))
                {
                    Position = new Vector2(Position.X, OldPosition.Y);

                    Speed = new Vector2(Speed.X, -DefaultJumpForce);
                    aEnemy.TakeDamage();
                }
                else if (CollidesWithEnemyLeftSide(aEnemy))
                {
                    Position = new Vector2(OldPosition.X, Position.Y);
                    Speed = new Vector2(Speed.X, -DefaultJumpForce);
                    TakeDamage();
                }
                else if (CollidesWithEnemyBottom(aEnemy))
                {
                    Position = new Vector2(Position.X, OldPosition.Y);
                    Speed = new Vector2(Speed.X, DefaultJumpForce);
                    TakeDamage();
                }
                else if (CollidesWithEnemyRightSide(aEnemy))
                {
                    Position = new Vector2(OldPosition.X, Position.Y);
                    Speed = new Vector2(Speed.X, -DefaultJumpForce);
                    TakeDamage();
                }
            }
        }

        public override void PlatformTopCollisionHandle()
        {
            myJumpStock = myMaxJumpStock;
            base.PlatformTopCollisionHandle();
        }

        public override void Update(GameTime aGameTime)
        {
            base.Update(aGameTime);
            UpdatePowerUps();
            UpdateShotCooldown(aGameTime);
            UpdateDamagedState(aGameTime);
        }
        #endregion

        #region Protected methods
        protected override void Death()
        {
            GameOver(this, EventArgs.Empty);
        }

        protected override void ChangeFrameIndex()
        {
            switch (Direction)
            {
                case Direction.Up:
                    myFrameXIndex = myFrameXIndex != 4 ? 4 : 5;
                    break;
                case Direction.Left:
                    myFrameXIndex = myFrameXIndex != 2 ? 2 : 3;
                    break;
                case Direction.Down:
                    myFrameXIndex = myFrameXIndex != 6 ? 6 : 7;
                    break;
                case Direction.Right:
                    myFrameXIndex = myFrameXIndex != 0 ? 0 : 1;
                    break;
            }
            UpdateSkin();
        }

        override protected void RotateAnimation()
        {
        }

        override protected void Movement(GameTime aGameTime)
        {
            Speed = new Vector2(0, Speed.Y);
            if (UserInputManager.UserInputUpClick)
            {
                UpMovement();
            }
            if (UserInputManager.UserInputLeft)
            {
                LeftMovement();
            }
            if (UserInputManager.UserInputDown)
            {
                DownMovement();
            }
            if (UserInputManager.UserInputRight)
            {
                RightMovement();
            }
            base.Movement(aGameTime);
        }

        #endregion

        #region Private method
        private void UpdateDamagedState(GameTime aGameTime)
        {
            const float DamagedStateDuration = 500f;

            if (DamagedState == true)
            {
                myDamagedStateTimer += aGameTime.ElapsedGameTime.Milliseconds;
                if (myDamagedStateTimer >= DamagedStateDuration)
                {
                    DamagedState = false;
                    myDamagedStateTimer = 0f;
                }
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

        private void UpdateSkin()
        {
            switch (PowerUp)
            {
                case PowerUpType.Violet:
                    myFrameYIndex = 1;
                    break;
                case PowerUpType.Blue:
                    myFrameYIndex = 2;
                    break;
                case PowerUpType.Orange:
                    myFrameYIndex = 3;
                    break;
                case PowerUpType.Green:
                    break;
                case PowerUpType.None:
                    myFrameYIndex = 0;
                    break;
            }
        }

        private void UpMovement()
        {
            Direction = Direction.Up;
            if (myJumpStock > 0)
            {
                Speed = new Vector2(Speed.X, -DefaultJumpForce);
                myJumpStock--;
            }
        }

        private void LeftMovement()
        {
            Direction = Direction.Left;
            Speed = new Vector2(-myMovementSpeed, Speed.Y);
        }

        private void DownMovement()
        {
            Direction = Direction.Down;
        }

        private void RightMovement()
        {
            Direction = Direction.Right;
            Speed = new Vector2(myMovementSpeed, Speed.Y);
        }

        private void VioletEffect()
        {
            myMaxJumpStock = DefaultJumpStock * 2;
            myMovementSpeed = DefaultMovementSpeed;
        }

        private void BlueEffect()
        {
            myMaxJumpStock = DefaultJumpStock;
            myMovementSpeed = DefaultMovementSpeed;

            if (UserInputManager.UserInputActionClick && ShotOnCooldown() == false)
            {
                ShootProjectile(this, EventArgs.Empty);
                myTimeSinceLastShot = 0;
            }
        }

        private void OrangeEffect()
        {
            myMaxJumpStock = DefaultJumpStock;
            myMovementSpeed = DefaultMovementSpeed * 2;
        }

        private void NoEffect()
        {
            myMaxJumpStock = DefaultJumpStock;
            myMovementSpeed = DefaultMovementSpeed;
        }

        private void GreenEffect()
        {
            Lives++;
            switch (myFrameYIndex)
            {
                case 0:
                    PowerUp = PowerUpType.None;
                    break;
                case 1:
                    PowerUp = PowerUpType.Violet;
                    break;
                case 2:
                    PowerUp = PowerUpType.Blue;
                    break;
                case 3:
                    PowerUp = PowerUpType.Orange;
                    break;
            }
        }

        private void UpdatePowerUps()
        {
            switch (PowerUp)
            {
                case PowerUpType.Violet:
                    VioletEffect();
                    break;
                case PowerUpType.Blue:
                    BlueEffect();
                    break;
                case PowerUpType.Orange:
                    OrangeEffect();
                    break;
                case PowerUpType.Green:
                    GreenEffect();
                    break;
                case PowerUpType.None:
                    NoEffect();
                    break;
            }
        }

        private bool CollidesWithEnemyLeftSide(Enemy aEnemy)
        {
            if (OldPosition.X + Size <= aEnemy.Hitbox.X)
            {
                return true;
            }
            return false;
        }

        private bool CollidesWithEnemyTop(Enemy aEnemy)
        {
            if (OldPosition.Y + Size <= aEnemy.Hitbox.Y + CollisionPadding)
            {
                return true;
            }
            return false;
        }

        private bool CollidesWithEnemyBottom(Enemy aEnemy)
        {
            if (OldPosition.Y >= aEnemy.Hitbox.Y + aEnemy.Size - CollisionPadding)
            {
                return true;
            }
            return false;
        }

        private bool CollidesWithEnemyRightSide(Enemy aEnemy)
        {
            if (OldPosition.X <= aEnemy.Hitbox.X + aEnemy.Size)
            {
                return true;
            }
            return false;
        }

        private void InitializeMemberVariables()
        {
            Lives = DefaultLifeNumber;
            myMaxJumpStock = DefaultJumpStock;
            myJumpStock = myMaxJumpStock;
            myMovementSpeed = DefaultMovementSpeed;
            PowerUp = PowerUpType.None;
            DamagedState = false;
        }
        #endregion
    }
}
