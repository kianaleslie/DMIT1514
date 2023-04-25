using Lesson05_Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Text;

namespace DMIT1514_Lab1
{
    public class ForestRun : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        CelAnimationSequence fox;

        CelAnimationPlayer animationPlayer;

        Texture2D forestTexture;
        Rectangle forestRectangle = new Rectangle();

        Texture2D personTexture;
        Vector2 personDirection = new Vector2();

        Texture2D bunnyTexture;

        public ForestRun()
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
            personDirection = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2 - 64, _graphics.GraphicsDevice.Viewport.Height / 2 - 64);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            forestTexture = Content.Load<Texture2D>("forest-bg");
            personTexture = Content.Load<Texture2D>("smallfry");
            bunnyTexture = Content.Load<Texture2D>("bunny");

            Texture2D spriteSheet = Content.Load<Texture2D>("FoxSpriteSheet0");
            fox = new CelAnimationSequence(spriteSheet, 90, 90, 1 / 8.0f);
            animationPlayer = new CelAnimationPlayer();
            animationPlayer.Play(fox);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.D))
            {
                personDirection.X += 10;
            }
            if (state.IsKeyDown(Keys.A))
            {
                personDirection.X -= 10;
            }
            if (state.IsKeyDown(Keys.W))
            {
                personDirection.Y -= 10;
            }
            if (state.IsKeyDown(Keys.S))
            {
                
                personDirection.Y += 10;
            }

            animationPlayer.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _spriteBatch.Draw(forestTexture, forestRectangle = new Rectangle(0, 0, 800, 444), Color.White);
            _spriteBatch.Draw(bunnyTexture, new Rectangle(580, 290, 100, 75), Color.White);
            _spriteBatch.Draw(personTexture, personDirection, Color.White);
            animationPlayer.Draw(_spriteBatch, new Vector2(200,300), SpriteEffects.None);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}