using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MosquitoAttack
{
    public abstract class Projectile
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Texture2D texture;
        protected Rectangle gameBoundingBox;

        protected float speed;
        protected string textureName;
        protected enum State
        {
            Flying,
            NotFlying
        }
        protected State state;
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
            state = State.NotFlying;
        }

        internal virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureName);
        }
        //"virtual" means "my children may override this method, but it's not required"
        internal virtual void Update(GameTime gameTime)
        {
            switch (state)
            {
                case State.Flying:
                    position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (!BoundingBox.Intersects(gameBoundingBox))
                    {
                        state = State.NotFlying;
                    }
                    break;
                case State.NotFlying:
                    break;
            }
        }

        //"abstract" forces the child class to define a method with this signature
        internal abstract void Draw(SpriteBatch spriteBatch);

        internal bool Shoot(Vector2 position, Vector2 direction)
        {
            bool shot = false;
            if (state == State.NotFlying)
            {
                this.position = position;
                velocity = speed * direction;
                state = State.Flying;
                shot = true;
            }
            return shot;
        }
        internal bool ProcessCollision(Rectangle boundingBox)
        {
            bool hit = false;
            if (state == State.Flying && BoundingBox.Intersects(boundingBox))
            {
                hit = true;
                state = State.NotFlying;
            }
            return hit;
        }
    }
}
