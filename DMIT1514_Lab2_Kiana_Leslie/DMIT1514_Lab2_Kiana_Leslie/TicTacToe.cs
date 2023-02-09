﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMIT1514_Lab2_Kiana_Leslie
{
    public class TicTacToe : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D backgroundImage;
        Texture2D xImage;
        Texture2D oImage;

        SpriteFont arial;

        MouseState currentMouseState;
        MouseState previousMouseState;

        const int WindowWidth = 800;
        const int WindowHeight = 600;

        public enum GameSpaceState
        {
            Empty,
            X,
            O
        }
        GameSpaceState nextTokenToBePlayed;

        public TicTacToe()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();

            nextTokenToBePlayed = GameSpaceState.O;

            //to prevent the bug that the user is holding down the mouse button
            //as they start game
            previousMouseState = Mouse.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundImage = Content.Load<Texture2D>("TicTacToeBoard");
            xImage = Content.Load<Texture2D>("X");
            oImage = Content.Load<Texture2D>("O");

            arial = Content.Load<SpriteFont>("SystemArialFont");
        }

        protected override void Update(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();
            //if the button is currently down, but it wasn't down the last call to update
            if (currentMouseState.LeftButton == ButtonState.Pressed
                && previousMouseState.LeftButton == ButtonState.Released)
            {
                if (nextTokenToBePlayed == GameSpaceState.X)
                {
                    nextTokenToBePlayed = GameSpaceState.O;
                }
                else
                {
                    nextTokenToBePlayed = GameSpaceState.X;
                }
            }
            previousMouseState = currentMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Vector2 adjustedMousePosition
                = new Vector2(currentMouseState.X - (xImage.Width / 2),
                                currentMouseState.Y - (oImage.Width / 2));

            Texture2D imageToDraw = xImage;
            if (nextTokenToBePlayed == GameSpaceState.O)
            {
                imageToDraw = oImage;
            }

            spriteBatch.Begin();


            int[,] numArray =
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };

            for (int row = numArray.GetLength(0) - 1; row >= 0; row--)
            {
                for (int column = numArray.GetLength(1) - 1; column >= 0; column--)
                {
                    string message = numArray[row, column] + "";

                    int xValue = (numArray.GetLength(1) - 1 - column) * 30;
                    int yValue = (numArray.GetLength(0) - 1 - row) * 20;

                    spriteBatch.DrawString(arial, message, new Vector2(xValue, yValue), Color.Black);
                }
            }

            //spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
            spriteBatch.Draw(imageToDraw, adjustedMousePosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

//    public class TicTacToe : Game
//    {
//        private GraphicsDeviceManager _graphics;
//        private SpriteBatch _spriteBatch;

//        Texture2D boardTexture;
//        Texture2D xTexture;
//        Texture2D oTexture;

//        SpriteFont font;

//        const int WINDOWWIDTH = 170;
//        const int WINDOWHEIGHT = 170;
//        public enum BoardState
//        {
//            Blank,
//            X,
//            O
//        }
//        BoardState nextMove;

//        Tile[,] GameBoard = new Tile[3, 3];
//        public enum MouseStates
//        {
//            IsPressed,
//            IsReleased,
//            WasPressed,
//            WasReleased
//        }
//        MouseStates currentState;
//        MouseStates lastState;
//        MouseState position;
//        public enum GameState
//        {
//            Initialize,
//            WaitForPlayerMove,
//            MakePlayerMove,
//            EvaluatePlayerMove,
//            GameOver
//        }
//        GameState currentGameState = GameState.Initialize;
//        public TicTacToe()
//        {
//            _graphics = new GraphicsDeviceManager(this);
//            Content.RootDirectory = "Content";
//        }

//        protected override void Initialize()
//        {
//            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
//            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
//            _graphics.ApplyChanges();
//            currentState = MouseStates.IsReleased;
//            lastState = MouseStates.IsReleased;

//            nextMove = BoardState.X;
//            for (int row = 0; row < 3; row++)
//            {
//                for (int col = 0; col < 3; col++)
//                {
//                    GameBoard[col, row] =
//                    new Tile(new Rectangle(new Point((col * 50) + (col * 2), (row * 50) + (row * 2)), new(50, 50)));
//                }
//            }
//            base.Initialize();
//        }

//        protected override void LoadContent()
//        {
//            _spriteBatch = new SpriteBatch(GraphicsDevice);
//            boardTexture = Content.Load<Texture2D>("TicTacToeBoard");
//            xTexture = Content.Load<Texture2D>("X");
//            oTexture = Content.Load<Texture2D>("O");
//            font = Content.Load<SpriteFont>("FredokaOne-Regular");
//        }

//        protected override void Update(GameTime gameTime)
//        {
//            position = Mouse.GetState();
//            switch (currentGameState)
//            {
//                case GameState.Initialize:
//                    currentState = MouseStates.IsReleased;
//                    currentGameState = GameState.WaitForPlayerMove;
//                    foreach (Tile tile in GameBoard)
//                    {
//                        tile.Reset();
//                    }
//                    break;
//                case GameState.WaitForPlayerMove:
//                    if (lastState == MouseStates.IsPressed && currentState == MouseStates.IsReleased)
//                    {
//                        currentGameState = GameState.MakePlayerMove;
//                    }
//                    break;
//                case GameState.MakePlayerMove:
//                    currentGameState = GameState.EvaluatePlayerMove;
//                    break;
//                case GameState.EvaluatePlayerMove:
//                    foreach (Tile tile in GameBoard)
//                    {
//                        if (tile.TrySetState(position.Position, (Tile.TileStates)(int)nextMove))
//                        {
//                            currentGameState = GameState.WaitForPlayerMove;
//                        }
//                    }
//                    if (nextMove == BoardState.X)
//                    {
//                        nextMove = BoardState.O;
//                    }
//                    else if (nextMove == BoardState.O)
//                    {
//                        nextMove = BoardState.X;
//                    }
//                    break;
//                case GameState.GameOver:
//                    break;
//            }
//            lastState = currentState;
//            currentState = (MouseStates)Mouse.GetState().LeftButton;
//            base.Update(gameTime);
//        }

//        protected override void Draw(GameTime gameTime)
//        {
//            GraphicsDevice.Clear(Color.CornflowerBlue);

//            _spriteBatch.Begin();

//            _spriteBatch.Draw(boardTexture, Vector2.Zero, Color.White);
//            foreach (Tile tile in GameBoard)
//            {
//                Texture2D texture2D = null;
//                if (tile.TileState == Tile.TileStates.X)
//                {
//                    texture2D = xTexture;
//                }
//                else
//                {
//                    if (tile.TileState == Tile.TileStates.O)
//                    {
//                        texture2D = oTexture;
//                    }
//                }
//                if (texture2D != null)
//                {
//                    _spriteBatch.Draw(texture2D, tile.Rectangle, Color.White);
//                }
//            }
//            switch (currentGameState)
//            {
//                case GameState.Initialize:
//                    break;
//                case GameState.WaitForPlayerMove:
//                    Vector2 newPosition = new Vector2(position.Position.X - (xTexture.Width / 2), position.Y - (xTexture.Height / 2));
//                    Texture2D imageToDraw = xTexture;
//                    if (nextMove == BoardState.O)
//                    {
//                        imageToDraw = oTexture;
//                    }
//                    else if (nextMove == BoardState.X)
//                    {
//                        imageToDraw = xTexture;
//                    }
//                    _spriteBatch.Draw(imageToDraw, newPosition, Color.White);
//                    break;
//                case GameState.MakePlayerMove:
//                    break;
//                case GameState.EvaluatePlayerMove:
//                    break;
//                case GameState.GameOver:
//                        if(currentGameState == GameState.GameOver)
//                    {
//                        Vector2 textCenter = font.MeasureString("Win!") / 2f;
//                        _spriteBatch.DrawString(font, "Win!", new Vector2(75, 75), Color.White, 0, textCenter, 2.0f, SpriteEffects.None, 0);
//                    }
//                    break;
//            }
//            _spriteBatch.End();
//            base.Draw(gameTime);
//        }
//    }
//}