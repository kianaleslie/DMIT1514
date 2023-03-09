using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MosquitoAttack
{
    internal class MosquitoAttackGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        protected const int WindowWidth = 550;
        protected const int WindowHeight = 400;
        protected const int NumMosquitoes = 10;

        protected Texture2D background;
        protected SpriteFont magra;
        protected string statusMessage;

        protected Cannon cannon;
        protected Mosquito[] mosquitoes;

        protected enum MosquitoAttackState
        {
            Playing,
            Paused,
            Over
        }

        protected MosquitoAttackState gameState;
        protected KeyboardState kbPreviousState;

        public MosquitoAttackGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();

            gameState = MosquitoAttackState.Playing;
            kbPreviousState = Keyboard.GetState();
            cannon = new Cannon();
            mosquitoes = new Mosquito[NumMosquitoes];

            for (int c = 0; c < NumMosquitoes; c++)
            {
                mosquitoes[c] = new Mosquito();
            }

            base.Initialize();

            cannon.Initialize(new Vector2(50, 325), new Rectangle(0, 0, WindowWidth, WindowHeight));
            int spaceBetweenMosquitoes = 1;
            foreach (Mosquito mosquito in mosquitoes)
            {
                mosquito.Initialize(new Vector2(spaceBetweenMosquitoes, 25), new Rectangle(0, 0, WindowWidth, WindowHeight), new Vector2(-1, 0));
                spaceBetweenMosquitoes += 50;
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("Background");
            magra = Content.Load<SpriteFont>("magra");

            cannon.LoadContent(Content);

            foreach (Mosquito mosquito in mosquitoes)
            {
                mosquito.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();
            switch (gameState)
            {
                case MosquitoAttackState.Playing:
                    if (kbState.IsKeyDown(Keys.P) && kbPreviousState.IsKeyUp(Keys.P))
                    {
                        gameState = MosquitoAttackState.Paused;
                        statusMessage = "Game Paused, press P to start playing.";
                    }
                    if (kbState.IsKeyDown(Keys.Left))
                    {
                        cannon.Move(new Vector2(-1, 0));
                    }
                    else if (kbState.IsKeyDown(Keys.Right))
                    {
                        cannon.Move(new Vector2(1, 0));
                    }
                    else
                    {
                        cannon.Move(new Vector2(0, 0));
                    }
                    cannon.Update(gameTime);

                    if (kbState.IsKeyDown(Keys.Space) && kbPreviousState.IsKeyUp(Keys.Space))
                    {
                        cannon.Shoot();
                    }
                    foreach (Mosquito mosquito in mosquitoes)
                    {
                        mosquito.Update(gameTime);
                        if (mosquito.Alive() && cannon.ProcessProjectileCollisions(mosquito.BoundingBox))
                        {
                            mosquito.Die();
                        }
                        if (cannon.Alive() && mosquito.ProcessProjectileCollisions(cannon.BoundingBox))
                        {
                            cannon.Die();
                        }
                    }
                    break;
                case MosquitoAttackState.Paused:
                    if (kbState.IsKeyDown(Keys.P) && kbPreviousState.IsKeyUp(Keys.P))
                    {
                        gameState = MosquitoAttackState.Playing;
                        statusMessage = "";
                    }
                    break;
                case MosquitoAttackState.Over:
                    break;
            }

            kbPreviousState = kbState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (gameState)
            {
                case MosquitoAttackState.Playing:
                    spriteBatch.Draw(background, Vector2.Zero, Color.White);
                    cannon.Draw(spriteBatch);
                    foreach (Mosquito mosquito in mosquitoes)
                    {
                        mosquito.Draw(spriteBatch);
                    }
                    break;
                case MosquitoAttackState.Paused:
                    spriteBatch.Draw(background, Vector2.Zero, Color.LightGray);
                    spriteBatch.DrawString(magra, statusMessage, new Vector2(20, 50), Color.White);
                    break;
                case MosquitoAttackState.Over:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
