using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platformer
{
    public class Platformer : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public const int WINDOWWIDTH = 1080;
        public const int WINDOWHEIGHT = 720;
        public string message;
        public Texture2D gameBackgroundTexture;
        public Texture2D menuBackgroundTexture;
        public SpriteFont font;
        public SpriteFont italicFont;
        public States.GameStates gameState;
        public KeyboardState keyState;

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
            gameState = States.GameStates.Menu;
            base.Initialize();
            
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            menuBackgroundTexture = Content.Load<Texture2D>("menuBg");
            gameBackgroundTexture = Content.Load<Texture2D>("menuBg");
            font = Content.Load<SpriteFont>("Megrim-Regular");
            italicFont = Content.Load<SpriteFont>("Ysabeau-Italic-VariableFont_wght");
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            switch (gameState)
            {
                case States.GameStates.Initalize:
                    break;
                case States.GameStates.Menu:
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        gameState = States.GameStates.LevelOne;
                    }
                    break;
                case States.GameStates.LevelOne:
                    break;
                case States.GameStates.Paused:
                    if (keys.IsKeyDown(Keys.P) && keyState.IsKeyUp(Keys.P))
                    {
                        gameState = States.GameStates.LevelOne;
                        message = "Paused...";
                    }
                    break;
                case States.GameStates.LevelTwo:
                    break;
                case States.GameStates.GameOver:
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
                case States.GameStates.Initalize:
                    break;
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
                    break;
                case States.GameStates.LevelOne:
                    _spriteBatch.Draw(gameBackgroundTexture, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
                    break;
                case States.GameStates.Paused:
                    break;
                case States.GameStates.LevelTwo:
                    break;
                case States.GameStates.GameOver:
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}