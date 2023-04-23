using Lab4_Kiana_Leslie;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using static Lab4_Kiana_Leslie.States;

namespace Lab4_Kiana_Leslie
{
    public class DragonSiege : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int WINDOWWIDTH = 550;
        public const int WINDOWHEIGHT = 400;
        public const int ENEMIES = 7;
        public const int ENEMIES2 = 8;
        public float speed = 3f;
        public string message;
        public Texture2D bgTexture1;
        public Texture2D bgTexture2;
        public Texture2D bgTextureMenu;
        public SpriteFont font;
        public Player player;
        public Enemy[] enemieslevelOne;
        public Enemy[] enemieslevelTwo;
        public States.GameStates gameState;
        public KeyboardState keyState;
        public Hud score;
        public Barrier barrier = new Barrier(new Vector2(100, 400));
        public Texture2D barrierTexture;

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
            keyState = Keyboard.GetState();
            player = new Player();
            enemieslevelOne = new Enemy[ENEMIES];
            enemieslevelTwo = new Enemy[ENEMIES2];

            for (int index = 0; index < ENEMIES; index++)
            {
                enemieslevelOne[index] = new Enemy();
            }
            for (int index = 0; index < ENEMIES2; index++)
            {
                enemieslevelTwo[index] = new Enemy();
            }
            base.Initialize();
            
