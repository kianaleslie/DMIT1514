using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Lab4_Kiana_Leslie
{
    public class Enemy : GameObject
    {
        new float timer = 0;
        float movementTimer = 0.1f;
        int movementCount = 0;
        new float speed = 0.5f;
        const int randomFire = 1000;
        Random random = new();

        public Enemy()
        {
            speed = 250;
            projectileCount = 12;

            projectiles = new Projectile[projectileCount];

            for (int index = 0; index < projectileCount; index++)
            {
                projectiles[index] = new EnemyProjectile();
            }
        }
        internal void Initialize(Vector2 position, Rectangle bBox, Vector2 initialDirection)
        {
            Initialize(position, bBox);
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