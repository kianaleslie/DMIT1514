using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PingPongPractice
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WINDOWWIDTH = 1050;
        const int WINDOWHEIGHT = 650;

        Texture2D oceanBgTexture;
        Rectangle oceanRectangle;

        Texture2D seahorseLeftTexture;
        Rectangle seahorseLeftRectangle = new Rectangle();
        Vector2 seahorseLeftDirection = new Vector2();

        Texture2D seahorseRightTexture;
        Rectangle seahorseRightRectangle = new Rectangle();
        Vector2 seahorseRightDirection = new Vector2();

        Texture2D blowfishTexture;
        Rectangle blowfishRectangle = new Rectangle();
        Vector2 blowfishDirection = new Vector2();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT; 
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            seahorseLeftDirection = new Vector2(5, WINDOWHEIGHT / 2 - seahorseLeftRectangle.Height / 2);
            seahorseRightDirection = new Vector2(/*WINDOWWIDTH - seahorseRightRectangle.Width - 5, WINDOWHEIGHT / 2 - seahorseRightRectangle.Height /2*/);
            blowfishDirection = new Vector2(2f, 2f);

            base.Initialize();

            blowfishRectangle = blowfishTexture.Bounds;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            oceanBgTexture = Content.Load<Texture2D>("oceanbg");
            seahorseLeftTexture = Content.Load<Texture2D>("seahorse-left");
            seahorseRightTexture = Content.Load<Texture2D>("seahorse-right");
            blowfishTexture = Content.Load<Texture2D>("blowfish-ball");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (blowfishRectangle.Bottom > _graphics.PreferredBackBufferHeight || blowfishRectangle.Top < 0)
            {
                blowfishDirection.Y *= -1;
            }
            if (blowfishRectangle.Left < 0 || blowfishRectangle.Right > _graphics.PreferredBackBufferWidth)
            {
                blowfishDirection.X *= -1;
            }
            blowfishRectangle.Offset(blowfishDirection);

            seahorseLeftRectangle.Offset(seahorseLeftDirection);
            seahorseRightRectangle.Offset(seahorseRightDirection);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(oceanBgTexture, oceanRectangle = new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
            _spriteBatch.Draw(seahorseLeftTexture, seahorseLeftDirection, Color.White);
            _spriteBatch.Draw(seahorseRightTexture, seahorseRightDirection, Color.White);
            _spriteBatch.Draw(blowfishTexture, blowfishRectangle, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}