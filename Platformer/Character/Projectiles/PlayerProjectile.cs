using Microsoft.Xna.Framework;

namespace Platformer
{
    class PlayerProjectile : Projectile
    {
        #region Constructors
        public PlayerProjectile(Player aPlayer) 
            : base("PlayerProjectile", new Vector2(aPlayer.Position.X + aPlayer.Size / 2, aPlayer.Position.Y), 1, 3000, 16)
        {
            InitializeSpeed(aPlayer.Direction, aPlayer.Speed);
        }
        #endregion
    }
}
