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

        internal override bool IsColliding(Player player)
        {
            bool didCollide = false;
            if (BBox.Intersects(player.Box))
            {
                didCollide = true;
                player.Land(BBox);
                player.StandOn(BBox);
            }
            return didCollide;
        }
    }
}