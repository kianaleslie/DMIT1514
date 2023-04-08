using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4_Kiana_Leslie
{
    public class DragonSiege : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int WINDOWWIDTH = 550;
        public const int WINDOWHEIGHT = 400;
        public const int ENEMIES = 7;
        public string message;
        public Texture2D bgTexture;
        public SpriteFont font;
        public Player player;
        public Enemy[] enemies;
        public States.GameStates gameState;
        public KeyboardState keyboardState;
        public Hud score;

        public DragonSiege()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            gameState = States.GameStates.Menu;
            keyboardState = Keyboard.GetState();
            player = new Player();
            enemies = new Enemy[ENEMIES];

            for (int index = 0; index < ENEMIES; index++)
            {
                enemies[index] = new Enemy();
            }

            base.Initialize();

            player.Initialize(new Vector2(50, 350), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT));
            int dragonSpaces = 1;
            foreach (Enemy dragon in enemies)
            {
                dragon.Initialize(new Vector2(dragonSpaces, 25), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), new Vector2(-1, 0));
                dragonSpaces += 50;
            }
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bgTexture = Content.Load<Texture2D>("backG");
            font = Content.Load<SpriteFont>("magra");
            player.LoadContent(Content);
            score = new Hud(Content.Load<SpriteFont>("magra"), WINDOWWIDTH);
            foreach (Enemy dragon in enemies)
            {
                dragon.LoadContent(Content);
            }
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();
            switch (gameState)
            {
                case States.GameStates.Menu:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameState = States.GameStates.LevelOne;
                    }
                    break;
                case States.GameStates.LevelOne:
                    if (kbState.IsKeyDown(Keys.P) && keyboardState.IsKeyUp(Keys.P))
                    {
                        gameState = States.GameStates.Paused;
                        message = "Paused!";
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
                    foreach (Enemy dragons in enemies)
                    {
                        dragons.Update(gameTime);
                        if (dragons.Alive() && player.Collisions(dragons.bBox))
                        {
                            dragons.Die();
                            score.PlayerScore();
                        }
                        if (player.Alive() && dragons.Collisions(player.bBox))
                        {
                            player.Die();
                        }
                    }
                    break;
                case States.GameStates.Paused:
                    if (kbState.IsKeyDown(Keys.P) && keyboardState.IsKeyUp(Keys.P))
                    {
                        gameState = States.GameStates.LevelOne;
                        message = "Paused!";
                    }
                    break;
                case States.GameStates.LevelTwo:
                    break;
                case States.GameStates.GameOver:
                    if(kbState.IsKeyDown(Keys.Enter))
                    {
                        score.ScoreReset();
                        gameState = States.GameStates.Menu;
                    }
                    break;
            }
            keyboardState = kbState;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            switch (gameState)
            {
                case States.GameStates.Menu:
                    _spriteBatch.Draw(bgTexture, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    string message1 = "Welcome to Dragon Siege!";
                    string message2 = "Click anywhere to Start!";
                    Vector2 messageSize = font.MeasureString(message1);
                    Vector2 messageSize2 = font.MeasureString(message2);
                    Vector2 messagePosition = new Vector2((WINDOWWIDTH - messageSize.X) / 2, (WINDOWHEIGHT - messageSize.Y) / 8);
                    Vector2 messagePosition2 = new Vector2((WINDOWWIDTH - messageSize2.X) / 2, (WINDOWHEIGHT - messageSize2.Y) / 2);
                    _spriteBatch.DrawString(font, message1, messagePosition, Color.White);
                    _spriteBatch.DrawString(font, message2, messagePosition2, Color.White);
                    break;
                case States.GameStates.LevelOne:
                    _spriteBatch.Draw(bgTexture, new Rectangle(0,0,WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    player.Draw(_spriteBatch);
                    foreach (Enemy dragon in enemies)
                    {
                        dragon.Draw(_spriteBatch);
                    }
                    break;
                case States.GameStates.Paused:
                    _spriteBatch.Draw(bgTexture, Vector2.Zero, Color.LightGray);
                    _spriteBatch.DrawString(font, message, new Vector2(180, 150), Color.White);
                    break;
                case States.GameStates.GameOver:
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}