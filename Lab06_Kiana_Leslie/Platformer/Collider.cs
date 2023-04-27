using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class Collider
    {
        public Texture2D texture;
        public Vector2 pos;
        public Vector2 dim;
        internal Rectangle BBox
        { get { return new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y); } }
        public Collider(Vector2 position, Vector2 dimensions)
        {
            pos = position;
            dim = dimensions;
        }
        internal void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ColliderBottom");
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BBox, new Rectangle(0, 0, 1, 1), Color.White);
        }
        internal virtual bool IsColliding(Player player)
        {
            return false;
        }
    }
}