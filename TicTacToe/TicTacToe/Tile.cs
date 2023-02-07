using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TicTacToe
{
    public class Tile
    {
        public enum TileStates
        {
            Blank,
            X,
            O
        }
        public Rectangle Rectangle { get; private set; }
        public TileStates TileState { get; private set; }
        public Tile(Rectangle rectangle)
        {
            Rectangle = rectangle;
            TileState = TileStates.Blank;
        }
        public Tile(Rectangle rectangle, TileStates tileState)
        {
            Rectangle = rectangle;
            TileState = tileState;
        }
        public void Reset()
        {
            TileState = TileStates.Blank;
        }
        public void SetState(TileStates state)
        {
            TileState = state;
        }
        public bool TrySetState(Point point, TileStates state)
        {
            if (TileState == TileStates.Blank && Rectangle.Contains(point))
            {
                SetState(state);
                return true;
            }
            return false;
        }
    }
}
//Game1 class:


//{
//    public class Game1 : Game
//{
//    private GraphicsDeviceManager graphics;
//    private SpriteBatch spriteBatch;

//    Texture2D backgroundImage;
//    Texture2D xImage;
//    Texture2D oImage;

//    const int WindowWidth = 170;
//    const int WindowHeight = 170;

//    public enum GameSpaceState
//    {
//        Empty,
//        X,
//        O
//    }
//    GameSpaceState nextTokenToBePlayed;

//    Tile[,] GameBoard =
//        new Tile[3, 3];
//    //{
//    //    {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},
//    //    {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty},
//    //    {GameSpaceState.Empty, GameSpaceState.Empty, GameSpaceState.Empty}

//    //};

//    public enum MouseButtonStates
//    {
//        IsPressed,
//        IsReleased,
//        WasPressed,
//        WasReleased
//    }
//    MouseButtonStates currentMouseState;
//    MouseButtonStates previousMouseState;
//    public enum GameState
//    {
//        Initialize,
//        WaitForPlayerMove,
//        MakePlayerMove,
//        EvaluatePlayerMove,
//        GameOver
//    }
//    GameState currentGameState = GameState.Initialize;

//    MouseState position;


//    public Game1()
//    {
//        graphics = new GraphicsDeviceManager(this);
//        Content.RootDirectory = "Content";
//    }

//    protected override void Initialize()
//    {
//        graphics.PreferredBackBufferWidth = WindowWidth;
//        graphics.PreferredBackBufferHeight = WindowHeight;
//        graphics.ApplyChanges();
//        currentMouseState = MouseButtonStates.IsReleased;
//        previousMouseState = MouseButtonStates.IsReleased;

//        nextTokenToBePlayed = GameSpaceState.X;
//        for (int row = 0; row < 3; row++)
//        {
//            for (int col = 0; col < 3; col++)
//            {
//                GameBoard[col, row] =
//                new Tile(new Rectangle(new Point((col * 50) + (col * 2), (row * 50) + (row * 2)), new(50, 50)));
//            }
//        }
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
//        position = Mouse.GetState();
//        switch (currentGameState)
//        {
//            case GameState.Initialize:

//                currentMouseState = MouseButtonStates.IsReleased;
//                currentGameState = GameState.WaitForPlayerMove;
//                foreach (Tile tile in GameBoard)
//                {
//                    tile.Reset();
//                }
//                break;
//            case GameState.WaitForPlayerMove:
//                if (previousMouseState == MouseButtonStates.IsPressed && currentMouseState == MouseButtonStates.IsReleased)
//                {
//                    currentGameState = GameState.MakePlayerMove;
//                }
//                break;
//            case GameState.MakePlayerMove:

//                currentGameState = GameState.EvaluatePlayerMove;
//                break;
//            case GameState.EvaluatePlayerMove:

//                foreach (Tile tile in GameBoard)
//                {
//                    if (tile.TrySetState(position.Position, (Tile.TileStates)(int)nextTokenToBePlayed))
//                    {
//                        currentGameState = GameState.WaitForPlayerMove;
//                    }
//                }

//                if (nextTokenToBePlayed == GameSpaceState.X)
//                {
//                    nextTokenToBePlayed = GameSpaceState.O;
//                }
//                else if (nextTokenToBePlayed == GameSpaceState.O)
//                {
//                    nextTokenToBePlayed = GameSpaceState.X;
//                }
//                break;
//            case GameState.GameOver:
//                break;
//        }
//        previousMouseState = currentMouseState;
//        currentMouseState = (MouseButtonStates)Mouse.GetState().LeftButton;
//        base.Update(gameTime);
//    }

//    protected override void Draw(GameTime gameTime)
//    {
//        GraphicsDevice.Clear(Color.CornflowerBlue);

//        spriteBatch.Begin();

//        spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
//        foreach (Tile tile in GameBoard)
//        {
//            Texture2D texture2D = null;
//            if (tile.TileState == Tile.TileStates.X)
//            {
//                texture2D = xImage;
//            }
//            else
//            {
//                if (tile.TileState == Tile.TileStates.O)
//                {
//                    texture2D = oImage;
//                }
//            }
//            if (texture2D != null)
//            {
//                spriteBatch.Draw(texture2D, tile.Rectangle, Color.White);
//            }
//        }
//        switch (currentGameState)
//        {
//            case GameState.Initialize:
//                break;
//            case GameState.WaitForPlayerMove:
//                Vector2 adjustedMousePosition = new Vector2(position.Position.X - (xImage.Width / 2),
//                   position.Y - (xImage.Height / 2));

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

//        //for (int row = 0; row < GameBoard.GetLength(0); row++)
//        //{
//        //    for (int col = 0; col < GameBoard.GetLength(1); col++)
//        //    {
//        //        if (GameBoard[row, col] == GameSpaceState.X)
//        //        {
//        //            spriteBatch.Draw(xImage, new Vector2(col * xImage.Width, row * xImage.Height), Color.White);
//        //        }
//        //        else if (GameBoard[row, col] == GameSpaceState.O)
//        //        {
//        //            spriteBatch.Draw(oImage, new Vector2(col * oImage.Width, row * oImage.Height), Color.White);
//        //        }
//        //    }
//        //}

//        spriteBatch.End();

//        base.Draw(gameTime);
//    }
//}
//}