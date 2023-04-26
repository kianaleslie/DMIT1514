using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4_Kiana_Leslie
{
    public class GameObject
    {
        public float speed;
        public int die;
        public int projectileCount;
        public int timer;
        public CelAnimationSequence animation;
        public CelAnimationPlayer animationPlayer;
        public Vector2 position;
        public Vector2 velocity;
        public Rectangle bBox;
        public States.PlayerState playerState;
        public Transform transform;
        public Projectile[] projectiles;

        internal Rectangle Box
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, animation.CelWidth, animation.CelHeight);
            }
        }
        public GameObject()
        {
            
        }
        internal void Initialize(Vector2 position, Rectangle bBox)
        {
            this.position = position;
            animationPlayer = new();

            animationPlayer.Play(animation);
            this.bBox = bBox;

            playerState = States.PlayerState.Alive;

            foreach (Projectile projectile in projectiles)
            {
                projectile.Initialize(bBox);
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
                    timer += gameTime.ElapsedGameTime.Milliseconds;
                    animationPlayer.Update(gameTime);
                    if (timer >= die)
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
        internal virtual void Draw(SpriteBatch spriteBatch)
        {
            switch (playerState)
            {
                case States.PlayerState.Alive:
                    animationPlayer.Draw(spriteBatch, position, SpriteEffects.None);
                    break;
                case States.PlayerState.Dying:
                    break;
                case States.PlayerState.Dead:
                    break;
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }
        internal virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            switch (playerState)
            {
                case States.PlayerState.Alive:
                    animationPlayer.Draw(spriteBatch, new Vector2(position.X + 30, position.Y - 15), SpriteEffects.None);
                    break;
                case States.PlayerState.Dying:
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
        }
        internal void Die()
        {
            if (playerState == States.PlayerState.Alive)
            {
                playerState = States.PlayerState.Dying;
                animationPlayer.Play(animation);
                timer = 0;
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
            while (playerState == States.PlayerState.Alive && projectileIndex < projectileCount && !shot)
            {
                shot = projectiles[projectileIndex].Shoot(new Vector2(Box.Center.X, Box.Top), direction);
                projectileIndex++;
            }
        }
        internal bool Collisions(Rectangle boundingBox)
        {
            bool collided = false;
            int projectileIndex = 0;
            while (!collided && projectileIndex < projectiles.Length)
            {
                collided = projectiles[projectileIndex].IsColliding(boundingBox);
                projectileIndex++;
            }
            return collided;
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