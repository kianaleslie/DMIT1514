using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading.Tasks.Dataflow;

namespace Lab4_Kiana_Leslie
{
    public class Enemy : GameObject
    {
        float timer = 0;
        float movementTimer = 0.1f;
        int movementCount = 0;
        float speed = 0.5f;
        protected const int randomFire = 1000;
        protected Random random = new();

        public Enemy()
        {
            maxSpeed = 250;
            numProjectiles = 12;

            projectiles = new Projectile[numProjectiles];

            for (int index = 0; index < numProjectiles; index++)
            {
                projectiles[index] = new EnemyProjectile();
            }
        }

        internal void Initialize(Vector2 position, Rectangle gameBoundingBox, Vector2 initialDirection)
        {
            Initialize(position, gameBoundingBox);
            Move(initialDirection);

        }

        internal override void LoadContent(ContentManager content)
        {
            animation = new CelAnimationSequence(content.Load<Texture2D>("dragon"), 58, 67, 1 / 8.0f);
            base.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (playerState)
            {
                case States.PlayerState.Alive:
                    if (movementCount == 60)
                    {
                        transform.Direction = new Vector2(0, 2);
                        movementCount = -1;
                        speed = -speed;
                        timer = 0;
                    }
                    timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (timer > movementTimer && movementCount != 60)
                    {
                        transform.Direction = new Vector2(speed, 0);
                        timer = 0;
                        movementCount++;
                    }
                    Move(transform.Direction);
                    animationPlayer.Update(gameTime);

                    if (random.Next(1, randomFire) == 1)
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