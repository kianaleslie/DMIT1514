using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMIT1514_Lab2_Kiana_Leslie
{
    public class CheckWinOrTie
    {
        public BoardState xWin;
        public BoardState oWin;
        public BoardState empty;

        public Square[,] Board = new Square[3, 3];

        public bool HasWon(Square symbol)
        {
            return IsHorizontalVictory(symbol) || IsVerticalVictory(symbol) || IsDiagonalVictory(symbol);
        }

        private bool IsHorizontalVictory(Square symbol)
        {
            for (int y = 0; y <= 2; y++)
            {
                if (Board[0, y] == symbol && Board[1, y] == symbol && Board[2, y] == symbol)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsVerticalVictory(Square symbol)
        {
            for (int x = 0; x <= 2; x++)
            {
                if (Board[x, 0] == symbol && Board[x, 1] == symbol && Board[x, 2] == symbol)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsDiagonalVictory(Square symbol)
        {
            if (Board[0, 0] == symbol && Board[1, 1] == symbol && Board[2, 2] == symbol)
            {
                return true;
            }
            if (Board[0, 2] == symbol && Board[1, 1] == symbol && Board[2, 0] == symbol)
            {
                return true;
            }
            return false;
        }
    }
}