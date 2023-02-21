using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMIT1514_Lab3_Kiana_Leslie
{
    public class HUD
    {
        private SpriteFont font;
        private int screenHeight;
        private int player1Score;
        private int player2Score;

        public HUD(SpriteFont font, int screenHeight)
        {
            this.font = font;
            this.screenHeight = screenHeight;
            this.player1Score = 0;
            this.player2Score = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string scoreText = string.Format("{0} - {1}", player1Score, player2Score);
            Vector2 position = new Vector2((PingPong.WINDOWWIDTH - font.MeasureString(scoreText).X) / 2, 10);
            spriteBatch.DrawString(font, scoreText, position, Color.White);
        }

        public void Player1Score()
        {
            player1Score++;
        }

        public void Player2Score()
        {
            player2Score++;
        }
    }
}