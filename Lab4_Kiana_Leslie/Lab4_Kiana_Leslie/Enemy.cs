using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lab4_Kiana_Leslie
{
    public class Enemy : GameObject
    {
        protected const int UpperRandomFiringRange = 1000;
        protected Random randomNumberGenerator = new Random();

        public Enemy()
        {
            maxSpeed = 250;
            dyingMillis = 1000;
            numProjectiles = 12;

            projectiles = new Projectile[numProjectiles];

            for (int c = 0; c < numProjectiles; c++)
            {
                projectiles[c] = new EnemyProjectile();
            }
            projectiles[0] = new PlayerProjectile();
            projectiles[2] = new PlayerProjectile();
            projectiles[4] = new PlayerProjectile();
            projectiles[8] = new PlayerProjectile();
            projectiles[9] = new PlayerProjectile();
        }

        internal void Initialize(Vector2 position, Rectangle gameBoundingBox, Vector2 initialDirection)
        {
            Initialize(position, gameBoundingBox);
            Move(initialDirection);

        }

        internal override void LoadContent(ContentManager content)
        {
            animationSequenceAlive = new CelAnimationSequence(content.Load<Texture2D>("dragon"), 99, 1 / 8.0f);
            //animationSequenceDying = new CelAnimationSequence(content.Load<Texture2D>("Poof"), 16, 1 / 8.0f);
            base.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (playerState)
            {
                case States.PlayerState.Alive:
                    if (BoundingBox.Left < gameBoundingBox.Left || BoundingBox.Right > gameBoundingBox.Right)
                    {
                        velocity.X *= -1;
                    }
                    animationPlayer.Update(gameTime);

                    if (randomNumberGenerator.Next(1, UpperRandomFiringRange) == 1)
                    {
                        Shoot();
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
            base.Shoot(new Vector2(0, 1));
        }
    }
}