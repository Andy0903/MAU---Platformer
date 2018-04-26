using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Utilities;

namespace Platformer
{
    class BackgroundLayer
    {
        #region Member variables
        Texture2D myTexture;
        List<Vector2> myPositions;
        int mySpacing;
        float mySpeed;
        #endregion

        #region Constructors
        public BackgroundLayer(string aFileName, int aSpacing, int aHeightFromGroundPosition, float aSpeed)
        {
            InitializeMemberVariables(aFileName, aSpacing, aHeightFromGroundPosition, aSpeed);
           
        }
        #endregion

        #region Public methods
        public void Update(Player aPlayer)
        {
            const int ReversePadding = 1;

            for (int i = 0; i < myPositions.Count; i++)
            {
                myPositions[i] = new Vector2(myPositions[i].X - mySpeed * (aPlayer.Position.X - aPlayer.OldPosition.X), myPositions[i].Y);

                if (myPositions[i].X <= -mySpacing)
                {
                    int j = i == 0 ? myPositions.Count - 1 : i - 1;
                    myPositions[i] = new Vector2(myPositions[j].X + mySpacing - ReversePadding, myPositions[i].Y);
                }

                if (myPositions[i].X >= mySpacing)
                {
                    int j = i == 0 ? myPositions.Count - 1 : i - 1;
                    myPositions[i] = new Vector2(myPositions[j].X - mySpacing + ReversePadding, myPositions[i].Y);

                }
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            foreach (Vector2 position in myPositions)
            {
                aSpriteBatch.Draw(myTexture, new Vector2(position.X - Camera.TranslationX, position.Y - Camera.TranslationY), Color.White);
            }
        }
        #endregion
        
        #region Private methods
        private void InitializeMemberVariables(string aFileName, int aSpacing, int aHeightFromGroundPosition, float aSpeed)
        {
            const int ExtraTextures = 2;

            myTexture = Game1.myContentManager.Load<Texture2D>(aFileName);
            mySpacing = aSpacing;
            mySpeed = aSpeed;
            myPositions = new List<Vector2>();
            for (int i = 0; i < (WindowManager.WindowWidth / mySpacing) + ExtraTextures; i++)
            {
                myPositions.Add(new Vector2(i * mySpacing, WindowManager.WindowHeight - aHeightFromGroundPosition));
            }
        }
        #endregion
    }
}