            player.Initialize(new Vector2(50, 350), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT));
            //barrier.Initialize(new Vector2(100, 400), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT));
            int dragonSpace = 1;
            int enemySpace = 1;
            foreach (Enemy dragon in enemieslevelOne)
            {
                dragon.Initialize(new Vector2(dragonSpace, 25), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), new Vector2(-1, 0));
                dragonSpace += 50;
            }
            foreach (Enemy enemy in enemieslevelTwo)
            {
                enemy.Initialize(new Vector2(enemySpace, 25), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), new Vector2(-1, 0));
                enemySpace += 50;
            }
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bgTexture1 = Content.Load<Texture2D>("backG");
            bgTexture2 = Content.Load<Texture2D>("background");
            bgTextureMenu = Content.Load<Texture2D>("bg");
            font = Content.Load<SpriteFont>("magra");
            player.LoadContent(Content);
            barrier.LoadContent(Content);
            score = new Hud(Content.Load<SpriteFont>("magra"), WINDOWWIDTH);
            foreach (Enemy dragon in enemieslevelOne)
            {
                dragon.LoadContent(Content);
            }
            foreach (Enemy enemy in enemieslevelTwo)
            {
                enemy.LoadContent(Content);
            }
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            switch (gameState)
            {
                case States.GameStates.Menu:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameState = States.GameStates.LevelOne;
                    }
                    break;
                case States.GameStates.LevelOne:
                    if (enemieslevelOne.All(enemy => !enemy.Alive()))
                    {
                        gameState = States.GameStates.LevelTwo;
                    }
                    else if (!player.Alive())
                    {
                        gameState = States.GameStates.GameOver;
                    }
                    else
                    {
                        if (keys.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                        {
                            gameState = States.GameStates.Paused;
                            message = "Paused!";
                        }
                        if (keys.IsKeyDown(Keys.Left))
                        {
                            player.Move(new Vector2(-1, 0) * speed);
                        }
                        else if (keys.IsKeyDown(Keys.Right))
                        {
                            player.Move(new Vector2(1, 0) * speed);
                        }
                        else
                        {
                            player.Move(new Vector2(0, 0) * speed);
                        }
                    }
                    player.Update(gameTime);

                    if (keys.IsKeyDown(Keys.Space) && keyState.IsKeyUp(Keys.Space))
                    {
                        player.Shoot();
                    }
                    foreach (Enemy dragons in enemieslevelOne)
                    {
                        dragons.Update(gameTime);
                        if (dragons.Alive() && player.Collisions(new Rectangle(dragons.position.ToPoint(), new Point(67, 58))))
                        {
                            dragons.Die();
                            score.PlayerScore();
                        }
                        if (player.Alive() && dragons.Collisions(new Rectangle(player.position.ToPoint(), new Point(32, 31))))
                        {
                            player.Die();
                        }
                    }
                    barrier.Update();
                    keyState = Keyboard.GetState();
                    break;
                case States.GameStates.Paused:
                    if (keys.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                    {
                        gameState = States.GameStates.LevelOne;
                        message = "Paused!";
                    }
                    break;
                case States.GameStates.LevelTwo:
                    if (enemieslevelTwo.All(enemy => !enemy.Alive()))
                    {
                        gameState = States.GameStates.GameOver;
                    }
                    else if (!player.Alive())
                    {
                        gameState = GameStates.GameOver;
                    }
                    else
                    {
                        if (keys.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                        {
                            gameState = States.GameStates.Paused;
                            message = "Paused!";
                        }
                        if (keys.IsKeyDown(Keys.Left))
                        {
                            player.Move(new Vector2(-1, 0) * speed);
                        }
                        else if (keys.IsKeyDown(Keys.Right))
                        {
                            player.Move(new Vector2(1, 0) * speed);
                        }
                        else
                        {
                            player.Move(new Vector2(0, 0) * speed);
                        }
                    }
                    player.Update(gameTime);

                    if (keys.IsKeyDown(Keys.Space) && keyState.IsKeyUp(Keys.Space))
                    {
                        player.Shoot();
                    }
                    foreach (Enemy enemy in enemieslevelTwo)
                    {
                        enemy.Update(gameTime);
                        if (enemy.Alive() && player.Collisions(new Rectangle(enemy.position.ToPoint(), new Point(67, 58))))
                        {
                            enemy.Die();
                            score.PlayerScore();
                        }
                        if (player.Alive() && enemy.Collisions(new Rectangle(player.position.ToPoint(), new Point(32, 31))))
                        {
                            player.Die();
                        }
                    }
                    barrier.Update();
                    keyState = Keyboard.GetState();
                    break;
                case States.GameStates.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        score.ScoreReset();
                        gameState = States.GameStates.Menu;
                    }
                    break;
            }
            keyState = Keyboard.GetState();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            switch (gameState)
            {
                case States.GameStates.Menu:
                    _spriteBatch.Draw(bgTextureMenu, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
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
                    _spriteBatch.Draw(bgTexture1, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    player.Draw(_spriteBatch);
                    foreach (Enemy dragon in enemieslevelOne)
                    {
                        dragon.Draw(_spriteBatch);
                    }
                    barrier.Draw(_spriteBatch);
                    break;
                case States.GameStates.Paused:
                    _spriteBatch.Draw(bgTextureMenu, Vector2.Zero, Color.White);
                    _spriteBatch.DrawString(font, message, new Vector2(180, 150), Color.White);
                    break;
                case States.GameStates.LevelTwo:
                    _spriteBatch.Draw(bgTexture2, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    player.Draw(_spriteBatch);
                    foreach (Enemy enemy in enemieslevelTwo)
                    {
                        enemy.Draw(_spriteBatch);
                    }
                    barrier.Draw(_spriteBatch);
                    break;
                case States.GameStates.GameOver:
                    _spriteBatch.Draw(bgTextureMenu, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    string gameOver1 = "Game Over!";
                    string gameOver2 = "Hit enter to play again!";
                    Vector2 gameOverSize = font.MeasureString(gameOver1);
                    Vector2 gameOverSize2 = font.MeasureString(gameOver2);
                    Vector2 mPos = new Vector2((WINDOWWIDTH - gameOverSize.X) / 2, (WINDOWHEIGHT - gameOverSize.Y) / 8);
                    Vector2 mPosition = new Vector2((WINDOWWIDTH - gameOverSize2.X) / 2, (WINDOWHEIGHT - gameOverSize2.Y) / 2);
                    _spriteBatch.DrawString(font, gameOver1, mPos, Color.White);
                    _spriteBatch.DrawString(font, gameOver2, mPosition, Color.White);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}