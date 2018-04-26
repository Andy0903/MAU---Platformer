using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Platformer
{
    class Background
    {
        #region Member variables
        List<BackgroundLayer> myLayers;
        #endregion

        #region Constructors
        public Background()
        {
            InitializeMemberVariables();
        }
        #endregion

        #region Public methods
        public void Update(Player aPlayer)
        {
            foreach (BackgroundLayer layer in myLayers)
            {
                layer.Update(aPlayer);
            }
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            foreach (BackgroundLayer layer in myLayers)
            {
                layer.Draw(aSpriteBatch);
            }
        }
        #endregion

        #region Private methods
        private void InitializeMemberVariables()
        {
            myLayers = new List<BackgroundLayer>();
            myLayers.Add(new BackgroundLayer("Background", 1920, 1080, 0f));
            myLayers.Add(new BackgroundLayer("FurthestMountain", 1915, 1080, 0.1f));
            myLayers.Add(new BackgroundLayer("MiddleMountain", 1915, 1080, 0.15f));
            myLayers.Add(new BackgroundLayer("CloseMountain", 1915, 1080, 0.3f));
            myLayers.Add(new BackgroundLayer("ClosestMountain", 1915, 1080, 0.6f));
        }
        #endregion
    }
}
