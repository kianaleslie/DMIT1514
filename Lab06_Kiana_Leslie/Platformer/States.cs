
namespace Platformer
{
    public class States
    {
        public enum CollectableState
        {
            Collectable,
            Collected,
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