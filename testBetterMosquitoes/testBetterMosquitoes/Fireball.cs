using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MosquitoAttack
{
    public class FireBall : Projectile
    {
        protected CelAnimationSequence celAnimationSequence;
        protected CelAnimationPlayer celAnimationPlayer;
        
        protected const float Speed = 150;
        protected const string TextureName = "FireBall";

        public FireBall()
        {
            speed = Speed;
            textureName = TextureName;
            celAnimationPlayer = new CelAnimationPlayer();
        }

        //"override" means "I know what I'm doing, I'm hiding the parent method"
        internal override void Initialize(Rectangle gameBoundingBox)
        {
            base.Initialize(gameBoundingBox);
            celAnimationPlayer.Play(celAnimationSequence);
        }

        internal override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            celAnimationSequence = new CelAnimationSequence(texture, 5, 1 / 8f);
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (state)
            {
                case State.Flying:
                    celAnimationPlayer.Update(gameTime);
                    break;
                case State.NotFlying:
                    break;
            }
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case State.Flying:
                    celAnimationPlayer.Draw(spriteBatch, position, SpriteEffects.None);
                    break;
                case State.NotFlying:
                    break;
            }
        }
    }
}