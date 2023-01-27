using Lesson05_Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMIT1514_Lab1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        CelAnimationSequence fox;
        Texture2D foxTexture;
        Vector2 foxDirection = new Vector2();
        Rectangle foxRectangle = new Rectangle();

        CelAnimationPlayer animationPlayer;

        Texture2D forestTexture;
        Vector2 forestDirection = new Vector2();
        Rectangle forestRectangle = new Rectangle();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 444;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            forestTexture = Content.Load<Texture2D>("forest-bg");
            foxTexture = Content.Load<Texture2D>("FoxSpriteSheet0");
            Texture2D spriteSheet = Content.Load<Texture2D>("FoxSpriteSheet0");
            fox = new CelAnimationSequence(spriteSheet, 90, 1 / 8.0f);

            animationPlayer = new CelAnimationPlayer();
            animationPlayer.Play(fox);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (foxRectangle.Bottom > _graphics.PreferredBackBufferHeight || foxRectangle.Top < 0)
            {
                foxDirection.Y *= -1;
            }
            if (foxRectangle.Left < 0 || foxRectangle.Right > _graphics.PreferredBackBufferWidth)
            {
                foxDirection.X *= -1;
            }
            foxRectangle.Offset(foxDirection);

            animationPlayer.Update(gameTime);   

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(forestTexture, forestRectangle = new Rectangle(0, 0, 800, 444), Color.White);
            animationPlayer.Draw(_spriteBatch, Vector2.Zero, SpriteEffects.None);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}