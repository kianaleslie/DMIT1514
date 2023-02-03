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
    internal class Tile
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
    }
}
