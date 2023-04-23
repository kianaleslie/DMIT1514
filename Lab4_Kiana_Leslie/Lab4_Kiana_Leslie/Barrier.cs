using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static Lab4_Kiana_Leslie.States;

namespace Lab4_Kiana_Leslie
{
    public class Barrier
    {
        public Texture2D texture;
        public Vector2 position;
        public int screenW;
        public int screenH;
        public Rectangle barrier;
        public Projectile[] playerProjectiles;
        public Projectile[] enemyProjectiles;
        
        public Rectangle bBox;
        public int playerProjectileCount;
        public int enemyProjectileCount;

        public Barrier(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            screenH = DragonSiege.WINDOWHEIGHT;
            screenW = DragonSiege.WINDOWWIDTH;
            //barrier = new Rectangle((int)position.X, (int)position.Y, 200 / 2, 100);
            //playerProjectileCount = 12;
            //playerProjectiles = new Projectile[playerProjectileCount];
            //enemyProjectiles = new Projectile[enemyProjectileCount];
            //for (int index = 0; index < playerProjectileCount; index++)
            //{
            //    playerProjectiles[index] = new PlayerProjectile();
            //}
            //for (int index = 0; index < enemyProjectileCount; index++)
            //{
            //    enemyProjectiles[index] = new EnemyProjectile();
            //}
        }
        public Rectangle BoundingBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        //internal void LoadContent(ContentManager content)
        //{
        //    texture = content.Load<Texture2D>("cloud-barriers");
        //}
        internal void Update()
        {
            position = new Vector2(100, 200);
            //foreach (Projectile projectile in playerProjectiles)
            //{
            //    if (projectile.Box.Intersects(barrier))
            //    {
            //        projectile.projectileState = States.ProjectileState.NotFlying;
            //    }
            //}
            //foreach (Projectile projectile in enemyProjectiles)
            //{
            //    if (projectile.Box.Intersects(barrier))
            //    {
            //        projectile.projectileState = States.ProjectileState.NotFlying;
            //    }
            //}
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, barrier, Color.White);
        }
    }
}