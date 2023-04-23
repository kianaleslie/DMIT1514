using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static Lab4_Kiana_Leslie.States;

namespace Lab4_Kiana_Leslie
{
    public class Barrier
    {
        public Texture2D texture;
        public Rectangle barrier;
        public Projectile[] projectiles;
        public Vector2 position;
        public Rectangle bBox;

        public Barrier(Vector2 position)
        {
            barrier = new Rectangle((int)position.X, (int)position.Y, texture.Width / 2, texture.Height);
        }
        internal void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("cloud-barrier");
        }
        internal void Update()
        {
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.Box.Intersects(barrier))
                {
                    projectile.projectileState = States.ProjectileState.NotFlying;
                }
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, barrier, Color.White);
        }
    }
}