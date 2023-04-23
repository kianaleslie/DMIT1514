using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lab4_Kiana_Leslie
{
    public class Player : GameObject
    {
        public Player()
        {
            projectileCount = 12;
            projectiles = new Projectile[projectileCount];
            for (int index = 0; index < projectileCount; index++)
            {
                projectiles[index] = new PlayerProjectile();
                if(projectiles.Length == 0)
                {
                    projectiles[index].Reload();
                }
            }
        }
        internal override void LoadContent(ContentManager content)
        {
            animation = new CelAnimationSequence(content.Load<Texture2D>("wizard"), 31, 32, 1 / 8.0f);
            base.LoadContent(content);
        }
        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (playerState)
            {
                case States.PlayerState.Alive:
                    if (Box.Left < bBox.Left)
                    {
                        position.X = bBox.Left;
                    }
                    else if (Box.Right > bBox.Right)
                    {
                        position.X = bBox.Right - Box.Width;
                    }
                    break;
                case States.PlayerState.Dying:
                    break;
                case States.PlayerState.Dead:
                    break;
            }
        }
        internal void Shoot()
        {
            base.Shoot(new Vector2(0, -1));
        }  
    }
}