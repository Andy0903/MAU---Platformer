using Microsoft.Xna.Framework;

namespace Platformer
{
    static class PhysicsManager
    {
        public static void Gravity(GameTime aGameTime, Character aCharacter)
        {
            aCharacter.Speed = new Vector2(aCharacter.Speed.X, aCharacter.Speed.Y + ((20f) / 1000) * aGameTime.ElapsedGameTime.Milliseconds);
        }
    }
}
