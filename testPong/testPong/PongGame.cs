using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;

namespace testPong
{
    public class PongGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        const int Scale = 4;
        const int WindowWidth = 250 * Scale;
        const int PlayAreaHeight = 150 * Scale;

        const int PaddleLine = 215 * Scale;
        const int PlayAreaEdgeLineWidth = 4 * Scale;

        Rectangle playAreaBoundingBox2D;
        Texture2D backgroundTexture;

        HUD hud;
        Ball ball;
        Paddle paddle;

        public PongGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            hud = new HUD();
            hud.Initialize(Scale, new Vector2(0, PlayAreaHeight));

            playAreaBoundingBox2D = new Rectangle(0, 0 + PlayAreaEdgeLineWidth, WindowWidth, PlayAreaHeight - PlayAreaEdgeLineWidth * 2);

            ball = new Ball();
            ball.Initialize(Scale, playAreaBoundingBox2D.Center.ToVector2(), playAreaBoundingBox2D, new Vector2(1, -1));

            paddle = new Paddle();
            paddle.Initialize(Scale, new Vector2(PaddleLine, playAreaBoundingBox2D.Center.Y), playAreaBoundingBox2D);

            //this calls LoadContent(), so make sure that all objects in LoadContent are instantiated before calling base.Initialize()
            base.Initialize();

            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = PlayAreaHeight + hud.Height;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("Court");
            ball.LoadContent(Content);
            paddle.LoadContent(Content);
            hud.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.Up))
            {
                paddle.Direction = new Vector2(0, -1);
            }
            else if (kbState.IsKeyDown(Keys.Down))
            {
                paddle.Direction = new Vector2(0, 1);
            }
            else
            {
                paddle.Direction = Vector2.Zero;
            }

            paddle.Update(gameTime);
            ball.Update(gameTime);

            ball.ProcessCollision(paddle.BoundingBox);


            //when the ball hits a paddle, tell the hud to increment the paddle's score
            //hud.Paddle01Score++;
            //like this:
            //if (ball.ProcessCollision(paddle.BoundingBox))
            //{
            //    hud.Paddle01Score++;
            //}

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, Vector2.Zero, null, Color.Wheat, 0, Vector2.Zero, Scale, SpriteEffects.None, 0f);

            ball.Draw(spriteBatch);
            paddle.Draw(spriteBatch);
            hud.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}