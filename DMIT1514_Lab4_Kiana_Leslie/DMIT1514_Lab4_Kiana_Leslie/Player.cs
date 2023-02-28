using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DMIT1514_Lab4_Kiana_Leslie
{
    public class Player
    {
        Texture2D playerTexture;
        Rectangle playerRectangle;
        Controls playerControls;
        //projectiles
        //ref to gameObject
        Vector2 playerPosition;
        Vector2 playerDirection;

        float playerSpeed;
        int currentPlayerHealth;
        int maxplayerHealth;

        int currentPlayerAmmo;
        int maxPlayerAmmo;
        float fireRate;

        PlayerState currentPlayerState = PlayerState.Alive;

        public Player(Texture2D texture, Vector2 position, Controls controls)
        {
            playerControls = controls;
            playerTexture = texture;
            playerPosition = position;
        }
        public void Update(GameTime gameTime)
        {
            switch(currentPlayerState)
            {
                case PlayerState.Alive:
                    PlayerMove();
                    PlayerFire();
                    break;
                case PlayerState.Dying:
                    break;
                case PlayerState.Dead:
                    break;
            }
        }
        public void PlayerMove()
        {
            if (playerControls.positiveDirection)
            {
                //move right
            }
            if (playerControls.negativeDirection)
            {
                //move left
            }
        }
        public void PlayerFire()
        {
            if (playerControls.wasFirePressedThisFrame)
            {
                //fire bullet
            }
        }

    }
}
public struct Controls
{
    public Controls(bool positive, bool negative, bool fire)
    {
        positiveDirection = positive;
        negativeDirection = negative;
        wasFirePressedThisFrame = fire;
    }
    public bool positiveDirection;
    public bool negativeDirection;
    public bool wasFirePressedThisFrame;
}
public enum PlayerUpgradeState
{
    None
}
public enum PlayerState
{
    Alive,
    Dying,
    Dead
    //can add more but we NEED these 3 ^
}