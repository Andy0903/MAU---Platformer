using Microsoft.Xna.Framework;

namespace Utilities
{
    class Circle
    {
        #region Properties
        public Vector2 Center { get; private set; }
        public float Radius { get; private set; }
        #endregion

        #region Constructor
        public Circle(Vector2 aCenter, float aRadius)
        {
            Center = aCenter;
            Radius = aRadius;
        }
        #endregion

        #region Public method
        public bool Contains(Vector2 aPoint)
        {
            return ((aPoint - Center).Length() <= Radius);
        }

        public bool Intersects(Circle aCircle)
        {
            return ((aCircle.Center - Center).Length() <= (aCircle.Radius + Radius));
        }

        public bool Intersects(Rectangle aRectangle)
        {
            if (IsCornerInCircle(aRectangle) || IsCircleInRectanlge(aRectangle))
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Private methods
        private bool IsCircleInRectanlge(Rectangle aRectangle)
        {
            if (Center.X - Radius >= aRectangle.Right || Center.X + Radius <= aRectangle.Left)
            {
                return false;
            }

            if (Center.Y - Radius >= aRectangle.Bottom || Center.Y + Radius <= aRectangle.Top)
            {
                return false;
            }
            return true;
        }

        private bool IsCornerInCircle(Rectangle aRectangle)
        {
            Vector2[] aRectangleCorners = new Vector2[]
            {
                new Vector2(aRectangle.Left, aRectangle.Top),
                new Vector2(aRectangle.Right, aRectangle.Top),

                new Vector2(aRectangle.Left, aRectangle.Bottom),
                new Vector2(aRectangle.Right, aRectangle.Bottom)
            };

            foreach (Vector2 corner in aRectangleCorners)
            {
                if (Contains(corner))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
