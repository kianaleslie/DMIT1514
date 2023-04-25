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
        public Texture2D barrierTexture;
        public Texture2D textureHud;
        public SpriteFont font;
        public Player player;
        public Buddy buddy;
        public Enemy[] enemieslevelOne;
        public Enemy[] enemieslevelTwo;
        public GameStates gameState;
        public KeyboardState keyState;
        public Hud hud;
        public Barrier barrier;
        public Barrier barrier2;
        public bool IsLvl1;
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
            buddy = new Buddy();
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
            barrier = new Barrier(barrierTexture, new Vector2(150, 225));
            barrier2 = new Barrier(barrierTexture, new Vector2(325, 225));
            player.Initialize(new Vector2(50, 350), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT));
            buddy.Initialize(new Vector2(80, 335), new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT));
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
            buddy.LoadContent(Content);
            barrierTexture = Content.Load<Texture2D>("clouds");
            hud = new Hud(Content.Load<SpriteFont>("magra"), WINDOWHEIGHT);
            textureHud = Content.Load<Texture2D>("whiteBlock");
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
                case GameStates.Menu:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameState = States.GameStates.LevelOne;
                    }
                    break;
                case GameStates.LevelOne:
                    //gameState = States.GameStates.LevelOne;
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
                            buddy.Move(new Vector2(-1, 0) * speed);
                        }
                        else if (keys.IsKeyDown(Keys.Right))
                        {
                            player.Move(new Vector2(1, 0) * speed);
                            buddy.Move(new Vector2(1, 0) * speed);
                        }
                        else
                        {
                            player.Move(new Vector2(0, 0) * speed);
                            buddy.Move(new Vector2(0, 0) * speed);
                        }
                    }
                    player.Update(gameTime);
                    foreach (PlayerProjectile projectile in player.projectiles)
                    {
                        if (projectile.projectileState == ProjectileState.Flying)
                        {
                            projectile.Update(gameTime);
                            if (projectile.IsColliding(barrier.BoundingBox()) || projectile.IsColliding(barrier2.BoundingBox()))
                            {
                                _ = projectile.projectileState == ProjectileState.NotFlying;
                            }
                        }
                    }
                    foreach (Enemy enemy in enemieslevelOne)
                    {
                        foreach (EnemyProjectile projectile in enemy.projectiles)
                        {
                            if (projectile.projectileState == ProjectileState.Flying)
                            {
                                projectile.Update(gameTime);
                                if (projectile.IsColliding(barrier.BoundingBox()) || projectile.IsColliding(barrier2.BoundingBox()))
                                {
                                    _ = projectile.projectileState == ProjectileState.NotFlying;
                                }
                            }
                        }
                    }
                    if (keys.IsKeyDown(Keys.Space) && keyState.IsKeyUp(Keys.Space))
                    {
                        player.Shoot();
                        //buddy.Shoot();
                    }
                    foreach (Enemy dragons in enemieslevelOne)
                    {
                        dragons.Update(gameTime);
                        if (dragons.Alive() && player.Collisions(new Rectangle(dragons.position.ToPoint(), new Point(67, 58))))
                        {
                            dragons.Die();
                            hud.PlayerScore();
                            hud.HighScore();
                        }
                        if (player.Alive() && dragons.Collisions(new Rectangle(player.position.ToPoint(), new Point(32, 31))))
                        {
                            if (hud.GetLives() == 0 && !player.Alive())
                            {
                                player.Die();
                                hud.ScoreReset();
                            }
                            hud.DecreaseLives();
                        }
                    }
                    barrier.Update(gameTime);
                    barrier2.Update(gameTime);
                    keyState = Keyboard.GetState();
                    break;
                case GameStates.Paused:
                    if (IsLvl1)
                    {
                        if (keys.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                        {
                            gameState = States.GameStates.LevelOne;
                            message = "Paused!";
                        }
                    }
                    else
                    {
                        if (keys.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                        {
                            gameState = States.GameStates.LevelOne;
                            message = "Paused!";
                        }
                    }
                    break;
                case GameStates.LevelTwo:
                    gameState = States.GameStates.LevelTwo;
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
                            buddy.Move(new Vector2(-1, 0) * speed);
                        }
                        else if (keys.IsKeyDown(Keys.Right))
                        {
                            player.Move(new Vector2(1, 0) * speed);
                            buddy.Move(new Vector2(1, 0) * speed);
                        }
                        else
                        {
                            player.Move(new Vector2(0, 0) * speed);
                            buddy.Move(new Vector2(0, 0) * speed);
                        }
                    }
                    player.Update(gameTime);
                    foreach (PlayerProjectile projectile in player.projectiles)
                    {
                        if (projectile.projectileState == ProjectileState.Flying)
                        {
                            projectile.Update(gameTime);
                            if (projectile.IsColliding(barrier.BoundingBox()) || projectile.IsColliding(barrier2.BoundingBox()))
                            {
                                _ = projectile.projectileState == ProjectileState.NotFlying;
                            }
                        }
                    }
                    foreach (Enemy enemy in enemieslevelTwo)
                    {
                        foreach (EnemyProjectile projectile in enemy.projectiles)
                        {
                            if (projectile.projectileState == ProjectileState.Flying)
                            {
                                projectile.Update(gameTime);
                                if (projectile.IsColliding(barrier.BoundingBox()) || projectile.IsColliding(barrier2.BoundingBox()))
                                {
                                    _ = projectile.projectileState == ProjectileState.NotFlying;
                                }
                            }
                        }
                    }
                    if (keys.IsKeyDown(Keys.Space) && keyState.IsKeyUp(Keys.Space))
                    {
                        player.Shoot();
                        //buddy.Shoot();
                    }
                    foreach (Enemy enemy in enemieslevelTwo)
                    {
                        enemy.Update(gameTime);
                        if (enemy.Alive() && player.Collisions(new Rectangle(enemy.position.ToPoint(), new Point(67, 58))))
                        {
                            enemy.Die();
                            hud.PlayerScore();
                            hud.HighScore();
                        }
                        if (player.Alive() && enemy.Collisions(new Rectangle(player.position.ToPoint(), new Point(32, 31))))
                        {
                            player.Die();
                        }
                    }
                    barrier.Update(gameTime);
                    barrier2.Update(gameTime);
                    keyState = Keyboard.GetState();
                    break;
                case GameStates.GameOver:
                    //Was going to make a re-play mechanic, but didn't have time to add a initalize case in as I need time to work on platformer
                    //if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    //{
                    //hud.ScoreReset();
                    //gameState = States.GameStates.Menu;
                    //}
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
                case GameStates.Menu:
                    _spriteBatch.Draw(bgTextureMenu, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    string message1 = "Welcome to Dragon Siege!";
                    string message2 = "Click anywhere to Start!";
                    string message3 = "Press P to Pause the game.";
                    Vector2 messageSize = font.MeasureString(message1);
                    Vector2 messageSize2 = font.MeasureString(message2);
                    Vector2 messageSize3 = font.MeasureString(message3);
                    Vector2 messagePosition = new Vector2((WINDOWWIDTH - messageSize.X) / 2, (WINDOWHEIGHT - messageSize.Y) / 8);
                    Vector2 messagePosition2 = new Vector2((WINDOWWIDTH - messageSize2.X) / 2, (WINDOWHEIGHT - messageSize2.Y) / 4);
                    Vector2 messagePosition3 = new Vector2((WINDOWWIDTH - messageSize2.X) / 2, (WINDOWHEIGHT - messageSize3.Y) / 2);
                    _spriteBatch.DrawString(font, message1, messagePosition, Color.White);
                    _spriteBatch.DrawString(font, message2, messagePosition2, Color.White);
                    _spriteBatch.DrawString(font, message3, messagePosition3, Color.White);
                    break;
                case GameStates.LevelOne:
                    _spriteBatch.Draw(bgTexture1, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    _spriteBatch.Draw(textureHud, new Rectangle(0, 0, WINDOWWIDTH, 32), Color.White);
                    player.Draw(_spriteBatch);
                    buddy.Draw(_spriteBatch);
                    foreach (Enemy dragon in enemieslevelOne)
                    {
                        dragon.Draw(_spriteBatch);
                    }
                    barrier.Draw(_spriteBatch);
                    barrier2.Draw(_spriteBatch);

                    hud.Draw(_spriteBatch);
                    break;
                case GameStates.Paused:
                    _spriteBatch.Draw(bgTextureMenu, Vector2.Zero, Color.White);
                    _spriteBatch.DrawString(font, message, new Vector2(180, 150), Color.White);
                    break;
                case GameStates.LevelTwo:
                    _spriteBatch.Draw(bgTexture2, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    player.Draw(_spriteBatch);
                    buddy.Draw(_spriteBatch);
                    foreach (Enemy enemy in enemieslevelTwo)
                    {
                        enemy.Draw(_spriteBatch);
                    }
                    barrier.Draw(_spriteBatch);
                    barrier2.Draw(_spriteBatch);
                    _spriteBatch.Draw(textureHud, new Vector2(200, 550), Color.White);
                    hud.Draw(_spriteBatch);
                    break;
                case GameStates.GameOver:
                    _spriteBatch.Draw(bgTextureMenu, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    string gameOver = string.Format("Game Over!");
                    string gameOver2 = string.Format("Score: {0}", hud.playerScore);
                    string gameOver3 = string.Format("New High Score: {0}", hud.highScore);
                    Vector2 gameOverPos = new Vector2((WINDOWWIDTH - font.MeasureString(gameOver).X) / 2, 100);
                    Vector2 goPos = new Vector2((WINDOWWIDTH - font.MeasureString(gameOver2).X) / 2, 180);
                    Vector2 scoreDisplay = new Vector2((WINDOWWIDTH - font.MeasureString(gameOver3).X) / 2, 210);
                    _spriteBatch.DrawString(font, gameOver, gameOverPos, Color.White);
                    _spriteBatch.DrawString(font, gameOver2, goPos, Color.White);
                    _spriteBatch.DrawString(font, gameOver3, scoreDisplay, Color.AliceBlue);
                    hud.Draw(_spriteBatch);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}