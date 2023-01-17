using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;

namespace Practice1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D astronautTexture;
        Texture2D spaceTexture;
        //Texture2D spaceTextureRectangle;
        Texture2D alienTexture;

        //express direction with 2 numbers
        Vector2 astronautDirection = new Vector2();
        Rectangle astronautRectangle = new Rectangle();

        Vector2 spaceDirection = Vector2.Zero;
        Rectangle spaceRectangle = new Rectangle();

        Vector2 alienDirection = new Vector2();
        Rectangle alienRectangle = new Rectangle();

        private SpriteFont astronautFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            astronautDirection = new Vector2(1f, 1f); //moves towards bottom right
            spaceDirection = new Vector2(0f, 0f);
            alienDirection = new Vector2(2f, 2f);   

            base.Initialize();
            //grab the rectangle from the texture after it has been loaded
            astronautRectangle = astronautTexture.Bounds;
            spaceRectangle = spaceTexture.Bounds;
            alienRectangle = alienTexture.Bounds;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            astronautTexture = Content.Load<Texture2D>("astronaut2");
            spaceTexture = Content.Load<Texture2D>("1876");
            alienTexture = Content.Load<Texture2D>("alien");
            astronautFont = Content.Load<SpriteFont>("SourceCodePro");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (astronautRectangle.Bottom > _graphics.PreferredBackBufferHeight || astronautRectangle.Top < 0)
                {
                    astronautDirection.Y *= -1;
                }
                if (astronautRectangle.Left < 0 || astronautRectangle.Right > _graphics.PreferredBackBufferWidth)
                {
                    astronautDirection.X *= -1;
                }
                astronautRectangle.Offset(astronautDirection);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (alienRectangle.Bottom > _graphics.PreferredBackBufferHeight || alienRectangle.Top < 0)
                {
                    alienDirection.Y *= -1;
                }
                if (alienRectangle.Left < 0 || alienRectangle.Right > _graphics.PreferredBackBufferWidth)
                {
                    alienDirection.X *= -1;
                }
                alienRectangle.Offset(alienDirection);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            Vector2 textCenter = astronautFont.MeasureString("Astronaut Saver...") / 2f;
            _spriteBatch.Draw(spaceTexture, new Rectangle(1,1,800,480), Color.White);
            _spriteBatch.Draw(astronautTexture, new Rectangle((int)astronautDirection.X,(int)astronautDirection.Y, 80,80), Color.White);
            _spriteBatch.Draw(alienTexture, new Rectangle((int)alienDirection.X,(int)alienDirection.Y,80,80), Color.White);

            _spriteBatch.DrawString(astronautFont, "Astronaut Saver...", new Vector2(400, 200), Color.White, 0, textCenter, 2.0f, SpriteEffects.None, 0);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}