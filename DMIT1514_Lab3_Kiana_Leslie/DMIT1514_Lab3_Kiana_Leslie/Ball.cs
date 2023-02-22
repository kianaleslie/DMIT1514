using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Framework.Utilities.Deflate;

namespace DMIT1514_Lab3_Kiana_Leslie
{
    public class Ball
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        public int screenWidth;
        public int screenHeight;
        public int speed;
        public int paddle1Hits;
        public int paddle2Hits;
        public static Random random = new Random();

        public Ball(Texture2D ballTexture, Vector2 ballPosition, Vector2 ballVelocity, int screenW, int screenH)
        {
            texture = ballTexture;
            position = ballPosition;
            velocity = ballVelocity;
            screenWidth = screenW;
            screenHeight = screenH;
        }

        public Rectangle BoundingBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update()
        {
            //Update the position of the ball 
            position += velocity;

            //Bounce the ball off the top and bottom walls
            if (position.Y < 0 || position.Y + texture.Height > screenHeight)
            {
                velocity.Y = -velocity.Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        public void Reset()
        {
            // Reset position to center of screen
            position = new Vector2(PingPong.WINDOWWIDTH / 2 - texture.Width / 2, PingPong.WINDOWHEIGHT / 2 - texture.Height / 2);

            // Choose random direction
            float angle = MathHelper.ToRadians(random.Next(45, 135));
            velocity = new Vector2(speed * (float)Math.Cos(angle), speed * (float)Math.Sin(angle));
            if (random.NextDouble() > 0.5)
            {
                velocity.X *= -1;
            }
        }
        public void BallCollidesWithPaddle1()
        {
            velocity.X = -velocity.X;
            paddle1Hits++;
        }

        public void BallCollidesWithPaddle2()
        {
            velocity.X = -velocity.X;
            paddle2Hits++;
        }
    }
}