using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Lab4_Kiana_Leslie.GameObject;

namespace Lab4_Kiana_Leslie
{
    public class EnemyProjectile : Projectile
    {
        protected CelAnimationSequence celAnimationSequence;
        protected CelAnimationPlayer celAnimationPlayer;

        protected const float Speed = 150;
        protected const string TextureName = "enemyProjectile";

        public EnemyProjectile()
        {
            speed = Speed;
            textureName = TextureName;
            celAnimationPlayer = new CelAnimationPlayer();
        }
        internal override void Initialize(Rectangle gameBoundingBox)
        {
            base.Initialize(gameBoundingBox);
            celAnimationPlayer.Play(celAnimationSequence);
        }

        internal override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            celAnimationSequence = new CelAnimationSequence(texture, 5, 5, 1 / 8f);
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (projectileState)
            {
                case States.ProjectileState.Flying:
                    celAnimationPlayer.Update(gameTime);
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
                    celAnimationPlayer.Draw(spriteBatch, position, SpriteEffects.None);
                    break;
                case States.ProjectileState.NotFlying:
                    break;
            }
        }
    }
}