using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Lab4_Kiana_Leslie
{
    public class EnemyProjectile : Projectile
    {
        public const float SPEED = 150;
        public const string TEXTURE = "enemyProjectile";

        public EnemyProjectile()
        {
            speed = SPEED;
            textureName = TEXTURE;
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (projectileState)
            {
                case States.ProjectileState.Flying:
                    spriteBatch.Draw(texture, position, Color.White);
                    break;
                case States.ProjectileState.NotFlying:
                    break;
            }
        }
    }
}