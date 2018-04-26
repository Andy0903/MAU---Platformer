using Microsoft.Xna.Framework;

namespace Platformer
{
    class PowerUpBall : Character
    {
        #region Member variables
        PowerUpType myType;
        #endregion

        #region Constructors
        public PowerUpBall(PowerUpType aPowerUpBallType, PowerUpContainer aContainer, Player aPlayer)
            : base("PowerUpBalls", new Vector2(aContainer.Position.X, aContainer.Position.Y - 25), 25, 5, 4, 0, 1, 50)
        {
            InitializeMemberVariables(aPowerUpBallType);
            CalculateSpeed(aPlayer);
        }
        #endregion

        #region Properties
        public bool IsEaten
        {
            get;
            private set;
        }
        #endregion

        #region Public methods
        public void Collision(Player aPlayer)
        {
            if (CollidesWithPlayer(aPlayer))
            {
                PowerUpEffect(aPlayer);
                Death();
            }
        }
        #endregion

        #region Protected methods
        protected override void ChangeFrameIndex()
        {
            if (Speed.X > 0)
            {
                myFrameXIndex = myFrameXIndex + 1 % NumberOfXFrames;
            }
            else if (Speed.X < 0)
            {
                myFrameXIndex = myFrameXIndex - 1 % NumberOfXFrames;
            }
        }

        protected override void Death()
        {
            IsEaten = true;
        }

        #endregion

        #region Private methods

        private void PowerUpEffect(Player aPlayer)
        {
            switch (myType)
            {
                case PowerUpType.Violet:
                    aPlayer.PowerUp = PowerUpType.Violet;
                    break;
                case PowerUpType.Blue:
                    aPlayer.PowerUp = PowerUpType.Blue;
                    break;
                case PowerUpType.Orange:
                    aPlayer.PowerUp = PowerUpType.Orange;
                    break;
                case PowerUpType.Green:
                    aPlayer.PowerUp = PowerUpType.Green;
                    break;
                case PowerUpType.None:
                    aPlayer.PowerUp = PowerUpType.None;
                    break;
            }
        }

        private void CalculateSpeed(Player aPlayer)
        {
            const int UpwardForce = -12;
            const int Momentum = 4;

            if (aPlayer.Speed.X > 0)
            {
                Speed = new Vector2(Momentum, UpwardForce);
            }
            else if (aPlayer.Speed.X < 0)
            {
                Speed = new Vector2(-Momentum, UpwardForce);
            }
            else
            {
                Speed = new Vector2(0, UpwardForce);
            }
        }

        private void InitializeMemberVariables(PowerUpType aPowerUpBallType)
        {
            IsEaten = false;
            myType = aPowerUpBallType;
            SetFrameYIndex();
        }

        private void SetFrameYIndex()
        {
            switch (myType)
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
                    myFrameYIndex = 0;
                    break;
                case PowerUpType.None:
                    myFrameYIndex = 4;
                    break;
            }
        }

        private bool CollidesWithPlayer(Player aPlayer)
        {
            if (Hitbox.Intersects(aPlayer.Hitbox))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
