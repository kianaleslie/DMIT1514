using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    public class TicTacToe : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D boardTexture;
        Texture2D xTexture;
        Texture2D oTexture;
        Texture2D Blank;

        Rectangle oRectangle, xRectangle, boardRectangle;

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

        Rectangle[,] GameBoard = new /*Tile*/ Rectangle[3, 3]; 
        public TicTacToe()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //Blank = new Texture2D(GraphicsDevice, 50, 50);
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
            boardTexture = Content.Load<Texture2D>("TicTacToeBoard");
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
                    currentMouseState = MouseButtonStates.IsReleased;
                    for(int currentRow = 0; currentRow < 3; currentRow++)
                    {
                        for(int currentCol = 0; currentCol < 3; currentCol++)
                        {
                            GameBoard[currentCol, currentRow] = new Rectangle(new ((currentCol * 50) + (currentCol * 10), currentRow * 50), new (50,50));
                        }
                    }
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
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
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
            _spriteBatch.Draw(boardTexture, Vector2.Zero, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
//public class TicTacToeGame : Game
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

//    GameSpaceState[,] gameBoard =
//        new GameSpaceState[,]
//        {
//                {GameSpaceState.X, GameSpaceState.O, GameSpaceState.Empty},
//                {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},
//                {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.X}

//        };

//    public enum GameState
//    {
//        Initialize,
//        WaitForPlayerMove,
//        MakePlayerMove,
//        EvaluatePlayerMove,
//        GameOver
//    }
//    GameState currentGameState = GameState.Initialize;


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

//        switch (currentGameState)
//        {
//            case GameState.Initialize:
//                nextTokenToBePlayed = GameSpaceState.X;
//                /*
//                 * Set all of the game board spaces to "Empty".
//                 */

//                currentGameState = GameState.WaitForPlayerMove;
//                break;
//            case GameState.WaitForPlayerMove:
//                //there has been a mouse click (mouse release)
//                if (previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
//                {
//                    currentGameState = GameState.MakePlayerMove;
//                }
//                break;
//            case GameState.MakePlayerMove:
//                /*
//                 * 1. Figure out what game space of the array was clicked on
//                 * using currentMouseState.X and currentMouseState.Y to get the pixels of where the mouse is
//                 * 2. If they clicked on an empty game space, set that game space of the array to the nextTokenToBePlayed.
//                 * Then, go to EvaluatePlayerMove.
//                 * 3. If they clicked on a full slot, go back to WaitForPlayerMove
//                 */


//                currentGameState = GameState.EvaluatePlayerMove;
//                break;
//            case GameState.EvaluatePlayerMove:
//                /*
//                 * 1. figure out if someone won or tied. If so, go to GameOver state.
//                 * Otherwise, set nextTokenToBePlayed and go back to WaitForPlayerMove
//                 */

//                if (nextTokenToBePlayed == GameSpaceState.X)
//                {
//                    nextTokenToBePlayed = GameSpaceState.O;
//                }
//                else if (nextTokenToBePlayed == GameSpaceState.O)
//                {
//                    nextTokenToBePlayed = GameSpaceState.X;
//                }
//                currentGameState = GameState.WaitForPlayerMove;
//                break;
//            case GameState.GameOver:
//                /*
//                 * Display game over message. Wait for a click to restart the game. When game restarts, go to Initialize.
//                 */
//                break;
//        }

//        previousMouseState = currentMouseState;
//        base.Update(gameTime);
//    }

//    protected override void Draw(GameTime gameTime)
//    {
//        GraphicsDevice.Clear(Color.CornflowerBlue);

//        spriteBatch.Begin();

//        spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);

//        switch (currentGameState)
//        {
//            case GameState.Initialize:
//                break;
//            case GameState.WaitForPlayerMove:
//                Vector2 adjustedMousePosition = new Vector2(currentMouseState.Position.X - (xImage.Width / 2),
//                    currentMouseState.Position.Y - (xImage.Height / 2));

//                Texture2D imageToDraw = xImage;
//                if (nextTokenToBePlayed == GameSpaceState.O)
//                {
//                    imageToDraw = oImage;
//                }
//                else if (nextTokenToBePlayed == GameSpaceState.X)
//                {
//                    imageToDraw = xImage;
//                }
//                spriteBatch.Draw(imageToDraw, adjustedMousePosition, Color.White);
//                break;
//            case GameState.MakePlayerMove:
//                break;
//            case GameState.EvaluatePlayerMove:
//                break;
//            case GameState.GameOver:
//                break;
//        }

//        for (int row = 0; row < gameBoard.GetLength(0); row++)
//        {
//            for (int col = 0; col < gameBoard.GetLength(1); col++)
//            {
//                if (gameBoard[row, col] == GameSpaceState.X)
//                {
//                    spriteBatch.Draw(xImage, new Vector2(col * xImage.Width, row * xImage.Height), Color.White);
//                }
//                else if (gameBoard[row, col] == GameSpaceState.O)
//                {
//                    spriteBatch.Draw(oImage, new Vector2(col * oImage.Width, row * oImage.Height), Color.White);
//                }
//            }
//        }

//        spriteBatch.End();

//        base.Draw(gameTime);
//    }
//}