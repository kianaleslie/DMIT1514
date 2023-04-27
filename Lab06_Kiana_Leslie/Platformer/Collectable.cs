using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Platformer
{
    public class Collectable
    {
        public Texture2D texture;
        public Vector2 pos;
        public Rectangle box;
        public States.CollectableState starState = States.CollectableState.Collectable;

        public Collectable(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            pos = position;
            box = new Rectangle((int)pos.X, (int)pos.Y, this.texture.Width, this.texture.Height);
        }

        internal void Initialize(Vector2 position, Rectangle bBox)
        {
            pos = position;
            box = bBox;
        }

        internal void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("collectStar");
        }

        internal void Update(GameTime gameTime)
        {
           
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            switch (starState)
            {
                case States.CollectableState.Collectable:
                    //if (!collected)
                    //{
                    spriteBatch.Draw(texture, pos, Color.White);
                    //}
                    //if (collected)
                    //{

                    //}
                    break;
                case States.CollectableState.Collected:
                    //Do nothing, star has already been collected
                    break;
                case States.CollectableState.NotCollectable:
                    //Do nothing, star cannot be collected
                    break;
            }
        }
        public void Collect()
        {
            starState = States.CollectableState.NotCollectable;
        }

        public States.CollectableState IsCollected()
        {
            return starState;
        }
    }
}