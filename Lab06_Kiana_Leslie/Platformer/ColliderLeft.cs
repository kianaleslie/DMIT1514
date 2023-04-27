﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class ColliderLeft : Collider
    {
        public ColliderLeft(Vector2 position, Vector2 dimensions) : base(position, dimensions)
        {
        }

        internal override bool ProcessCollisions(Player player)
        {
            bool didCollide = false;
            if (BoundingBox.Intersects(player.Box))
            {
                didCollide = true;
                if (player.Velocity.X > 0)
                {
                    player.MoveHorizontally(player, 0);
                }
            }

            return didCollide;
        }
    }
}