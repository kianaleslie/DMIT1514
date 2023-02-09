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
        Texture2D Blank;

        Rectangle oRectangle, xRectangle, boardRectangle;

        const int WINDOWWIDTH = 170;
        const int WINDOWHEIGHT = 170;
        public enum GameSpaceState
        {
            Empty,
            X,
            O
        }
        GameSpaceState nextTokenToBePlayed;

        Tile[,] GameBoard =
            new Tile[3, 3];
        //{
        //    {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},
        //    {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},
        //    {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty}

        //};

        public enum MouseButtonStates
        {
            IsPressed,
            IsReleased,
            WasPressed,
            WasReleased
        }
        MouseButtonStates currentMouseState;
        MouseButtonStates previousMouseState;
        public enum GameState
        {
            Initialize,
            WaitForPlayerMove,
            MakePlayerMove,
            EvaluatePlayerMove,
            GameOver
        }
        GameState currentGameState = GameState.Initialize;

        MouseState position;


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
            currentMouseState = MouseButtonStates.IsReleased;
            previousMouseState = MouseButtonStates.IsReleased;

            nextTokenToBePlayed = GameSpaceState.X;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    GameBoard[col, row] =
                    new Tile(new Rectangle(new Point((col * 50) + (col * 2), (row * 50) + (row * 2)), new(50, 50)));
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
        }

        protected override void Update(GameTime gameTime)
        {
            position = Mouse.GetState();
            switch (currentGameState)
            {
                case GameState.Initialize:

                    currentMouseState = MouseButtonStates.IsReleased;
                    currentGameState = GameState.WaitForPlayerMove;
                    foreach (Tile tile in GameBoard)
                    {
                        tile.Reset();
                    }
                    break;
                case GameState.WaitForPlayerMove:
                    if (previousMouseState == MouseButtonStates.IsPressed && currentMouseState == MouseButtonStates.IsReleased)
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
                        if (tile.TrySetState(position.Position, (Tile.TileStates)(int)nextTokenToBePlayed))
                        {
                            currentGameState = GameState.WaitForPlayerMove;
                        }
                    }

                    if (nextTokenToBePlayed == GameSpaceState.X)
                    {
                        nextTokenToBePlayed = GameSpaceState.O;
                    }
                    else if (nextTokenToBePlayed == GameSpaceState.O)
                    {
                        nextTokenToBePlayed = GameSpaceState.X;
                    }
                    break;
                case GameState.GameOver:
                    break;
            }
            previousMouseState = currentMouseState;
            currentMouseState = (MouseButtonStates)Mouse.GetState().LeftButton;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(boardTexture, Vector2.Zero, Color.White);
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
                    Vector2 adjustedMousePosition = new Vector2(position.Position.X - (xTexture.Width / 2),
                       position.Y - (xTexture.Height / 2));

                    Texture2D imageToDraw = xTexture;
                    if (nextTokenToBePlayed == GameSpaceState.O)
                    {
                        imageToDraw = oTexture;
                    }
                    else if (nextTokenToBePlayed == GameSpaceState.X)
                    {
                        imageToDraw = xTexture;
                    }
                    _spriteBatch.Draw(imageToDraw, adjustedMousePosition, Color.White);
                    break;
                case GameState.MakePlayerMove:
                    break;
                case GameState.EvaluatePlayerMove:
                    break;
                case GameState.GameOver:
                    break;
            }

            //for (int row = 0; row < GameBoard.GetLength(0); row++)
            //{
            //    for (int col = 0; col < GameBoard.GetLength(1); col++)
            //    {
            //        if (GameBoard[row, col] == GameSpaceState.X)
            //        {
            //            spriteBatch.Draw(xImage, new Vector2(col * xImage.Width, row * xImage.Height), Color.White);
            //        }
            //        else if (GameBoard[row, col] == GameSpaceState.O)
            //        {
            //            spriteBatch.Draw(oImage, new Vector2(col * oImage.Width, row * oImage.Height), Color.White);
            //        }
            //    }
            //}

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}