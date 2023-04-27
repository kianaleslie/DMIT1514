using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class Collider
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 dimensions;

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }

        public Collider(Vector2 position, Vector2 dimensions)
        {
            this.position = position;
            this.dimensions = dimensions;
        }

        internal void LoadContent(ContentManager Content, string textureString)
        {
            texture = Content.Load<Texture2D>(textureString);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BoundingBox, new Rectangle(0, 0, 1, 1), Color.White);
        }

        internal virtual bool ProcessCollisions(Player player)
        {
            return false;
        }
    }
}