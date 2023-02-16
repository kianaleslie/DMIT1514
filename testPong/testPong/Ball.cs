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
    class Ball
    {
        protected const int BallWidthAndHeight = 7;
        protected const float BallInitialSpeed = 10;
        protected const int CollisionTimerIntervalMillis = 400;

        protected Texture2D texture;
        protected Vector2 dimensions, position, direction;
        protected float speed;
        protected int gameScale;
        protected Rectangle playAreaBoundingBox2D;
        protected int collisionTimerMillis;
        protected Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }

        //internal is more visible than protected, but less than public
        //internal methods/data are visible to any class in the namespace
        internal void Initialize(int gameScale, Vector2 initialPosition, Rectangle playAreaBoundingBox2D, Vector2 initialDirection)
        {
            this.gameScale = gameScale;
            speed = BallInitialSpeed * gameScale;
            direction = initialDirection;
            dimensions = new Vector2(BallWidthAndHeight * gameScale, BallWidthAndHeight * gameScale);
            position = initialPosition;
            this.playAreaBoundingBox2D = playAreaBoundingBox2D;
            collisionTimerMillis = 0;
        }

        internal void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Ball");
        }

        internal void Update(GameTime gameTime)
        {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.X <= playAreaBoundingBox2D.Left
                || position.X + dimensions.X >= playAreaBoundingBox2D.Right)
            {
                direction.X *= -1;
            }
            if (position.Y + dimensions.Y >= (playAreaBoundingBox2D.Bottom) || position.Y <= playAreaBoundingBox2D.Top)
            {
                direction.Y *= -1;
            }
            collisionTimerMillis += gameTime.ElapsedGameTime.Milliseconds;
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.Wheat, 0, Vector2.Zero, gameScale, SpriteEffects.None, 0f);
        }

        internal bool ProcessCollision(Rectangle otherBoundingBox)
        {
            bool didCollide = false;
            if (collisionTimerMillis > CollisionTimerIntervalMillis && BoundingBox.Intersects(otherBoundingBox))
            {
                didCollide = true;
                collisionTimerMillis = 0;

                Rectangle intersection = Rectangle.Intersect(BoundingBox, otherBoundingBox);
                if (intersection.Width > intersection.Height)
                {
                    //top or bottom collision
                    direction.Y *= -1;
                }
                else
                {
                    //side collision
                    direction.X *= -1;
                }
            }
            return didCollide;
        }
    }
}