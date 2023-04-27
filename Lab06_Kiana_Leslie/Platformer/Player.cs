using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platformer
{
    public class Player
    {
        public const int JUMP = -300;
        public const int SPEED = 150;
        public bool turnedLeft = false;
        public States.PlayerState playerState;
        public CelAnimationSequence idle;
        public CelAnimationSequence run;
        public CelAnimationSequence jump;
        public CelAnimationPlayer animationPlayer;
        public Vector2 pos;
        public Vector2 vel;
        public Vector2 dim;
        public Rectangle bBox;
        internal Vector2 Velocity { get { return vel; } }
        internal Rectangle Box { get { return new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y); } }
        public int Stars { get; private set; }
        public Player(Vector2 position, Rectangle bBox)
        {
            pos = position;
            this.bBox = bBox;
            dim = new Vector2(46, 40);
            animationPlayer = new();
        }
        internal void Initialize()
        {
            playerState = States.PlayerState.Idle;
            animationPlayer.Play(idle);
        }
        internal void LoadContent(ContentManager content)
        {
            idle = new CelAnimationSequence(content.Load<Texture2D>("Owlet_Monster_Idle_4"), 30, 32, 1 / 8f);
            run = new CelAnimationSequence(content.Load<Texture2D>("Owlet_Monster_Run_6"), 30, 32,  1 / 8f);
            jump = new CelAnimationSequence(content.Load<Texture2D>("Owlet_Monster_Jump_8"), 30 , 32,  1 / 8f);
        }
        internal void Update(GameTime gameTime)
        {
            animationPlayer.Update(gameTime);
            vel.Y += Platformer.GRAV;
            pos += vel * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Math.Abs(vel.Y) > Platformer.GRAV)
            {
                playerState = States.PlayerState.Jumping;
                animationPlayer.Play(jump);
            }

            switch (playerState)
            {
                case States.PlayerState.Idle:
                    break;
                case States.PlayerState.Running:
                    break;
                case States.PlayerState.Jumping:
                    break;
            }
        }
        internal void Draw(SpriteBatch SpriteBatch)
        {
            switch (playerState)
            {
                case States.PlayerState.Idle:
                    SpriteEffects ef = SpriteEffects.None;
                    animationPlayer.Draw(SpriteBatch, pos, ef);
                    break;
                case States.PlayerState.Running:
                    SpriteEffects effects = SpriteEffects.None;
                    if (turnedLeft)
                    {
                        effects = SpriteEffects.FlipHorizontally;
                    }
                    animationPlayer.Draw(SpriteBatch, pos, effects);
                    break;
                case States.PlayerState.Jumping:
                    SpriteEffects spriteEffect = SpriteEffects.None;
                    animationPlayer.Draw(SpriteBatch, pos, spriteEffect);
                    break;
            }
        }
        internal void MoveHorizontally(float dir)
        {
            float lastXDir = vel.X;
            vel.X = dir * SPEED;
            if (lastXDir > 0)
            {
                turnedLeft = false;
            }
            else if (lastXDir < 0)
            {
                turnedLeft = true;
            }
            if (playerState != States.PlayerState.Jumping)
            {
                animationPlayer.Play(run);
                playerState = States.PlayerState.Running;
            }
        }
        internal void MoveVertically(float dir)
        {
            vel.Y = dir * SPEED;
        }
        internal void Land(Rectangle land)
        {
            if (playerState == States.PlayerState.Jumping)
            {
                pos.Y = land.Top - dim.Y + 1;
                vel.Y = 0;
                playerState = States.PlayerState.Running;
            }
        }
        internal void StandOn(Rectangle standOn)
        {
            vel.Y -= Platformer.GRAV;
        }
        internal void Stop()
        {
            if (playerState == States.PlayerState.Running)
            {
                vel = Vector2.Zero;
                playerState = States.PlayerState.Idle;
                animationPlayer.Play(idle);
            }
        }
        internal void Jump()
        {
            if (playerState != States.PlayerState.Jumping)
            {
                vel.Y = JUMP;
            }
        }
        public void CollectStar(Collectable star)
        {
            Stars++;
            star.Collect();
        }
    }
}