using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MosquitoAttack
{
    internal class GameBot
    {
        protected float maxSpeed;
        protected int dyingMillis;
        protected int numProjectiles;

        protected CelAnimationSequence animationSequenceAlive;
        protected CelAnimationSequence animationSequenceDying;
        protected CelAnimationPlayer animationPlayer;

        protected Vector2 position;
        protected Vector2 velocity; //velocity is speed * direction
        protected Rectangle gameBoundingBox;
        protected int timerDyingMillis;

        protected Projectile[] projectiles;
        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, animationSequenceAlive.CelWidth, animationSequenceAlive.CelHeight);
            }
        }

        public enum State
        {
            Alive,
            Dying,
            Dead
        }
        internal State state;

        public GameBot()
        {
            ;
        }

        internal void Initialize(Vector2 position, Rectangle gameBoundingBox)
        {
            this.position = position;
            animationPlayer = new CelAnimationPlayer();

            //we need to make sure that the animationSequence is not null before this code runs
            animationPlayer.Play(animationSequenceAlive);
            this.gameBoundingBox = gameBoundingBox;

            state = State.Alive;

            foreach (Projectile projectile in projectiles)
            {
                projectile.Initialize(gameBoundingBox);
            }
        }

        internal virtual void LoadContent(ContentManager content)
        {
            foreach (Projectile projectile in projectiles)
            {
                projectile.LoadContent(content);
            }
        }

        internal virtual void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.Alive:
                    position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case State.Dying:
                    timerDyingMillis += gameTime.ElapsedGameTime.Milliseconds;
                    animationPlayer.Update(gameTime);
                    if (timerDyingMillis >= dyingMillis)
                    {
                        state = State.Dead;
                    }
                    break;
                case State.Dead:
                    break;
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case State.Alive:
                case State.Dying:
                    animationPlayer.Draw(spriteBatch, position, SpriteEffects.None);
                    break;
                case State.Dead:
                    break;
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
        internal void Move(Vector2 direction)
        {
            velocity = direction * maxSpeed;
        }
        internal void Die()
        {
            if (state == State.Alive)
            {
                state = State.Dying;
                animationPlayer.Play(animationSequenceDying);
                timerDyingMillis = 0;
            }
        }
        internal bool Alive()
        {
            return state == State.Alive;
        }
        internal void Shoot(Vector2 direction)
        {
            int projectileIndex = 0;
            bool shot = false;
            while (state == State.Alive && projectileIndex < numProjectiles && !shot)
            {
                shot = projectiles[projectileIndex].Shoot(new Vector2(BoundingBox.Center.X, BoundingBox.Top), direction );
                projectileIndex++;
            }
        }
        internal bool ProcessProjectileCollisions(Rectangle boundingBox)
        {
            bool hit = false;
            int projectileIndex = 0;
            while (!hit && projectileIndex < projectiles.Length)
            {
                hit = projectiles[projectileIndex].ProcessCollision(boundingBox);
                projectileIndex++;
            }
            return hit;
        }
    }
}
