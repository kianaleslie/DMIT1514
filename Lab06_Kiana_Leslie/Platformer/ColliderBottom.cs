using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class ColliderBottom : Collider
    {
        public ColliderBottom(Vector2 position, Vector2 dimensions) : base(position, dimensions)
        {

        }

        internal override bool IsColliding(Player player)
        {
            bool didCollide = false;
            if (BBox.Intersects(player.Box))
            {
                didCollide = true;
                if (player.Velocity.Y < 0)
                {
                    player.MoveVertically(0);
                }
            }
            return didCollide;
        }
    }
}