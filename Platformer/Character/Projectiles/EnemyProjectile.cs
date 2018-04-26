using Microsoft.Xna.Framework;

namespace Platformer
{
    class EnemyProjectile : Projectile
    {
        #region Constructors
        public EnemyProjectile(ShootingEnemy aShootingEnemy)
            : base("EnemyProjectile",
                  new Vector2(aShootingEnemy.Position.X + aShootingEnemy.Size / 2, aShootingEnemy.Position.Y + aShootingEnemy.Size / 3),
                  1, 500, 16)
        {
            InitializeSpeed(aShootingEnemy.Direction, aShootingEnemy.Speed);
        }
        #endregion
    }
}
