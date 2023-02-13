using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMIT1514_Lab2_Kiana_Leslie
{
    public class TicTacToe : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WINDOWWIDTH = 170;
        const int WINDOWHEIGHT = 170;

        Texture2D boardTexture;
        Texture2D xTexture;
        Texture2D oTexture;

        SpriteFont font;
        Square[,] GameBoard = new Square[3, 3];

        public BoardState nextMove;
        public MouseStates currentState;
        public MouseStates lastState;
        public MouseState location;
        public GameState currentGameState = GameState.Initialize;

        public TicTacToe()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //window fits game board
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;

            _graphics.ApplyChanges();

            //keep track of mouse state 
            currentState = MouseStates.IsReleased;
            lastState = MouseStates.IsReleased;

            //intialize the board
            nextMove = BoardState.X;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    GameBoard[col, row] =
                    new Square(new Rectangle(new Point((col * 50) + (col * 4), (row * 50) + (row * 4)), new(50, 50)));
                }
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            boardTexture = Content.Load<Texture2D>("TicTacToeBoard");
            xTexture = Content.Load<Texture2D>("X");
            oTexture = Content.Load<Texture2D>("O");
            font = Content.Load<SpriteFont>("FredokaOne-Regular");
        }

        protected override void Update(GameTime gameTime)
        {
            location = Mouse.GetState();
            switch (currentGameState)
            {
                case GameState.Initialize:
                    currentState = MouseStates.IsReleased;
                    currentGameState = GameState.WaitForMove;
                    foreach (Square tile in GameBoard)
                    {
                        tile.Reset();
                    }
                    break;
                case GameState.WaitForMove:
                    if (lastState == MouseStates.IsPressed && currentState == MouseStates.IsReleased)
                    {
                        currentGameState = GameState.MakeMove;
                    }
                    break;
                case GameState.MakeMove:
                    currentGameState = GameState.EvaluateMove;
                    break;
                case GameState.EvaluateMove:
                    foreach (Square tile in GameBoard)
                    {
                        if (tile.TrySetState(location.Position, (Square.SquareStates)(int)nextMove))
                        {
                            currentGameState = GameState.WaitForMove;
                        }
                    }
                    if (nextMove == BoardState.X)
                    {
                        nextMove = BoardState.O;
                    }
                    else if (nextMove == BoardState.O)
                    {
                        nextMove = BoardState.X;
                    }
                    break;
                case GameState.GameOver:
                    break;
            }
            lastState = currentState;
            currentState = (MouseStates)Mouse.GetState().LeftButton;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(boardTexture, Vector2.Zero, Color.White);
            foreach (Square tile in GameBoard)
            {
                Texture2D texture2D = null;
                if (tile.CurrentSquareState == Square.SquareStates.X)
                {
                    texture2D = xTexture;
                }
                else
                {
                    if (tile.CurrentSquareState == Square.SquareStates.O)
                    {
                        texture2D = oTexture;
                    }
                }
                if (texture2D != null)
                {
                    _spriteBatch.Draw(texture2D, tile.Rectangle, Color.White);
                }
            }
            switch (currentGameState)
            {
                case GameState.Initialize:
                    break;
                case GameState.WaitForMove:
                    Vector2 newPosition = new Vector2(location.Position.X - (xTexture.Width / 2), location.Y - (xTexture.Height / 2));
                    Texture2D imageToDraw = xTexture;
                    if (nextMove == BoardState.O)
                    {
                        imageToDraw = oTexture;
                    }
                    else if (nextMove == BoardState.X)
                    {
                        imageToDraw = xTexture;
                    }
                    _spriteBatch.Draw(imageToDraw, newPosition, Color.White);
                    break;
                case GameState.MakeMove:
                    break;
                case GameState.EvaluateMove:
                    break;
                case GameState.GameOver:
                    if (currentGameState == GameState.GameOver)
                    {
                        //Vector2 textCenter = font.MeasureString("Win!") / 2f;
                        //_spriteBatch.DrawString(font, "Win!", new Vector2(75, 75), Color.White, 0, textCenter, 2.0f, SpriteEffects.None, 0);
                    }
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}