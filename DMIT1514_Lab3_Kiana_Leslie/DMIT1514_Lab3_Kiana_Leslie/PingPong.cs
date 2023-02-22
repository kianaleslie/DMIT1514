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
        public float timer = 0f;
        //TRIED COLOUR CHANGE
        //public bool colourChange;
        //public bool paddleHit;

        public Texture2D oceanBgTexture;
        public Rectangle oceanRectangle;

        public Texture2D seahorseLeftTexture;
        public Texture2D seahorseRightTexture;
        public Texture2D blowfishTexture;
        public Texture2D seashellTexture;
        public SpriteFont font;

        Paddle leftPaddle;
        Paddle rightPaddle;
        Ball ball;
        Ball ball2;
        HUD score;

        public static GameState currentGameState = GameState.Start;
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
            ball = new Ball(blowfishTexture, new Vector2(515, 325), new Vector2(6, 6), WINDOWWIDTH, WINDOWHEIGHT);
            ball2 = new Ball(seashellTexture, new Vector2(515, 300), new Vector2(6, 6), WINDOWWIDTH, WINDOWHEIGHT);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            oceanBgTexture = Content.Load<Texture2D>("oceanbg");
            seahorseLeftTexture = Content.Load<Texture2D>("seahorse-left");
            seahorseRightTexture = Content.Load<Texture2D>("seahorse-right");
            blowfishTexture = Content.Load<Texture2D>("blowfish-ball");
            seashellTexture = Content.Load<Texture2D>("seashell-ball-");
            score = new HUD(Content.Load<SpriteFont>("ConcertOne"), WINDOWWIDTH);
            font = Content.Load<SpriteFont>("Sriracha");
        }

        protected override void Update(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Start:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        currentGameState = GameState.Initialize;
                    }
                    break;

                case GameState.Initialize:
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (timer > 3000f)
                    {
                        currentGameState = GameState.Serving;
                    }
                    //TRIED SPEED INCREASE AND DECREASE W KEYBOARD INPUT
                    //if(Keyboard.GetState().IsKeyDown(Keys.O))
                    //{
                    //    ball.SpeedUp();
                    //}
                    //if(Keyboard.GetState().IsKeyDown(Keys.P))
                    //{
                    //    ball.SlowDown();
                    //}
                    break;

                case GameState.Serving:

                    currentGameState = GameState.Playing;

                    break;

                case GameState.Playing:

                    if (ball.position.X < leftPaddle.position.X - ball.texture.Width)
                    {
                        score.Player2Score();
                        ball.Reset();
                        currentGameState = GameState.GameOver;
                    }
                    if (ball.position.X > rightPaddle.position.X + rightPaddle.texture.Width)
                    {
                        score.Player1Score();
                        ball.Reset();
                        currentGameState = GameState.GameOver;
                    }
                    //second ball 
                    if (ball2.position.X < leftPaddle.position.X - ball2.texture.Width)
                    {
                        score.Player2Score();
                        ball2.Reset();
                        //currentGameState = GameState.GameOver;
                    }
                    if (ball2.position.X > rightPaddle.position.X + rightPaddle.texture.Width)
                    {
                        score.Player1Score();
                        ball2.Reset();
                        //currentGameState = GameState.GameOver;
                    }
                    //TRIED COLOUR CHANGE
                    //colourChange = leftPaddle.BoundingBox().Intersects(ball.BoundingBox());
                    //colourChange = rightPaddle.BoundingBox().Intersects(ball.BoundingBox());
                    //if (colourChange == true)
                    //{
                    //    {
                    //        timer = 500f;
                    //    }
                    //    if (timer > 0)
                    //    {
                    //        paddleHit = true;
                    //        timer--;
                    //    }
                    //    else
                    //    {
                    //        paddleHit = false;
                    //    }
                    //}
                    leftPaddle.Update(gameTime);
                    rightPaddle.Update(gameTime);
                    ball.Update();
                    ball2.Update();
                    CollisionCheck();

                    break;

                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        timer = 0;
                        score.ScoreReset();
                        currentGameState = GameState.Initialize;
                    }
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

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
            if (currentGameState == GameState.Initialize)
            {
                _spriteBatch.Draw(oceanBgTexture, oceanRectangle = new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                leftPaddle.Draw(_spriteBatch);
                rightPaddle.Draw(_spriteBatch);
                ball.Draw(_spriteBatch);
                ball2.Draw(_spriteBatch);

                score.Draw(_spriteBatch);
            }
            else
            if (currentGameState == GameState.Serving)
            {
                _spriteBatch.Draw(oceanBgTexture, oceanRectangle = new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                leftPaddle.Draw(_spriteBatch);
                rightPaddle.Draw(_spriteBatch);
                ball.Draw(_spriteBatch);
                ball2.Draw(_spriteBatch);

                score.Draw(_spriteBatch);
            }
            else
            if (currentGameState == GameState.Playing)
            {
                _spriteBatch.Draw(oceanBgTexture, oceanRectangle = new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                leftPaddle.Draw(_spriteBatch);
                rightPaddle.Draw(_spriteBatch);
                ball.Draw(_spriteBatch);
                ball2.Draw(_spriteBatch);

                score.Draw(_spriteBatch);
                //TRIED COLOUR CHANGE
                //if (paddleHit)
                //{
                //    leftPaddle.Draw(_spriteBatch, gameTime, Color.Black);
                //    rightPaddle.Draw(_spriteBatch, gameTime, Color.RoyalBlue);
                //}
                //else
                //{
                //    leftPaddle.Draw(_spriteBatch, gameTime, Color.White);
                //    rightPaddle.Draw(_spriteBatch, gameTime, Color.White);
                //}
            }
            else
            if (currentGameState == GameState.GameOver)
            {
                _spriteBatch.DrawString(font, "Press Enter to Play Again!", new Vector2(100, 100), Color.Black);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void CollisionCheck()
        {
            Rectangle ballRect = ball.BoundingBox();
            Rectangle ballRect2 = ball2.BoundingBox();  
            Rectangle paddle1Rect = leftPaddle.BoundingBox();
            Rectangle paddle2Rect = rightPaddle.BoundingBox();

            //Check collision with paddle 1
            if (ballRect.Intersects(paddle1Rect))
            {
                //Once the ball collides with the left paddle reverse and increase speed
                ball.velocity.X = -ball.velocity.X;
                ball.velocity.X *= 1.1f;
                score.Player1Score();

                ball.velocity.Y = (float)random.NextDouble() * 8f - 4f;
            }
            //Check collision with paddle 2
            if (ballRect.Intersects(paddle2Rect))
            {
                //Once the ball collides with the right paddle reverse and increase speed
                ball.velocity.X = -ball.velocity.X;
                ball.velocity.X *= 1.1f;
                score.Player2Score();

                ball.velocity.Y = (float)random.NextDouble() * 8f - 4f;
            }
            score.HighScore();

            if (ballRect2.Intersects(paddle1Rect))
            {
                ball2.velocity.X = -ball2.velocity.X;
                ball2.velocity.X *= 1.1f;
                //SECOND BALL SCORE
               //score.Player1Score();

                ball2.velocity.Y = (float)random.NextDouble() * 8f - 4f;
            }
            //Check collision with paddle 2
            if (ballRect2.Intersects(paddle2Rect))
            {
                ball2.velocity.X = -ball2.velocity.X;
                ball2.velocity.X *= 1.1f;
                //SECOND BALL SCORE
               //score.Player2Score();

                ball2.velocity.Y = (float)random.NextDouble() * 8f - 4f;
            }
        }
    }
}