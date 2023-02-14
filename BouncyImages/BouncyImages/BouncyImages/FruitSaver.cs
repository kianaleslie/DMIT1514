using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BouncyImages
{
    public class FruitSaver : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D cherriesTexture;
        private Vector2 cherriesDirection = new Vector2();
        private Rectangle cherriesRectangle = new Rectangle();

        private Texture2D pineappleTexture;
        private Vector2 pineappleDirection = new Vector2();
        private Rectangle pineappleRectangle = new Rectangle();

        private Texture2D appleTexture;
        private Vector2 appleDirection = new Vector2();
        private Rectangle appleRectangle = new Rectangle();

        private Texture2D bananaTexture;
        private Vector2 bananaDirection = new Vector2();
        private Rectangle bananaRectangle = new Rectangle();

        private Texture2D grapesTexture;
        private Vector2 grapesDirection = new Vector2();
        private Rectangle grapesRectangle = new Rectangle();

        private Texture2D pearTexture;
        private Vector2 pearDirection = new Vector2();
        private Rectangle pearRectangle = new Rectangle();

        private SpriteFont gameFont;

        public FruitSaver()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cherriesDirection = new Vector2(7f, 7f);    //describing the movement
            pineappleDirection = new Vector2(2f, 2f);
            appleDirection = new Vector2(3f, 3f);
            bananaDirection = new Vector2(4f, 4f);
            grapesDirection = new Vector2(5f, 5f);
            pearDirection = new Vector2(6f, 6f);

            base.Initialize();
            cherriesRectangle = cherriesTexture.Bounds;
            pineappleRectangle = pineappleTexture.Bounds;
            appleRectangle = appleTexture.Bounds;
            bananaRectangle = bananaTexture.Bounds;
            grapesRectangle = grapesTexture.Bounds;
            pearRectangle = pearTexture.Bounds;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            cherriesTexture = Content.Load<Texture2D>("cherries");
            pineappleTexture = Content.Load<Texture2D>("pineapple");
            appleTexture = Content.Load<Texture2D>("apple");
           bananaTexture = Content.Load<Texture2D>("banana");
            grapesTexture = Content.Load<Texture2D>("grapes");
            pearTexture = Content.Load<Texture2D>("pear");

            gameFont = Content.Load<SpriteFont>("CinzelDecorative");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //{
                if (cherriesRectangle.Bottom > _graphics.PreferredBackBufferHeight || cherriesRectangle.Top < 0)
                {
                    cherriesDirection.Y *= -1;
                }
                if (cherriesRectangle.Left < 0 || cherriesRectangle.Right > _graphics.PreferredBackBufferWidth)
                {
                    cherriesDirection.X *= -1;
                }
                cherriesRectangle.Offset(cherriesDirection);
            //}

            if (pineappleRectangle.Bottom > _graphics.PreferredBackBufferHeight || pineappleRectangle.Top < 0)
            {
                pineappleDirection.Y *= -1;
            }
            if (pineappleRectangle.Left < 0 || pineappleRectangle.Right > _graphics.PreferredBackBufferWidth)
            {
                pineappleDirection.X *= -1;
            }
            pineappleRectangle.Offset(pineappleDirection);

            if (appleRectangle.Bottom > _graphics.PreferredBackBufferHeight || appleRectangle.Top < 0)
            {
                appleDirection.Y *= -1;
            }
            if (appleRectangle.Left < 0 || appleRectangle.Right > _graphics.PreferredBackBufferWidth)
            {
                appleDirection.X *= -1;
            }
            appleRectangle.Offset(appleDirection);

            if (bananaRectangle.Bottom > _graphics.PreferredBackBufferHeight || bananaRectangle.Top < 0)
            {
                bananaDirection.Y *= -1;
            }
            if (bananaRectangle.Left < 0 || bananaRectangle.Right > _graphics.PreferredBackBufferWidth)
            {
                bananaDirection.X *= -1;
            }
            bananaRectangle.Offset(bananaDirection);

            if (grapesRectangle.Bottom > _graphics.PreferredBackBufferHeight || grapesRectangle.Top < 0)
            {
                grapesDirection.Y *= -1;
            }
            if (grapesRectangle.Left < 0 || grapesRectangle.Right > _graphics.PreferredBackBufferWidth)
            {
                grapesDirection.X *= -1;
            }
            grapesRectangle.Offset(grapesDirection);

            if (pearRectangle.Bottom > _graphics.PreferredBackBufferHeight || pearRectangle.Top < 0)
            {
                pearDirection.Y *= -1;
            }
            if (pearRectangle.Left < 0 || pearRectangle.Right > _graphics.PreferredBackBufferWidth)
            {
                pearDirection.X *= -1;
            }
            pearRectangle.Offset(pearDirection);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Thistle);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            Vector2 textCenter = gameFont.MeasureString("Welcome to fruit saver!") / 2f;
            _spriteBatch.Draw(cherriesTexture, cherriesRectangle.Location.ToVector2(), Color.White);
            _spriteBatch.Draw(pineappleTexture, new Rectangle(pineappleRectangle.X, pineappleRectangle.Y, 90, 90), Color.White);
            _spriteBatch.Draw(appleTexture, appleRectangle.Location.ToVector2(), Color.White);
            _spriteBatch.Draw(bananaTexture, bananaRectangle.Location.ToVector2(), Color.White);
            _spriteBatch.Draw(grapesTexture, grapesRectangle.Location.ToVector2(), Color.White);
            _spriteBatch.Draw(pearTexture, pearRectangle.Location.ToVector2(), Color.White);

            _spriteBatch.DrawString(gameFont, "Welcome to fruit saver!", new Vector2(400,200), Color.White, 0, textCenter, 2.0f, SpriteEffects.None, 0);
            _spriteBatch.End();

            

            base.Draw(gameTime);
        }
        enum SpriteState
        { 
        Left, 
        Up, 
        Down,
        Right
        }
        SpriteState currentSpriteState = SpriteState.Left;
    }
}