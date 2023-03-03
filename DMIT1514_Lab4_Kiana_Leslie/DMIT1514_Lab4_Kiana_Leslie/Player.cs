using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DMIT1514_Lab4_Kiana_Leslie
{
    public class Player : GameObject
    {
        public Transform transform;
        public Sprite sprite;
        Controls playerControls;

        //Texture2D playerTexture;
        //Rectangle playerRectangle;
        //Controls playerControls;
        ////projectiles
        ////ref to gameObject
        //Vector2 playerPosition;
        //Vector2 playerDirection;

        float playerSpeed = 4f;
        int currentPlayerHealth;
        int maxplayerHealth;

        int currentPlayerAmmo;
        int maxPlayerAmmo;
        float fireRate;

        bool leftPressed;
        bool rightPressed;
        bool firePressed;

        PlayerState currentPlayerState = PlayerState.Alive;

        //public Player(Texture2D texture, Vector2 position, Controls controls)
        //{
        //    playerControls = controls;
        //    playerTexture = texture;
        //    playerPosition = position;
        //}
        public Player(Sprite sprite, Transform transform, Controls controls) : base(sprite, transform)
        {
            this.sprite = sprite;
            this.transform = transform;
            playerControls = controls;
        }
        public void Update(GameTime gameTime)
        {
            switch (currentPlayerState)
            {
                case PlayerState.Alive:
                    PlayerInput(playerControls);
                    PlayerMove();
                    PlayerFire();
                    break;
                case PlayerState.Dying:
                    break;
                case PlayerState.Dead:
                    break;
            }
        }
        
        private void PlayerInput(Controls controls)
        {
            rightPressed = Keyboard.GetState().IsKeyDown(controls.positiveDirection);
            leftPressed = Keyboard.GetState().IsKeyDown(controls.negativeDirection);
            firePressed = Keyboard.GetState().IsKeyDown(controls.wasFirePressedThisFrame);
        }

        public void PlayerMove() //implements what any gameObject can do - MOVE
        {
            if (leftPressed)
            {
                transform.Direction = new Vector2(-1, 0);
            }
            else
            {
                if (rightPressed)
                {
                    transform.Direction = new Vector2(1, 0);
                }
                else
                {
                    transform.Direction = Vector2.Zero;
                }
            }
            Move(transform.Direction * playerSpeed);
            //if (playerControls.positiveDirection == playerControls.negativeDirection)
            //{
            //    transform.Direction = new Vector2();
            //}
            //else
            //{
            //    if (playerControls.positiveDirection)
            //    {

            //    }
            //    if (playerControls.negativeDirection)
            //    {

            //    }
            //}
            //Move(transform.Direction * playerSpeed);
        }
        public void PlayerFire()
        {
            if (firePressed)
            {
                //fire bullet
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.SpriteSheet, transform.Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }
    }
}
public struct Controls
{
    public /*bool*/ Keys positiveDirection;
    public /*bool*/ Keys negativeDirection;
    public /*bool*/ Keys wasFirePressedThisFrame;
    public Controls(Keys positive, Keys negative, Keys fire)
    {
        positiveDirection = positive;
        negativeDirection = negative;
        wasFirePressedThisFrame = fire;
    }
    //public Controls(GamePad positive, bool negative, bool fire)
    //{
    //    positiveDirection = positive;
    //    negativeDirection = negative;
    //    wasFirePressedThisFrame = fire;
    //}
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