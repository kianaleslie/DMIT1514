using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lab4_Kiana_Leslie
{
    public class Projectile
    {
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D texture;
        public Rectangle bBox;
        public float speed;
        public string textureName;
        public States.ProjectileState projectileState;

        internal Rectangle Box
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }
        internal virtual void Initialize(Rectangle bBox)
        {
            this.bBox = bBox;
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
                    if (!Box.Intersects(bBox))
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
        //Tried to do reload but again - timing issue
        //internal void Reload()
        //{
        //    //int reloadSpeed;
        //    //int bulletCount;
        //    projectileState = States.ProjectileState.NotFlying;
        //}
        internal bool IsColliding(Rectangle bBox)
        {
            bool collided = false;
            if (projectileState == States.ProjectileState.Flying && Box.Intersects(bBox))
            {
                collided = true;
                projectileState = States.ProjectileState.NotFlying;
            }
            return collided;
        }
    }
}