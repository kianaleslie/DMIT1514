using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMIT1514_Lab2_Kiana_Leslie
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
