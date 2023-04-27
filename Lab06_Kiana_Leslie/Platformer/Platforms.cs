using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class Platforms
    {
        protected Texture2D texture;
        protected string textureName;
        protected Vector2 pos;
        protected Vector2 dim;

        protected Collider colTop;
        protected Collider colRight;
        protected Collider colBottom;
        protected Collider colLeft;

        public Platforms(Vector2 position, Vector2 dimensions, string textureName)
        {
            this.textureName = textureName;
            colTop = new Collider(new Vector2(position.X + 3, position.Y), new Vector2(dimensions.X - 6, 1));
            colRight = new Collider(new Vector2(position.X + dimensions.X - 1, position.Y + 1), new Vector2(1, dimensions.Y - 2));
            colBottom = new Collider(new Vector2(position.X + 3, position.Y + dimensions.Y), new Vector2(dimensions.X - 6, 1));
            colLeft = new Collider(new Vector2(position.X, position.Y + 1), new Vector2(1, dimensions.Y - 2));
        }
        internal void LoadContent(ContentManager Content)
        {
            colTop.LoadContent(Content);
            colRight.LoadContent(Content);
            colBottom.LoadContent(Content);
            colLeft.LoadContent(Content);
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            colTop.Draw(spriteBatch);
            colRight.Draw(spriteBatch);
            colBottom.Draw(spriteBatch);
            colLeft.Draw(spriteBatch);
        }

        internal void ProcessCollisions(Player player)
        {
            colTop.IsColliding(player);
            colRight.IsColliding(player);
            colBottom.IsColliding(player);
            colLeft.IsColliding(player);
        }
    }
}