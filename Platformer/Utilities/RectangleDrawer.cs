using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Utilities
{
    static class RectangleDrawer
    {
        public static void DrawRectangle(SpriteBatch aSpriteBatch, Rectangle aCordinates, Color aColor, GraphicsDevice aGraphicsDevice)
        {
            var rect = new Texture2D(aGraphicsDevice, 1, 1);
            rect.SetData(new[] { aColor });
            aSpriteBatch.Draw(rect, aCordinates, aColor);
        }
    }
}
