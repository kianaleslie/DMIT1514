using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMIT1514_Lab3_Kiana_Leslie
{
    public class Paddle
    {
        public Texture2D texture;
        public Vector2 position;
        public int speed;
        public int screenHeight;
        public Vector2 direction;
        public bool player1;

        public Paddle(Texture2D paddleTexture, Vector2 paddlePosition, int paddleSpeed, int screenH, bool player1)
        {
            texture = paddleTexture;
            position = paddlePosition;
            speed = paddleSpeed;
            screenHeight = screenH;
            this.player1 = player1;
        }
        internal Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }
        public Rectangle BoundingBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            position += speed * Direction * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (player1)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    position.Y -= speed;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    position.Y += speed;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    position.Y -= speed;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    position.Y += speed;
                }
            }
            //Paddle doesn't leave the screen
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            else if (position.Y + texture.Height > screenHeight)
            {
                position.Y = screenHeight - texture.Height;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}