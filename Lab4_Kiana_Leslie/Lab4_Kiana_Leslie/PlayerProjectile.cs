using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Lab4_Kiana_Leslie.States;

namespace Lab4_Kiana_Leslie
{
    public class PlayerProjectile : Projectile
    {
        public const float SPEED = 100;
        public const string TEXTURE = "playerProjectile";
        public PlayerProjectile()
        {
            speed = SPEED;
            textureName = TEXTURE;
        }
        internal override void Draw(SpriteBatch spriteBatch)
        {
            switch (projectileState)
            {
                case States.ProjectileState.Flying:
                    spriteBatch.Draw(texture, position, Color.White);
                    break;
                case States.ProjectileState.NotFlying:
                    break;
            }
        }
    }
}