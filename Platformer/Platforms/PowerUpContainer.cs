using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
    class PowerUpContainer : Platform
    {
        #region Member variables
        bool myIsOpen;
        bool myHasSpawnedPowerUp;
        Rectangle mySourceRectangle;
        int myFrameXIndex;
        #endregion

        #region Properties
        public PowerUpType PowerUpType
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        public PowerUpContainer(Vector2 aPosition, PowerUpType aPowerUpType)
            : base("PowerUpContainer", aPosition, 32, 32)
        {
            InitializeMemberVariables(aPowerUpType);
        }
        #endregion


        #region Events
        public event EventHandler SpawnPowerUpBall;
        #endregion

        #region Public methods
        public override void Update(GameTime aGameTime)
        {
            UpdateAnimation();
        }

        override public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Texture, Position, mySourceRectangle, Color);
        }
        #endregion

        #region Protected methods
        override protected void PlatformBottomCollisionHandle(Character aCharacter)
        {
            base.PlatformBottomCollisionHandle(aCharacter);
            if (aCharacter is Player)
            {
                myIsOpen = true;
                if (myHasSpawnedPowerUp == false)
                {
                    SpawnPowerUpBall(this, EventArgs.Empty);
                    myHasSpawnedPowerUp = true;
                }
            }
        }
        #endregion

        #region Private methods
        private void InitializeMemberVariables(PowerUpType aPowerUpType)
        {
            myIsOpen = false;
            myHasSpawnedPowerUp = false;
            myFrameXIndex = 0;
            PowerUpType = aPowerUpType;
        }

        private void UpdateAnimation()
        {
            UpdateFrameIndex();
            UpdateSourceRectangle();
        }

        private void UpdateFrameIndex()
        {
            if (myIsOpen == false)
            {
                myFrameXIndex = 0;
            }
            else
            {
                myFrameXIndex = 1;
            }
        }

        private void UpdateSourceRectangle()
        {
            mySourceRectangle = new Rectangle((Width * myFrameXIndex), 0, Width, Height);
        }

        private void Reset()
        {
            myIsOpen = false;
            myHasSpawnedPowerUp = false;
        }
        #endregion
    }
}
