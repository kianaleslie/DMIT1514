using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Lab4_Kiana_Leslie.GameObject;

namespace Lab4_Kiana_Leslie
{
    public class EnemyProjectile : Projectile
    {
        public const float Speed = 150;
        public const string TextureName = "enemyProjectile";

        public EnemyProjectile()
        {
            speed = Speed;
            textureName = TextureName;
        }
        internal override void Initialize(Rectangle gameBoundingBox)
        {
            base.Initialize(gameBoundingBox);
        }

        internal override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (projectileState)
            {
                case States.ProjectileState.Flying:
                    break;
                case States.ProjectileState.NotFlying:
                    break;
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (projectileState)
            {
                case States.ProjectileState.Flying:
                    break;
                case States.ProjectileState.NotFlying:
                    break;
            }
        }
    }
}