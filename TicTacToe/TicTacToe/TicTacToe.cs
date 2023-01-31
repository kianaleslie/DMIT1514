using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    public class TicTacToe : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D backgroundTexture;
        Texture2D xTexture;
        Texture2D oTexture;

        Rectangle oRectangle, xRectangle, backgroundRectangle;

        const int WINDOWWIDTH = 170;
        const int WINDOWHEIGHT = 170;

        public enum GameState
        {
            Initialize, 
            SwapTurn,
            ExecuteTurn,
            EvaluateBoard,
            GameEnd
        }
        GameState currrentGameState = GameState.Initialize;
        public enum MouseButtonStates
        {
            IsPressed,
            IsReleased,
            WasPressed,
            WasReleased
        }
        MouseButtonStates currentMouseState = MouseButtonStates.IsReleased;
        public enum Turn
        {
            XTurn,
            OTurn,
        }
        Turn currentTurn = Turn.XTurn;

        Rectangle[,] GameBoard = new Rectangle[3,3]; //set number of elements - using an array is easier 
        public TicTacToe()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
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
            backgroundTexture = Content.Load<Texture2D>("TicTacToeBoard");
            xTexture = Content.Load<Texture2D>("X");
            oTexture = Content.Load<Texture2D>("O");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (currrentGameState)
            {
                case GameState.Initialize:
                    break;
                case GameState.SwapTurn:
                    break;
                case GameState.ExecuteTurn:
                    break;
                case GameState.EvaluateBoard:
                    break;
                case GameState.GameEnd:
                    break;
                default:
                    break;
            }
            switch (currentMouseState)
            {
                case MouseButtonStates.IsPressed:
                    if (Mouse.GetState().LeftButton == ButtonState.Released)
                    {
                        currentMouseState = MouseButtonStates.WasReleased;
                    }
                    break;
                case MouseButtonStates.IsReleased:
                    if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        currentMouseState = MouseButtonStates.WasPressed;
                    }
                    break;
                default:
                    break;
            }
            currentMouseState = (MouseButtonStates)Mouse.GetState().LeftButton;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
//namespace
//{
//    public class TicTacToeGame : Game
//{
//    private GraphicsDeviceManager graphics;
//    private SpriteBatch spriteBatch;

//    Texture2D backgroundImage;
//    Texture2D xImage;
//    Texture2D oImage;

//    const int WindowWidth = 170;
//    const int WindowHeight = 170;

//    MouseState currentMouseState;
//    MouseState previousMouseState;

//    public enum GameSpaceState
//    {
//        Empty,
//        X,
//        O
//    }

//    GameSpaceState nextTokenToBePlayed;

//    public TicTacToeGame()
//    {
//        graphics = new GraphicsDeviceManager(this);
//        Content.RootDirectory = "Content";
//    }

//    protected override void Initialize()
//    {
//        graphics.PreferredBackBufferWidth = WindowWidth;
//        graphics.PreferredBackBufferHeight = WindowHeight;
//        graphics.ApplyChanges();

//        nextTokenToBePlayed = GameSpaceState.X;

//        base.Initialize();
//    }

//    protected override void LoadContent()
//    {
//        spriteBatch = new SpriteBatch(GraphicsDevice);
//        backgroundImage = Content.Load<Texture2D>("TicTacToeBoard");
//        xImage = Content.Load<Texture2D>("X");
//        oImage = Content.Load<Texture2D>("O");
//    }

//    protected override void Update(GameTime gameTime)
//    {
//        currentMouseState = Mouse.GetState();

//        if (previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
//        {
//            //there has been a mouse click (mouse release)
//            if (nextTokenToBePlayed == GameSpaceState.X)
//            {
//                nextTokenToBePlayed = GameSpaceState.O;
//            }
//            else if (nextTokenToBePlayed == GameSpaceState.O)
//            {
//                nextTokenToBePlayed = GameSpaceState.X;
//            }
//        }

//        previousMouseState = currentMouseState;
//        base.Update(gameTime);
//    }

//    protected override void Draw(GameTime gameTime)
//    {
//        GraphicsDevice.Clear(Color.CornflowerBlue);

//        Vector2 adjustedMousePosition =
//            new Vector2(currentMouseState.Position.X - (xImage.Width / 2),
//                currentMouseState.Position.Y - (xImage.Height / 2));

//        Texture2D imageToDraw = xImage;
//        if (nextTokenToBePlayed == GameSpaceState.O)
//        {
//            imageToDraw = oImage;
//        }
//        else if (nextTokenToBePlayed == GameSpaceState.X)
//        {
//            imageToDraw = xImage;
//        }

//        spriteBatch.Begin();
//        spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
//        spriteBatch.Draw(imageToDraw, adjustedMousePosition, Color.White);
//        spriteBatch.End();

//        base.Draw(gameTime);
//    }
//}
//}