﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4_Kiana_Leslie
{
    public class Player : GameObject
    {
        public Player()
        {
            maxSpeed = 275;
            dyingMillis = 1000;
            numProjectiles = 10;

            projectiles = new Projectile[numProjectiles];
            for (int c = 0; c < numProjectiles; c++)
            {
                projectiles[c] = new PlayerProjectile();
            }
            projectiles[0] = new EnemyProjectile();
            projectiles[2] = new EnemyProjectile();
            projectiles[4] = new EnemyProjectile();
            projectiles[8] = new EnemyProjectile();
            projectiles[9] = new EnemyProjectile();
        }

        internal override void LoadContent(ContentManager content)
        {
            animationSequenceAlive = new CelAnimationSequence(content.Load<Texture2D>("wizard"), 20, 1 / 8.0f);
            //animationSequenceDying = new CelAnimationSequence(content.Load<Texture2D>("Poof"), 16, 1 / 8.0f);
            base.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (playerState)
            {

                case States.PlayerState.Alive:
                    if (BoundingBox.Left < gameBoundingBox.Left)
                    {
                        position.X = gameBoundingBox.Left;
                    }
                    else if (BoundingBox.Right > gameBoundingBox.Right)
                    {
                        position.X = gameBoundingBox.Right - BoundingBox.Width;
                    }
                    else if (!velocity.Equals(Vector2.Zero))
                    {
                        animationPlayer.Update(gameTime);
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