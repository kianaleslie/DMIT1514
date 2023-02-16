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
    class Paddle
    {
        protected const int Speed = 200;
        protected const int Width = 2;
        protected const int Height = 18;

        protected Texture2D texture;
        protected Vector2 dimensions, position;
        protected float speed;
        protected int gameScale;
        protected Rectangle playAreaBoundingBox2D;

        protected Vector2 direction;
        internal Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }

        internal void Initialize(int gameScale, Vector2 initialPosition, Rectangle playAreaBoundingBox2D)
        {
            dimensions = new Vector2(Width * gameScale, Height * gameScale);
            position = initialPosition;
            this.gameScale = gameScale;
            this.playAreaBoundingBox2D = playAreaBoundingBox2D;
            speed = Speed * gameScale;
        }

        internal void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Paddle");
        }

        internal void Update(GameTime gameTime)
        {
            position += speed * Direction * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((position.Y + dimensions.Y) >= playAreaBoundingBox2D.Bottom)
            {
                position.Y = playAreaBoundingBox2D.Bottom - dimensions.Y;
            }
            else if (position.Y <= playAreaBoundingBox2D.Top)
            {
                position.Y = playAreaBoundingBox2D.Top;
            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Wheat, 0, Vector2.Zero, gameScale, SpriteEffects.None, 0f);
        }
    }
}