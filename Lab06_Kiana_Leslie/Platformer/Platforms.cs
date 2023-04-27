using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class Platforms
    {
        public Vector2 pos;
        public Vector2 dim;
        public ColliderTop colTop;
        public ColliderBottom colBottom;
        public ColliderRight colRight;
        public ColliderLeft colLeft;
        public Texture2D platform;

        public Platforms(Vector2 position, Vector2 dimensions)
        {
            colTop = new ColliderTop(pos = new Vector2(position.X + 3, position.Y), new Vector2(dimensions.X - 6, 1));
            colBottom = new ColliderBottom(pos = new Vector2(position.X + 3, position.Y + dimensions.Y), new Vector2(dimensions.X - 6, 1));
            colRight = new ColliderRight(pos = new Vector2(position.X + dimensions.X - 1, position.Y + 1), new Vector2(1, dimensions.Y - 2));
            colLeft = new ColliderLeft(pos = new Vector2(position.X, position.Y + 1), new Vector2(1, dimensions.Y - 2));
        }
        internal void LoadContent(ContentManager Content)
        {
            colTop.LoadContent(Content);
            colBottom.LoadContent(Content);
            colRight.LoadContent(Content);
            colLeft.LoadContent(Content);
            platform = Content.Load<Texture2D>("rock0");
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            colTop.Draw(spriteBatch);
            colBottom.Draw(spriteBatch);
            colRight.Draw(spriteBatch); 
            colLeft.Draw(spriteBatch);
            spriteBatch.Draw(platform, pos, Color.White);
        }

        internal void Collisions(Player player)
        {
            colTop.IsColliding(player);
            colBottom.IsColliding(player);
            colRight.IsColliding(player);
            colLeft.IsColliding(player);
        }
    }
}