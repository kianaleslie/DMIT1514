using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lab4_Kiana_Leslie
{
    public class Projectile
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Texture2D texture;
        protected Rectangle gameBoundingBox;

        protected float speed;
        protected string textureName;
       
        public States.ProjectileState projectileState;
        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }
        internal virtual void Initialize(Rectangle gameBoundingBox)
        {
            this.gameBoundingBox = gameBoundingBox;
            projectileState = States.ProjectileState.NotFlying;
        }

        internal virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureName);
        }
        internal virtual void Update(GameTime gameTime)
        {
            switch (projectileState)
            {
                case States.ProjectileState.Flying:
                    position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (!BoundingBox.Intersects(gameBoundingBox))
                    {
                        projectileState = States.ProjectileState.NotFlying;
                    }
                    break;
                case States.ProjectileState.NotFlying:
                    break;
            }
        }

        internal virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        internal bool Shoot(Vector2 position, Vector2 direction)
        {
            bool shot = false;
            if (projectileState == States.ProjectileState.NotFlying)
            {
                this.position = position;
                velocity = speed * direction;
                projectileState = States.ProjectileState.Flying;
                shot = true;
            }
            return shot;
        }
        internal bool IsColliding(Rectangle boundingBox)
        {
            bool collided = false;
            if (projectileState == States.ProjectileState.Flying && BoundingBox.Intersects(boundingBox))
            {
                collided = true;
                projectileState = States.ProjectileState.NotFlying;
            }
            return collided;
        }
    }
}