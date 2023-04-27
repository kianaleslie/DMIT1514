using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class ColliderTop : Collider
    {
        public ColliderTop(Vector2 position, Vector2 dimensions) : base(position, dimensions)
        {

        }

        internal override bool ProcessCollisions(Player player)
        {
            bool didCollide = false;
            if (BoundingBox.Intersects(player.Box))
            {
                didCollide = true;
                player.Land(player, BoundingBox);
                player.StandOn(player, BoundingBox);
            }

            return didCollide;
        }
    }
}