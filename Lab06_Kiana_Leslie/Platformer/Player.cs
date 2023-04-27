using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Platformer
{
    public class Player
    {
        public const int JUMP = -300;
        public const int SPEED = 150;
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
        public Player(Vector2 position, Rectangle bBox)
        {
            pos = position;
            this.bBox = bBox;
            dim = new Vector2(46, 40);
            animationPlayer = new();
        }
        internal void Initalize()
        {
            playerState = States.PlayerState.Idle;
            animationPlayer.Play(idle);
        }
        internal void LoadContent(ContentManager content)
        {
            idle = new CelAnimationSequence(content.Load<Texture2D>("Owlet_Monster_Idle_4"), 30, 30, 1 / 8f);
            run = new CelAnimationSequence(content.Load<Texture2D>("Owlet_Monster_Run_6"), 30, 30,  1 / 8f);
            jump = new CelAnimationSequence(content.Load<Texture2D>("Owlet_Monster_Jump_8"), 30, 30,  1 / 8f);
        }
        internal void Update(GameTime gameTime)
        {
            animationPlayer.Update(gameTime);
            //vel.Y += Platformer.Gravity;
            pos += vel * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (Math.Abs(vel.Y) > Platformer.Gravity)
            //{
            //    playerState = States.PlayerState.Jumping;
            //    animationPlayer.Play(jump);
            //}

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
        internal void MoveVertically(Player player, float distance)
        {
            player.pos.Y -= distance;
            player.vel.Y = Player.JUMP;
        }

        internal void MoveHorizontally(Player player, float distance)
        {
            player.pos.X += distance;
            player.vel.X = -player.vel.X;
        }

        internal void Land(Player player, Rectangle colliderBoundingBox)
        {
            player.pos.Y = colliderBoundingBox.Top - player.dim.Y;
        }

        internal void StandOn(Player player, Rectangle colliderBoundingBox)
        {
            player.pos.Y = colliderBoundingBox.Top - player.dim.Y;
            player.vel.Y = 0;
        }
    }
}