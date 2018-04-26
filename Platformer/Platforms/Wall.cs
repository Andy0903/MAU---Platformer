using Microsoft.Xna.Framework;

namespace Platformer
{
    class Wall : Platform
    {
        #region Constructors
        public Wall(Vector2 aPosition, int aWidth, int aHeight)
            :base("Wall",aPosition, aWidth, aHeight)
        {
        }
        #endregion
    }
}
