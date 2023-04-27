using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    public class States
    {
        public enum CollectableState
        {
            Collectable,
            Colleted,
            NotCollectable
        }
        public enum PlayerState
        {
            Idle,
            Running,
            Jumping
        }
        public enum GameStates
        {
            //Initalize,
            Menu,
            LevelOne,
            Paused,
            //LevelTwo,
            GameOver
        }
    }
}