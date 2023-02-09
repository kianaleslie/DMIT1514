using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMIT1514_Lab2_Kiana_Leslie
{
    public class TicTacToe : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D boardTexture;
        Texture2D xTexture;
        Texture2D oTexture;

        SpriteFont font;

        const int WINDOWWIDTH = 170;
        const int WINDOWHEIGHT = 170;
        const int ENDWIDTH = 800;
        const int ENDHEIGHT = 480;
        public enum BoardState
        {
            Blank,
            X,
            O
        }
        BoardState nextMove;

        Tile[,] GameBoard = new Tile[3, 3];
        public enum MouseStates
        {
            IsPressed,
            IsReleased,
            WasPressed,
            WasReleased
        }
        MouseStates currentState;
        MouseStates lastState;
        MouseState position;
        public enum GameState
        {
            Initialize,
            WaitForPlayerMove,
            MakePlayerMove,
            EvaluatePlayerMove,
            GameOver
        }
        GameState currentGameState = GameState.Initialize;
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
                    new Tile(new Rectangle(new Point((col * 50) + (col * 4), (row * 50) + (row * 4)), new(50, 50)));
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
            font = Content.Load<SpriteFont>("FredokaOneRegular");
        }
        protected override void Update(GameTime gameTime)
        {
            position = Mouse.GetState();
            switch (currentGameState)
            {
                case GameState.Initialize:
                    currentState = MouseStates.IsReleased;
                    currentGameState = GameState.WaitForPlayerMove;
                    foreach (Tile tile in GameBoard)
                    {
                        tile.Reset();
                    }
                    break;
                case GameState.WaitForPlayerMove:
                    if (lastState == MouseStates.IsPressed && currentState == MouseStates.IsReleased)
                    {
                        currentGameState = GameState.MakePlayerMove;
                    }
                    break;
                case GameState.MakePlayerMove:
                    currentGameState = GameState.EvaluatePlayerMove;
                    break;
                case GameState.EvaluatePlayerMove:
                    foreach (Tile tile in GameBoard)
                    {
                        if (tile.TrySetState(position.Position, (Tile.TileStates)(int)nextMove))
                        {
                            currentGameState = GameState.WaitForPlayerMove;
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
            _spriteBatch.Begin();

            _spriteBatch.Draw(boardTexture, Vector2.Zero, Color.Aquamarine);
            foreach (Tile tile in GameBoard)
            {
                Texture2D texture2D = null;
                if (tile.TileState == Tile.TileStates.X)
                {
                    texture2D = xTexture;
                }
                else
                {
                    if (tile.TileState == Tile.TileStates.O)
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
                case GameState.WaitForPlayerMove:
                    Vector2 newPosition = new Vector2(position.Position.X - (xTexture.Width / 2), position.Y - (xTexture.Height / 2));
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
                case GameState.MakePlayerMove:
                    break;
                case GameState.EvaluatePlayerMove:
                    break;
                case GameState.GameOver:
                    if (currentGameState == GameState.GameOver)
                    {
                        GraphicsDevice.Clear(Color.AliceBlue);
                        _graphics.PreferredBackBufferWidth = ENDWIDTH;
                        _graphics.PreferredBackBufferHeight = ENDHEIGHT;
                        Vector2 textCenter = font.MeasureString("Win!") / 2f;
                        _spriteBatch.DrawString(font, "Win!", new Vector2(75, 75), Color.White, 0, textCenter, 2.0f, SpriteEffects.None, 0);
                    }
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}