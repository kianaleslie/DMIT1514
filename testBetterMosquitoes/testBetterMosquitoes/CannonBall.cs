using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MosquitoAttack
{
    public class CannonBall : Projectile
    {
        protected const float Speed = 100;
        protected const string TextureName = "CannonBall";
        public CannonBall()
        {
            speed = Speed;
            textureName = TextureName;
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case State.Flying:
                    spriteBatch.Draw(texture, position, Color.White);
                    break;
                case State.NotFlying:
                    break;
            }
        }
    }
}