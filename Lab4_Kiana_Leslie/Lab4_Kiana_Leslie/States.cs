using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab4_Kiana_Leslie
{
    public class States
    {
        public enum ProjectileState
        {
            Flying,
            NotFlying
        }
        public enum PlayerState
        {
            Alive,
            Dying,
            Dead
        }
        public enum GameStates
        {
            Menu,
            LevelOne,
            Paused,
            LevelTwo,
            GameOver
        }
    }
}