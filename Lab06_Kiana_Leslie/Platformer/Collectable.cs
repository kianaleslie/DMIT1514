using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Platformer
{
    public class Collectable : GameObject
    {
        public Collectable(Game game) : base(game)
        {

        }
        private bool isCollected;
        Player player;
        public Collectable(Game game, Texture2D texture, Vector2 startPosition) : base(game)
        {
            //transform = new Transform(startPosition);
            this.texture = texture;
            rectangle = new Rectangle((int)startPosition.X, (int)startPosition.Y, texture.Width, texture.Height);
            isCollected = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isCollected)
            {
                // Check if the coin has been collected by the player.
                // You could replace this with more complex collision detection logic.
                Rectangle playerRect = player.Box; // Get the player's rectangle.
                if (rectangle.Intersects(playerRect))
                {
                    isCollected = true;
                    // Add the coin to the player's inventory or increase the player's score.
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!isCollected)
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                spriteBatch.Draw(texture, transform._position, texture.Bounds, Color.White, transform._rotation, texture.Bounds.Center.ToVector2(), transform._scale, SpriteEffects.None, 0);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}