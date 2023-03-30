using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4_Kiana_Leslie
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int WINDOWWIDTH = 550;
        public const int WINDOWHEIGHT = 400;
        public const int ENEMIES = 1;
        public string message;
        public Texture2D bgTexture;
        public SpriteFont magraFont;
        public Player player;
        public Enemy[] enemies;

        public States.GameStates gameState;
        public KeyboardState keyboardState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
            _graphics.ApplyChanges();

            gameState = States.GameStates.Playing;
            keyboardState = Keyboard.GetState();
            player = new Player();
            enemies = new Enemy[ENEMIES];

            for (int c = 0; c < ENEMIES; c++)
            {
                enemies[c] = new Enemy();
            }

            base.Initialize();
            player.Initialize(new Vector2(50, 325), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT));
            int spaceBetweenMosquitoes = 1;
            foreach (Enemy mosquito in enemies)
            {
                mosquito.Initialize(new Vector2(spaceBetweenMosquitoes, 25), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), new Vector2(-1, 0));
                spaceBetweenMosquitoes += 50;
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bgTexture = Content.Load<Texture2D>("bg");
            magraFont = Content.Load<SpriteFont>("magra");

            player.LoadContent(Content);

            foreach (Enemy mosquito in enemies)
            {
                mosquito.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();
            switch (gameState)
            {
                case States.GameStates.Playing:
                    if (kbState.IsKeyDown(Keys.P) && keyboardState.IsKeyUp(Keys.P))
                    {
                        gameState = States.GameStates.Paused;
                        message = "Game Paused, press P to start playing.";
                    }
                    if (kbState.IsKeyDown(Keys.Left))
                    {
                        player.Move(new Vector2(-1, 0));
                    }
                    else if (kbState.IsKeyDown(Keys.Right))
                    {
                        player.Move(new Vector2(1, 0));
                    }
                    else
                    {
                        player.Move(new Vector2(0, 0));
                    }
                    player.Update(gameTime);

                    if (kbState.IsKeyDown(Keys.Space) && keyboardState.IsKeyUp(Keys.Space))
                    {
                        player.Shoot();
                    }
                    foreach (Enemy mosquito in enemies)
                    {
                        mosquito.Update(gameTime);
                        if (mosquito.Alive() && player.ProcessProjectileCollisions(mosquito.BoundingBox))
                        {
                            mosquito.Die();
                        }
                        if (player.Alive() && mosquito.ProcessProjectileCollisions(player.BoundingBox))
                        {
                            player.Die();
                        }
                    }
                    break;
                case States.GameStates.Paused:
                    if (kbState.IsKeyDown(Keys.P) && keyboardState.IsKeyUp(Keys.P))
                    {
                        gameState = States.GameStates.Playing;
                        message = "Paused";
                    }
                    break;
                case States.GameStates.NewLevel:
                    break;
                case States.GameStates.Over:
                    break;
            }
            keyboardState = kbState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            switch (gameState)
            {
                case States.GameStates.Playing:
                    _spriteBatch.Draw(bgTexture, Vector2.Zero, Color.White);
                    player.Draw(_spriteBatch);
                    foreach (Enemy mosquito in enemies)
                    {
                        mosquito.Draw(_spriteBatch);
                    }
                    break;
                case States.GameStates.Paused:
                    _spriteBatch.Draw(bgTexture, Vector2.Zero, Color.LightGray);
                    _spriteBatch.DrawString(magraFont, message, new Vector2(20, 50), Color.White);
                    break;
                case States.GameStates.Over:
                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}