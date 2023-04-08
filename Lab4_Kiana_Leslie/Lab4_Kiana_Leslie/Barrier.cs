using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Lab4_Kiana_Leslie
{
    public class Barrier
    {
        const int WIDTH = 32;
        const int HEIGHT = 24;
        const int MAXHEALTH = 3;
        private Texture2D texture;
        private Rectangle[] rectangles;
        private int health;

        public Rectangle[] Rectangles => rectangles;
        public bool IsDestroyed => health <= 0;

        public Barrier(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            rectangles = new Rectangle[MAXHEALTH * 4];

            for (int i = 0; i < rectangles.Length; i++)
            {
                int row = i / 4;
                int column = i % 4;

                rectangles[i] = new Rectangle((int)position.X + column * WIDTH, (int)position.Y + row * HEIGHT, WIDTH, HEIGHT);
            }
            health = MAXHEALTH;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Rectangle rectangle in rectangles)
            {
                spriteBatch.Draw(texture, rectangle, Color.White);
            }
        }
        public void TakeDamage()
        {
            if (health > 0)
            {
                health--;
                for (int i = 0; i < rectangles.Length; i++)
                {
                    if (i / 4 == health)
                    {
                        rectangles[i].Width = 0;
                        rectangles[i].Height = 0;
                    }
                }
            }
        }
    }
}