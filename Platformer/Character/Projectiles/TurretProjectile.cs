using Microsoft.Xna.Framework;

namespace Platformer
{
    class TurretProjectile : Projectile
    {
        #region Constructors
        public TurretProjectile(Turret aTurret)
            : base("TurretProjectile", new Vector2(aTurret.Position.X + aTurret.Texture.Width / 2, aTurret.Position.Y + 12), 1, 2000, 32)
        {
            InitializeSpeed(aTurret.Direction, Vector2.Zero);
        }
        #endregion
    }
}
