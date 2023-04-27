using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Platformer
{
    public class Platformer : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int WINDOWWIDTH = 1080;
        public const int WINDOWHEIGHT = 720;
        public const int GRAV = 4;
        private string message;
        private Texture2D gameBackgroundTexture;
        private Texture2D menuBackgroundTexture;
        private Texture2D starTexture;
        private Vector2 starDirection = new Vector2();
        private Rectangle starRectangle = new Rectangle();
        private Texture2D planet0Texture;
        private Vector2 planet0Direction = new Vector2();
        private Rectangle planet0Rectangle = new Rectangle();
        private Texture2D planet1Texture;
        private Vector2 planet1Direction = new Vector2();
        private Rectangle planet1Rectangle = new Rectangle();
        private Texture2D planet2Texture;
        private Vector2 planet2Direction = new Vector2();
        private Rectangle planet2Rectangle = new Rectangle();
        private Texture2D planet3Texture;
        private Vector2 planet3Direction = new Vector2();
        private Rectangle planet3Rectangle = new Rectangle();
        private SpriteFont font;
        private SpriteFont italicFont;
        private States.GameStates gameState;
        private KeyboardState keyState;
        private Song song;
        private Rectangle GameBox = new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT);
        private Player player;
        private Collider ground;
        private Platforms[] platforms;
        private Collectable[] star;

        public Platformer()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            MediaPlayer.Volume = 0.5f;
            gameState = States.GameStates.Menu;
            starDirection = new Vector2(7f, 7f);
            planet0Direction = new Vector2(5f, 5f);
            planet1Direction = new Vector2(4f, 4f);
            planet2Direction = new Vector2(3f, 3f);
            planet3Direction = new Vector2(7f, 7f);

            _graphics.ApplyChanges();

            player = new Player(new Vector2(200, 680), GameBox);
            ground = new ColliderTop(new Vector2(0, 720), new Vector2(WINDOWWIDTH, 1));

            platforms = new Platforms[10];
            platforms[0] = new Platforms(new Vector2(50, 650), new Vector2(100, 25));
            platforms[1] = new Platforms(new Vector2(250, 550), new Vector2(100, 25));
            platforms[2] = new Platforms(new Vector2(100, 375), new Vector2(100, 25));
            platforms[3] = new Platforms(new Vector2(350, 250), new Vector2(100, 25));
            platforms[4] = new Platforms(new Vector2(500, 400), new Vector2(100, 25));
            platforms[5] = new Platforms(new Vector2(600, 650), new Vector2(100, 25));
            platforms[6] = new Platforms(new Vector2(800, 550), new Vector2(100, 25));
            platforms[7] = new Platforms(new Vector2(900, 300), new Vector2(100, 25));
            platforms[8] = new Platforms(new Vector2(600, 200), new Vector2(100, 25));
            platforms[9] = new Platforms(new Vector2(75, 100), new Vector2(100, 25));

            star = new Collectable[10];
            star[0] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(90, 600));
            star[1] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(280, 500));
            star[2] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(130, 325));
            star[3] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(380, 200));
            star[4] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(530, 350));
            star[5] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(630, 600));
            star[6] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(830, 500));
            star[7] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(930, 250));
            star[8] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(630, 150));
            star[9] = new Collectable(Content.Load<Texture2D>("collectStar"), new Vector2(105, 50));

            base.Initialize();

            player.Initialize();
            starRectangle = starTexture.Bounds;
            planet0Rectangle = planet0Texture.Bounds;
            planet1Rectangle = planet1Texture.Bounds;
            planet2Rectangle = planet2Texture.Bounds;
            planet3Rectangle = planet3Texture.Bounds;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            menuBackgroundTexture = Content.Load<Texture2D>("menuBg");
            gameBackgroundTexture = Content.Load<Texture2D>("gameImg");
            starTexture = Content.Load<Texture2D>("stars");
            planet0Texture = Content.Load<Texture2D>("planet09");
            planet1Texture = Content.Load<Texture2D>("planet00");
            planet2Texture = Content.Load<Texture2D>("planet01");
            planet3Texture = Content.Load<Texture2D>("planet03");
            font = Content.Load<SpriteFont>("Megrim-Regular");
            italicFont = Content.Load<SpriteFont>("Ysabeau-Italic-VariableFont_wght");
            song = Content.Load<Song>("space-journey-hartzmann-main-version-15284-03-33");
            player.LoadContent(Content);
            ground.LoadContent(Content);
            foreach (Platforms platforms in platforms)
            {
                platforms.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            switch (gameState)
            {
                case States.GameStates.Menu:

                    MediaPlayer.Play(song);
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameState = States.GameStates.LevelOne;
                    }
                    if (starRectangle.Bottom > WINDOWHEIGHT || starRectangle.Top < 0)
                    {
                        starDirection.Y *= -1;
                    }
                    if (starRectangle.Left < 0 || starRectangle.Right > WINDOWWIDTH)
                    {
                        starDirection.X *= -1;
                    }
                    starRectangle.Offset(starDirection);
                    if (planet0Rectangle.Bottom > WINDOWHEIGHT || planet0Rectangle.Top < 0)
                    {
                        planet0Direction.Y *= -1;
                    }
                    if (planet0Rectangle.Left < 0 || planet0Rectangle.Right > WINDOWWIDTH)
                    {
                        planet0Direction.X *= -1;
                    }
                    planet0Rectangle.Offset(planet0Direction);
                    if (planet1Rectangle.Bottom > WINDOWHEIGHT || planet1Rectangle.Top < 0)
                    {
                        planet1Direction.Y *= -1;
                    }
                    if (planet1Rectangle.Left < 0 || planet1Rectangle.Right > WINDOWWIDTH)
                    {
                        planet1Direction.X *= -1;
                    }
                    planet1Rectangle.Offset(planet1Direction);
                    if (planet2Rectangle.Bottom > WINDOWHEIGHT || planet2Rectangle.Top < 0)
                    {
                        planet2Direction.Y *= -1;
                    }
                    if (planet2Rectangle.Left < 0 || planet2Rectangle.Right > WINDOWWIDTH)
                    {
                        planet2Direction.X *= -1;
                    }
                    planet2Rectangle.Offset(planet2Direction);
                    if (planet3Rectangle.Bottom > WINDOWHEIGHT || planet3Rectangle.Top < 0)
                    {
                        planet3Direction.Y *= -1;
                    }
                    if (planet3Rectangle.Left < 0 || planet3Rectangle.Right > WINDOWWIDTH)
                    {
                        planet3Direction.X *= -1;
                    }
                    planet3Rectangle.Offset(planet3Direction);
                    break;
                case States.GameStates.LevelOne:

                    if (keys.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                    {
                        gameState = States.GameStates.Paused;
                        message = "Paused...";
                        MediaPlayer.Pause();
                    }
                    if (keys.IsKeyDown(Keys.Left))
                    {
                        player.MoveHorizontally(-1);
                    }
                    else if (keys.IsKeyDown(Keys.Right))
                    {
                        player.MoveHorizontally(1);
                    }
                    else
                    {
                        player.Stop();
                    }
                    if (keys.IsKeyDown(Keys.Space))
                    {
                        player.Jump();
                    }
                    ground.IsColliding(player);
                    foreach (Platforms platform in platforms)
                    {
                        platform.Collisions(player);
                    }
                    foreach (Collectable collectable in star)
                    {
                        collectable.Update(gameTime);
                        player.CollectStar(collectable);
                    }
                    player.Update(gameTime);
                    if (player.Stars == 10)
                    {
                        gameState = States.GameStates.GameOver;
                    }
                    break;
                case States.GameStates.Paused:

                    if (keys.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                    {
                        gameState = States.GameStates.LevelOne;
                        message = "";
                        MediaPlayer.Resume();
                    }
                    break;
                case States.GameStates.GameOver:

                    if (starRectangle.Bottom > WINDOWHEIGHT || starRectangle.Top < 0)
                    {
                        starDirection.Y *= -1;
                    }
                    if (starRectangle.Left < 0 || starRectangle.Right > WINDOWWIDTH)
                    {
                        starDirection.X *= -1;
                    }
                    starRectangle.Offset(starDirection);
                    if (planet0Rectangle.Bottom > WINDOWHEIGHT || planet0Rectangle.Top < 0)
                    {
                        planet0Direction.Y *= -1;
                    }
                    if (planet0Rectangle.Left < 0 || planet0Rectangle.Right > WINDOWWIDTH)
                    {
                        planet0Direction.X *= -1;
                    }
                    planet0Rectangle.Offset(planet0Direction);
                    if (planet1Rectangle.Bottom > WINDOWHEIGHT || planet1Rectangle.Top < 0)
                    {
                        planet1Direction.Y *= -1;
                    }
                    if (planet1Rectangle.Left < 0 || planet1Rectangle.Right > WINDOWWIDTH)
                    {
                        planet1Direction.X *= -1;
                    }
                    planet1Rectangle.Offset(planet1Direction);
                    if (planet2Rectangle.Bottom > WINDOWHEIGHT || planet2Rectangle.Top < 0)
                    {
                        planet2Direction.Y *= -1;
                    }
                    if (planet2Rectangle.Left < 0 || planet2Rectangle.Right > WINDOWWIDTH)
                    {
                        planet2Direction.X *= -1;
                    }
                    planet2Rectangle.Offset(planet2Direction);
                    if (planet3Rectangle.Bottom > WINDOWHEIGHT || planet3Rectangle.Top < 0)
                    {
                        planet3Direction.Y *= -1;
                    }
                    if (planet3Rectangle.Left < 0 || planet3Rectangle.Right > WINDOWWIDTH)
                    {
                        planet3Direction.X *= -1;
                    }
                    planet3Rectangle.Offset(planet3Direction);
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

                    _spriteBatch.Draw(menuBackgroundTexture, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    string message1 = "Welcome to Space Jump!";
                    string message2 = "click anywhere to start the adventure!";
                    string message3 = "...press *p* to pause the game...";
                    Vector2 messageSize = font.MeasureString(message1);
                    Vector2 messageSize2 = font.MeasureString(message2);
                    Vector2 messageSize3 = italicFont.MeasureString(message3);
                    Vector2 messagePosition = new Vector2((WINDOWWIDTH - messageSize.X) / 2, (WINDOWHEIGHT - messageSize.Y) / 4);
                    Vector2 messagePosition2 = new Vector2((WINDOWWIDTH - messageSize2.X) / 2, (WINDOWHEIGHT - messageSize2.Y) / 2);
                    Vector2 messagePosition3 = new Vector2((WINDOWWIDTH - messageSize3.X) / 2, (WINDOWHEIGHT - messageSize3.Y));
                    _spriteBatch.DrawString(font, message1, messagePosition, Color.White);
                    _spriteBatch.DrawString(font, message2, messagePosition2, Color.White);
                    _spriteBatch.DrawString(italicFont, message3, messagePosition3, Color.White);
                    _spriteBatch.Draw(starTexture, starRectangle.Location.ToVector2(), Color.White);
                    _spriteBatch.Draw(planet0Texture, planet0Rectangle.Location.ToVector2(), Color.White);
                    _spriteBatch.Draw(planet1Texture, planet1Rectangle.Location.ToVector2(), Color.White);
                    _spriteBatch.Draw(planet2Texture, planet2Rectangle.Location.ToVector2(), Color.White);
                    _spriteBatch.Draw(planet3Texture, planet3Rectangle.Location.ToVector2(), Color.White);
                    break;
                case States.GameStates.LevelOne:
                    
                   _spriteBatch.Draw(gameBackgroundTexture, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    _spriteBatch.DrawString(font, "Collect all the stars to win!", new Vector2(180, 5), Color.White);
                    player.Draw(_spriteBatch);
                    ground.Draw(_spriteBatch);
                    foreach (Platforms platform in platforms)
                    {
                        platform.Draw(_spriteBatch);
                    }
                    foreach (Collectable stars in star)
                    {
                        stars.Draw(_spriteBatch);
                    }
                    break;
                case States.GameStates.Paused:

                    _spriteBatch.Draw(menuBackgroundTexture, Vector2.Zero, Color.White);
                    _spriteBatch.DrawString(font, message, new Vector2(450, 300), Color.White);
                    break;
                case States.GameStates.GameOver:

                    _spriteBatch.Draw(menuBackgroundTexture, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    string gO = "Game Over!";
                    string gO1 = "I hope you had fun!";
                    Vector2 ms = font.MeasureString(gO);
                    Vector2 msg = font.MeasureString(gO1);
                    Vector2 msPos = new Vector2((WINDOWWIDTH - ms.X) / 2, (WINDOWHEIGHT - ms.Y) / 4);
                    Vector2 msgPos = new Vector2((WINDOWWIDTH - msg.X) / 2, (WINDOWHEIGHT - msg.Y) / 2);
                    _spriteBatch.DrawString(font, gO, msPos, Color.White);
                    _spriteBatch.DrawString(font, gO1, msgPos, Color.White);
                    _spriteBatch.Draw(starTexture, starRectangle.Location.ToVector2(), Color.White);
                    _spriteBatch.Draw(planet0Texture, planet0Rectangle.Location.ToVector2(), Color.White);
                    _spriteBatch.Draw(planet1Texture, planet1Rectangle.Location.ToVector2(), Color.White);
                    _spriteBatch.Draw(planet2Texture, planet2Rectangle.Location.ToVector2(), Color.White);
                    _spriteBatch.Draw(planet3Texture, planet3Rectangle.Location.ToVector2(), Color.White);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}