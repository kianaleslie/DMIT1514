using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Lab4_Kiana_Leslie.GameObject;
using System;
using static Lab4_Kiana_Leslie.States;

namespace Lab4_Kiana_Leslie
{
    public class Barrier
    {
        private Texture2D texture;
        private Vector2 position;
        private Transform transform;

        private float timer = 0;
        private float movementTimer = 0.1f;
        private int movementCount = 0;
        private float speed = 0.5f;
        public Barrier(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }
        public Rectangle BoundingBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        internal void Update(GameTime gameTime)
        {

            if (movementCount == 20)
            {
                transform.Direction = new Vector2(0, 0);
                movementCount = -1;
                speed = -speed;
                timer = 0;
            }
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > movementTimer && movementCount != 20)
            {
                transform.Direction = new Vector2(speed, 0);
                timer = 0;
                movementCount++;
            }
            Move(transform.Direction);

        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
        internal void Move(Vector2 direction)
        {
            position += direction;
        }
        public struct Transform
        {
            public Vector2 Position;
            public Vector2 Direction;
            public Transform(Vector2 position, Vector2 direction, float rotation)
            {
                this.Position = position;
                this.Direction = direction;
            }
            public void Translate(Vector2 offset)
            {
                Position += offset;
            }
        }
    }
}