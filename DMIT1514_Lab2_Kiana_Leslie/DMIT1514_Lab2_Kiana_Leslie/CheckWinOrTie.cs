using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMIT1514_Lab2_Kiana_Leslie
{
    public class CheckWinOrTie
    {
        public bool HasWon(Square.SquareStates symbol, Square[,] Board)
        {
            return IsHorizontalVictory(symbol, Board) || IsVerticalVictory(symbol, Board) || IsDiagonalVictory(symbol, Board);
        }
        public bool IsATie(Square[,] Board)
        {
            return IsTie(Board);
        }

        private bool IsHorizontalVictory(Square.SquareStates symbol, Square[,] Board)
        {
            for (int y = 0; y <= 2; y++)
            {
                if (Board[0, y].CurrentSquareState == symbol && Board[1, y].CurrentSquareState == symbol && Board[2, y].CurrentSquareState == symbol)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsVerticalVictory(Square.SquareStates symbol, Square[,] Board)
        {
            for (int x = 0; x <= 2; x++)
            {
                if (Board[x, 0].CurrentSquareState == symbol && Board[x, 1].CurrentSquareState == symbol && Board[x, 2].CurrentSquareState == symbol)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsDiagonalVictory(Square.SquareStates symbol, Square[,] Board)
        {
            if (Board[0, 0].CurrentSquareState == symbol && Board[1, 1].CurrentSquareState == symbol && Board[2, 2].CurrentSquareState == symbol)
            {
                return true;
            }
            if (Board[0, 2].CurrentSquareState == symbol && Board[1, 1].CurrentSquareState == symbol && Board[2, 0].CurrentSquareState == symbol)
            {
                return true;
            }
            return false;
        }
        private bool IsTie(Square[,] Board)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (Board[y,x].CurrentSquareState == Square.SquareStates.Blank)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}