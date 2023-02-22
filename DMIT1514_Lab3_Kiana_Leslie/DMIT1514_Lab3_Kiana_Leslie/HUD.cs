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
        public SpriteFont font;
        public int screenHeight;
        public int player1Score;
        public int player2Score;
        public int highScore;

        public HUD(SpriteFont font, int screenHeight)
        {
            this.font = font;
            this.screenHeight = screenHeight;
            this.player1Score = 0;
            this.player2Score = 0;
            highScore = 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            string scoreText = string.Format("{0} - {1}", player1Score, player2Score);
            string highScoreText = string.Format("High Score: {0}", highScore);
            Vector2 scorePosition = new Vector2((PingPong.WINDOWWIDTH - font.MeasureString(scoreText).X) / 2, 10);
            Vector2 highScorePosition = new Vector2((PingPong.WINDOWWIDTH - font.MeasureString(highScoreText).X) / 2, 40);
            spriteBatch.DrawString(font, scoreText, scorePosition, Color.Black);
            spriteBatch.DrawString(font, highScoreText, highScorePosition, Color.Black);
        }
        public void Player1Score()
        {
            player1Score++;
        }
        public void Player2Score()
        {
            player2Score++; 
        }
        public void HighScore()
        {
            highScore = player1Score + player2Score;
        }
        public void ScoreReset()
        {
            player1Score = 0;
            player2Score = 0;
        }
    }
}