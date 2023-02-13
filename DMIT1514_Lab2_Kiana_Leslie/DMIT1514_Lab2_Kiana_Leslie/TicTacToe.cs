using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

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
        public Square symbol;
        public MouseStates currentState;
        public MouseStates lastState;
        public MouseState location;
        public GameState currentGameState = GameState.Initialize;
        public bool isTie = false;

        public TicTacToe()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
            _graphics.ApplyChanges();
            currentState = MouseStates.IsReleased;
            lastState = MouseStates.IsReleased;

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
                    currentGameState = GameState.TakeTurn;
                    foreach (Square tile in GameBoard)
                    {
                        tile.Reset();
                    }
                    break;
                case GameState.TakeTurn:
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
                            currentGameState = GameState.EvaluateBoard;
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
                case GameState.EvaluateBoard:
                    CheckWinOrTie checkWin = new CheckWinOrTie();
                    CheckWinOrTie checkTie = new CheckWinOrTie();
                    if (checkWin.HasWon((Square.SquareStates)(int)nextMove + 1, GameBoard))
                    {
                        currentGameState = GameState.GameOver;
                    }
                    else
                    {
                        if (checkTie.IsATie(GameBoard))
                        {
                            isTie = true;
                            currentGameState = GameState.GameOver;
                        }
                        else
                        {
                            currentGameState = GameState.TakeTurn;
                        }
                    }
                    break;
                case GameState.GameOver:
                    if (lastState == MouseStates.IsPressed && currentState == MouseStates.IsReleased)
                    {
                        currentGameState = GameState.Initialize;
                    }
                    break;
            }
            lastState = currentState;
            currentState = (MouseStates)Mouse.GetState().LeftButton;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            if (currentGameState != GameState.GameOver)
            {
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
            }
            switch (currentGameState)
            {
                case GameState.Initialize:
                    break;
                case GameState.TakeTurn:
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
                    GraphicsDevice.Clear(Color.Turquoise);
                    if (isTie)
                    {
                        Vector2 textCenter = font.MeasureString("It's a tie!") / 2f;
                        _spriteBatch.DrawString(font, "It's a tie!", new Vector2(80, 50), Color.MidnightBlue, 0, textCenter, 2.0f, SpriteEffects.None, 0);
                    }
                    else
                    if ((Square.SquareStates)(int)nextMove + 1 == Square.SquareStates.O)
                    {
                        Vector2 textCenter = font.MeasureString("O wins!") / 2f;
                        _spriteBatch.DrawString(font, "O wins!", new Vector2(80, 50), Color.MidnightBlue, 0, textCenter, 2.0f, SpriteEffects.None, 0);
                    }
                    else
                    if ((Square.SquareStates)(int)nextMove + 1 == Square.SquareStates.X)
                    {
                        Vector2 textCenter = font.MeasureString("X wins!") / 2f;
                        _spriteBatch.DrawString(font, "X wins!", new Vector2(80, 50), Color.MidnightBlue, 0, textCenter, 2.0f, SpriteEffects.None, 0);
                    }
                    Vector2 playAgain = font.MeasureString("Click to \nplay again!") / 2f;
                    _spriteBatch.DrawString(font, "Click to \nplay again!", new Vector2(85, 120), Color.White, 0, playAgain, 2.0f, SpriteEffects.None, 0);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}