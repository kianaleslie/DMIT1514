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
    public class Square
    {
        public enum SquareStates
        {
            Blank,
            X,
            O
        }
        public Rectangle Rectangle { get; private set; }
        public SquareStates CurrentSquareState { get; private set; }
        public Square(Rectangle rectangle)
        {
            Rectangle = rectangle;
            CurrentSquareState = SquareStates.Blank;
        }
        public void Reset()
        {
            CurrentSquareState = SquareStates.Blank;
        }
        public void SetState(SquareStates state)
        {
            CurrentSquareState = state;
        }
        public bool TrySetState(Point point, SquareStates state)
        {
            if (CurrentSquareState == SquareStates.Blank && Rectangle.Contains(point))
            {
                SetState(state);
                return true;
            }
            return false;
        }
    }
}