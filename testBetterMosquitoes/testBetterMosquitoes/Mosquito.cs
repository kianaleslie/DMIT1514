using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MosquitoAttack
{
    internal class Mosquito : GameBot
    {
        protected const int UpperRandomFiringRange = 1000;
        protected Random randomNumberGenerator = new Random();

        public Mosquito()
        {
            maxSpeed = 250;
            dyingMillis = 1000;
            numProjectiles = 12;

            projectiles = new Projectile[numProjectiles];

            for(int c = 0; c < numProjectiles; c++)
            {
                projectiles[c] = new FireBall();
            }
            projectiles[0] = new CannonBall();
            projectiles[2] = new CannonBall();
            projectiles[4] = new CannonBall();
            projectiles[8] = new CannonBall();
            projectiles[9] = new CannonBall();
        }

        internal void Initialize(Vector2 position, Rectangle gameBoundingBox, Vector2 initialDirection)
        {
            Initialize(position, gameBoundingBox);
            Move(initialDirection);

        }

        internal override void LoadContent(ContentManager content)
        {
            animationSequenceAlive = new CelAnimationSequence(content.Load<Texture2D>("Mosquito"), 46, 1 / 8.0f);
            animationSequenceDying = new CelAnimationSequence(content.Load<Texture2D>("Poof"), 16, 1 / 8.0f);
            base.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (state)
            {
                case State.Alive:
                    if (BoundingBox.Left < gameBoundingBox.Left || BoundingBox.Right > gameBoundingBox.Right)
                    {
                        velocity.X *= -1;
                    }
                    animationPlayer.Update(gameTime);
                    
                    if(randomNumberGenerator.Next(1, UpperRandomFiringRange) == 1)
                    {
                        Shoot();
                    }
                    break;
                case State.Dying:
                    break;
                case State.Dead:
                    break;
            }
        }
        internal void Shoot()
        {
            base.Shoot(new Vector2(0, 1));
        }
    }
}
