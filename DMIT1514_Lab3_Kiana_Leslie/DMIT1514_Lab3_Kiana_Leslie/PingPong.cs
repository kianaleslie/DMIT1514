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

        public const int WINDOWWIDTH = 1050;
        public const int WINDOWHEIGHT = 650;

        public Texture2D oceanBgTexture;
        public Rectangle oceanRectangle;

        public Texture2D seahorseLeftTexture;
        public Texture2D seahorseRightTexture;
        public Texture2D blowfishTexture;

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
            score = new HUD(Content.Load<SpriteFont>("Sriracha"), WINDOWHEIGHT);
        }

        protected override void Update(GameTime gameTime)
        {
            if (ball.position.X < leftPaddle.position.X - ball.texture.Width)
            {
                score.Player2Score();
                ball.Reset(); // Reset ball position
            }
            if (ball.position.X > rightPaddle.position.X + rightPaddle.texture.Width)
            {
                score.Player1Score();
                ball.Reset(); // Reset ball position
            }

            leftPaddle.Update(gameTime);
            rightPaddle.Update(gameTime);
            ball.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
             
            //background, both paddles, ball 
            _spriteBatch.Draw(oceanBgTexture, oceanRectangle = new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
            leftPaddle.Draw(_spriteBatch);
            rightPaddle.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);

            //score
            score.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}