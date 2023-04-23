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
        public int lives;

        public Hud(SpriteFont font, int screenHeight)
        {
            this.font = font;
            this.screenHeight = screenHeight;
            playerScore = 0;
            highScore = 0;
            lives = 3;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            string scoreText = string.Format("Score: {0}", playerScore);
            string highScoreText = string.Format("High Score: {0}", highScore);
            string livesText = string.Format("Lives: {0}", lives);
            Vector2 scorePosition = new Vector2(20, 1);
            Vector2 highScorePosition = new Vector2(190, 1);
            Vector2 livesPos = new Vector2(420, 1);
            spriteBatch.DrawString(font, scoreText, scorePosition, Color.Black);
            spriteBatch.DrawString(font, highScoreText, highScorePosition, Color.Black);
            spriteBatch.DrawString(font, livesText, livesPos, Color.Black);
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
        public int DecreaseLives()
        {
            return lives -= 1;
        }
        public int GetLives()
        {
            return lives;
        }
    }
}