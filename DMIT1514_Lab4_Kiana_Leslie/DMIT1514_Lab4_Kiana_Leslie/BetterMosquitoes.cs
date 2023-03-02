using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MosquitoAttack;

namespace DMIT1514_Lab4_Kiana_Leslie
{
    public class BetterMosquitoes : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WINDOWWIDTH = 800;
        const int WINDOWHEIGHT = 600;

        CelAnimationSequence mosquito;
        CelAnimationSequence cannon;
        CelAnimationSequence fireBall;
        CelAnimationSequence poof;

        CelAnimationPlayer animationPlayer;

        Texture2D bg;

        Texture2D playerTexture;
        //********************************************************************************************************************************************************
        Player player;
        Controls playerControls;
        GameObject testObject;
        Sprite testSprite;
        Transform testTransform;
        //********************************************************************************************************************************************************
        public BetterMosquitoes()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WINDOWWIDTH;
            _graphics.PreferredBackBufferHeight = WINDOWHEIGHT;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playerControls = new Controls(GamePad.GetState(0).DPad.Right == ButtonState.Pressed, GamePad.GetState(0).DPad.Left == ButtonState.Pressed, GamePad.GetState(0).Buttons.A == ButtonState.Pressed);
            //Keyboard.GetState().IsKeyDown(Keys.A)

            base.Initialize();
            //INTITALIZE rect or bounds below base.Initalize();

            //********************************************************************************************************************************************************
            player = new Player(testSprite, testTransform, playerControls);
            testSprite = new Sprite(playerTexture, playerTexture.Bounds, 1);
            testTransform = new Transform();
            testObject = new GameObject(testSprite, testTransform);
            //********************************************************************************************************************************************************
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bg = Content.Load<Texture2D>("Background");
            animationPlayer = new CelAnimationPlayer();

            Texture2D spriteSheet = Content.Load<Texture2D>("Mosquito");
            mosquito = new CelAnimationSequence(spriteSheet, 90, 1 / 8.0f);
            Texture2D spriteSheet1 = Content.Load<Texture2D>("Cannon");
            cannon = new CelAnimationSequence(spriteSheet1, 90, 1 / 8.0f);
            Texture2D spriteSheet2 = Content.Load<Texture2D>("FireBall");
            fireBall = new CelAnimationSequence(spriteSheet2, 90, 1 / 8.0f);
            Texture2D spriteSheet3 = Content.Load<Texture2D>("Poof");
            poof = new CelAnimationSequence(spriteSheet, 90, 1 / 8.0f);

            animationPlayer.Play(mosquito);
            animationPlayer.Play(cannon);
            animationPlayer.Play(fireBall);
            animationPlayer.Play(poof);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            animationPlayer.Update(gameTime);

            //********************************************************************************************************************************************************
            testObject.Update(gameTime);
            //********************************************************************************************************************************************************

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bg, new Rectangle(0, 0, WINDOWWIDTH, WINDOWHEIGHT), Color.White);
            animationPlayer.Draw(_spriteBatch, Vector2.Zero, SpriteEffects.None);

            //********************************************************************************************************************************************************
            testObject.Draw(_spriteBatch);
            //********************************************************************************************************************************************************
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}