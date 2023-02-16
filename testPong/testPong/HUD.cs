
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace testPong
{
    class HUD
    {

        protected SpriteFont arial;
        protected Texture2D texture;

        protected Vector2 position;
        protected string message;

        protected int gameScale;

        public int Height { get => texture.Height * gameScale; }

        internal void Initialize(int scale, Vector2 position)
        {
            gameScale = scale;
            this.position = position;
        }
        internal void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("HUDBackground");
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Wheat, 0, Vector2.Zero, gameScale, SpriteEffects.None, 0f);
        }

    }
}
