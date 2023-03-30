using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4_Kiana_Leslie
{
    public class GameObject
    {
        public float maxSpeed;
        public int dyingMillis;
        public int numProjectiles;

        public CelAnimationSequence animation;
        public CelAnimationPlayer animationPlayer;

        public Vector2 position;
        public Vector2 velocity;
        public Rectangle gameBoundingBox;
        public int timerDyingMillis;
        public States.PlayerState playerState;
        public Transform transform;

        public Projectile[] projectiles;
        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, animation.CelWidth, animation.CelHeight);
            }
        }
        public GameObject()
        {
            
        }

        internal void Initialize(Vector2 position, Rectangle gameBoundingBox)
        {
            this.position = position;
            animationPlayer = new();

            animationPlayer.Play(animation);
            this.gameBoundingBox = gameBoundingBox;

            playerState = States.PlayerState.Alive;

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
            switch (playerState)
            {
                case States.PlayerState.Alive:
                    position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case States.PlayerState.Dying:
                    timerDyingMillis += gameTime.ElapsedGameTime.Milliseconds;
                    animationPlayer.Update(gameTime);
                    if (timerDyingMillis >= dyingMillis)
                    {
                        playerState = States.PlayerState.Dead;
                    }
                    break;
                case States.PlayerState.Dead:
                    break;
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            switch (playerState)
            {
                case States.PlayerState.Alive:
                case States.PlayerState.Dying:
                    animationPlayer.Draw(spriteBatch, position, SpriteEffects.None);
                    break;
                case States.PlayerState.Dead:
                    break;
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
        internal void Move(Vector2 direction)
        {
            position += direction;
            //velocity = direction * maxSpeed;
            //transform.Translate(direction);
        }
        internal void Die()
        {
            if (playerState == States.PlayerState.Alive)
            {
                playerState = States.PlayerState.Dying;
                animationPlayer.Play(animation);
                timerDyingMillis = 0;
            }
        }
        internal bool Alive()
        {
            return playerState == States.PlayerState.Alive;
        }
        internal void Shoot(Vector2 direction)
        {
            int projectileIndex = 0;
            bool shot = false;
            while (playerState == States.PlayerState.Alive && projectileIndex < numProjectiles && !shot)
            {
                shot = projectiles[projectileIndex].Shoot(new Vector2(BoundingBox.Center.X, BoundingBox.Top), direction);
                projectileIndex++;
            }
        }
        internal bool ProcessProjectileCollisions(Rectangle boundingBox)
        {
            bool hit = false;
            int projectileIndex = 0;
            while (!hit && projectileIndex < projectiles.Length)
            {
                hit = projectiles[projectileIndex].IsColliding(boundingBox);
                projectileIndex++;
            }
            return hit;
        }
        public struct Transform
        {
            public Vector2 Position;
            public Vector2 Direction;
            public Transform(Vector2 position, Vector2 direction, float rotation)
            {
                this.Position = position;
                this.Direction = direction;
            }
            public void Translate(Vector2 offset)
            {
                Position += offset;
            }
        }
    }
}