using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Lab4_Kiana_Leslie
{
    public class Hud
    {
        public SpriteFont font;
        public int screenHeight;
        public int playerScore;
        public int highScore;

        public Hud(SpriteFont font, int screenHeight)
        {
            this.font = font;
            this.screenHeight = screenHeight;
            playerScore = 0;
            highScore = 0;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            string scoreText = string.Format("{0}", playerScore);
            string highScoreText = string.Format("High Score: {0}", highScore);
            Vector2 scorePosition = new Vector2((DragonSiege.WINDOWWIDTH - font.MeasureString(scoreText).X) / 2, 10);
            Vector2 highScorePosition = new Vector2((DragonSiege.WINDOWWIDTH - font.MeasureString(highScoreText).X) / 2, 40);
            spriteBatch.DrawString(font, scoreText, scorePosition, Color.Black);
            spriteBatch.DrawString(font, highScoreText, highScorePosition, Color.Black);
        }
        public void PlayerScore()
        {
            playerScore++;
        }
        public void HighScore()
        {
            highScore = playerScore += 10;
        }
        public void ScoreReset()
        {
            playerScore = 0;
        }
    }
}