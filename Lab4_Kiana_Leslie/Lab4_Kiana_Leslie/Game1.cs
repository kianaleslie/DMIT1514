using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Specialized;

namespace Lab4_Kiana_Leslie
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WIDTH = 550;
        const int HEIGHT = 400;

        CelAnimationSequence dragon;
        CelAnimationSequence wizard;
        CelAnimationPlayer animationPlayer;
        CelAnimationPlayer animationEnemy;

        Texture2D bgTexture;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WIDTH;
            _graphics.PreferredBackBufferHeight = HEIGHT;
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

            Texture2D spriteSheet = Content.Load<Texture2D>("dragon");
           dragon = new CelAnimationSequence(spriteSheet, 100, 1 / 8.0f);
            animationEnemy = new CelAnimationPlayer();
            animationEnemy.Play(dragon);

            Texture2D sprite = Content.Load<Texture2D>("wizard");
            wizard = new CelAnimationSequence(sprite, 20, 1 / 4.0f);
            animationPlayer = new CelAnimationPlayer();
            animationPlayer.Play(wizard);

            bgTexture = Content.Load<Texture2D>("bg");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            animationPlayer.Update(gameTime);
            animationEnemy.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bgTexture, new Rectangle(0, 0, WIDTH, HEIGHT), Color.White);
            animationPlayer.Draw(_spriteBatch, new Vector2(200, 350), SpriteEffects.None);
            animationEnemy.Draw(_spriteBatch, new Vector2(100, 30), SpriteEffects.None);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}