using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Platformer
{
    public class Collectable
    {
        private Texture2D texture;
        private Vector2 pos;
        private Rectangle box;
        States.CollectableState starState;
        bool collected;
        Player player;

        public Collectable(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            pos = position;
            box = new Rectangle((int)pos.X, (int)pos.Y, this.texture.Width, this.texture.Height);
            collected = false;
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
            switch (starState)
            {
                case States.CollectableState.Collectable:
                    if (box.Intersects(player.bBox) && !collected)
                    {
                        collected = true;
                        starState = States.CollectableState.Collected;
                        Collect();
                    }
                    break;
                case States.CollectableState.Collected:
                    //Do nothing, star has already been collected
                    break;
                case States.CollectableState.NotCollectable:
                    //Do nothing, star cannot be collected
                    break;
            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            switch (starState)
            {
                case States.CollectableState.Collectable:
                    if (!collected)
                    {
                        spriteBatch.Draw(texture, pos, Color.White);
                    }
                    if (collected)
                    {

                    }
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
            collected = true;
        }

        public bool IsCollected()
        {
            return collected;
        }
    }
}