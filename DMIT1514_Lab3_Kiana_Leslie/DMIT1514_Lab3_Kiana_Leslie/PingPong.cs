using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks.Sources;

namespace DMIT1514_Lab3_Kiana_Leslie
{
    public class PingPong : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Random random = new Random();

        public const int WINDOWWIDTH = 1050;
        public const int WINDOWHEIGHT = 650;

        public Texture2D oceanBgTexture;
        public Rectangle oceanRectangle;

        public Texture2D seahorseLeftTexture;
        public Texture2D seahorseRightTexture;
        public Texture2D blowfishTexture;
        public SpriteFont font;

        Paddle leftPaddle;
        Paddle rightPaddle;
        Ball ball;
        HUD score;

        public GameState currentGameState = GameState.Start;
        public PingPong()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            leftPaddle = new Paddle(seahorseLeftTexture, new Vector2(20, (WINDOWHEIGHT / 2) - (seahorseLeftTexture.Height / 2)), 5, WINDOWHEIGHT, true);
            rightPaddle = new Paddle(seahorseRightTexture, new Vector2(WINDOWWIDTH - seahorseRightTexture.Width, (WINDOWHEIGHT / 2) - (seahorseRightTexture.Height / 2)), 5, WINDOWHEIGHT, false);
            ball = new Ball(blowfishTexture, new Vector2(400, 300), new Vector2(5, 5), WINDOWWIDTH, WINDOWHEIGHT);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            oceanBgTexture = Content.Load<Texture2D>("oceanbg");
            seahorseLeftTexture = Content.Load<Texture2D>("seahorse-left");
            seahorseRightTexture = Content.Load<Texture2D>("seahorse-right");
            blowfishTexture = Content.Load<Texture2D>("blowfish-ball");
            score = new HUD(Content.Load<SpriteFont>("ConcertOne"), WINDOWWIDTH);
            font = Content.Load<SpriteFont>("Sriracha");
        }

        protected override void Update(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Start:
                    if (currentGameState == GameState.Start && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.Initialize;
                    }
                    break;
                case GameState.Initialize:
                    if (currentGameState == GameState.Initialize && Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        ball.Reset();
                    }
                    else
                    {
                        currentGameState = GameState.Serving;
                    }
                    break;
                case GameState.Serving:
                    //
                    break;
                case GameState.Playing:
                    if (ball.position.X < leftPaddle.position.X - ball.texture.Width)
                    {
                        score.Player2Score();
                        ball.Reset();
                    }
                    if (ball.position.X > rightPaddle.position.X + rightPaddle.texture.Width)
                    {
                        score.Player1Score();
                        ball.Reset();
                    }

                    leftPaddle.Update(gameTime);
                    rightPaddle.Update(gameTime);
                    ball.Update();
                    CollisionCheck();
                    break;
                case GameState.GameOver:
                    // Update logic for game over state
                    break;
            }
            //if (ball.position.X < leftPaddle.position.X - ball.texture.Width)
            //{
            //    score.Player2Score();
            //    ball.Reset();
            //}
            //if (ball.position.X > rightPaddle.position.X + rightPaddle.texture.Width)
            //{
            //    score.Player1Score();
            //    ball.Reset();
            //}

            //leftPaddle.Update(gameTime);
            //rightPaddle.Update(gameTime);
            //ball.Update();
            //CollisionCheck();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            switch (currentGameState)
            {
                case GameState.Start:
            
                    break;
                case GameState.Initialize:

                    break;
                case GameState.Serving:
                    // Update logic for serving state
                    break;
                case GameState.Playing:
                    // Update logic for playing state
                    break;
                case GameState.GameOver:
                    // Update logic for game over state
                    break;
            }
            if (currentGameState == GameState.Start)
            {
                _spriteBatch.Draw(oceanBgTexture, oceanRectangle = new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                string message = "Welcome to Ping Pong!";
                string message2 = "Click anywhere to Start!";
                Vector2 messageSize = font.MeasureString(message);
                Vector2 messageSize2 = font.MeasureString(message2);
                Vector2 messagePosition = new Vector2((WINDOWWIDTH - messageSize.X) / 2, (WINDOWHEIGHT - messageSize.Y) / 8);
                Vector2 messagePosition2 = new Vector2((WINDOWWIDTH - messageSize2.X) / 2, (WINDOWHEIGHT - messageSize2.Y) / 2);
                _spriteBatch.DrawString(font, message, messagePosition, Color.White);
                _spriteBatch.DrawString(font, message2, messagePosition2, Color.White);
            }
            else
            {
                _spriteBatch.Draw(oceanBgTexture, oceanRectangle = new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                leftPaddle.Draw(_spriteBatch);
                rightPaddle.Draw(_spriteBatch);
                ball.Draw(_spriteBatch);

                score.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void CollisionCheck()
        {
            Rectangle ballRect = ball.BoundingBox();
            Rectangle paddle1Rect = leftPaddle.BoundingBox();
            Rectangle paddle2Rect = rightPaddle.BoundingBox();

            //Check collision with paddle 1
            if (ballRect.Intersects(paddle1Rect))
            {
                //Once the ball collides with the paddle reverse and increase speed
                ball.velocity.X = -ball.velocity.X;
                ball.velocity.X *= 1.1f;
                score.Player1Score();

                ball.velocity.Y = (float)random.NextDouble() * 8f - 4f;
            }
            //Check collision with paddle 2
            if (ballRect.Intersects(paddle2Rect))
            {
                //Once the ball collides with the paddle reverse and increase speed
                ball.velocity.X = -ball.velocity.X;
                ball.velocity.X *= 1.1f;
                score.Player2Score();

                ball.velocity.Y = (float)random.NextDouble() * 8f - 4f;
            }
            score.HighScore();
        }
    }
}