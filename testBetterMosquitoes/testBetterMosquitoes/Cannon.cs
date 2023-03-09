using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MosquitoAttack
{
    internal class Cannon : GameBot
    {
        public Cannon()
        {
            maxSpeed = 275;
            dyingMillis = 1000;
            numProjectiles = 10;

            projectiles = new Projectile[numProjectiles];
            for (int c = 0; c < numProjectiles; c++)
            {
                projectiles[c] = new CannonBall();
            }
            projectiles[0] = new FireBall();
            projectiles[2] = new FireBall();
            projectiles[4] = new FireBall();
            projectiles[8] = new FireBall();
            projectiles[9] = new FireBall();
        }

        internal override void LoadContent(ContentManager content)
        {
            animationSequenceAlive = new CelAnimationSequence(content.Load<Texture2D>("Cannon"), 40, 1 / 8.0f);
            animationSequenceDying = new CelAnimationSequence(content.Load<Texture2D>("Poof"), 16, 1 / 8.0f);
            base.LoadContent(content);
        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (state)
            {

                case State.Alive:
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
                case State.Dying:
                    break;
                case State.Dead:
                    break;
            }
        }
        internal void Shoot()
        {
            base.Shoot(new Vector2(0, -1));
        }
    }
}
