using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMIT1514_Lab4_Kiana_Leslie
{
    public class BetterMosquitoes : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WINDOWWIDTH = 800;
        const int WINDOWHEIGHT = 600;

        Texture2D bg;

        Texture2D playerTexture;

        Player player;
        Controls playerControls;
        public BetterMosquitoes()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playerControls = new Controls(GamePad.GetState(0).DPad.Right == ButtonState.Pressed, GamePad.GetState(0).DPad.Left == ButtonState.Pressed, GamePad.GetState(0).Buttons.A == ButtonState.Pressed);
            //Keyboard.GetState().IsKeyDown(Keys.A)
            base.Initialize();
            //INTITALIZE rect or bounds below base.Initalize();
            player = new Player(playerTexture, Vector2.Zero, playerControls);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bg = Content.Load<Texture2D>("Background");
            playerTexture = Content.Load<Texture2D>("Cannon");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bg, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}