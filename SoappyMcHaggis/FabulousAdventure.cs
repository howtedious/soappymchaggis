using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoappyMcHaggis;

namespace SoappyMcHaggis
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FabulousAdventure : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ContentManager contentManager;
        private int levelNo = 2;
        private Level currentLevel;
        private PlayerCharacter character;
        private Camera theCamera;
        private SpriteFont arial;


        public GameState currentGameState;
        public enum GameState
        {
            MenuScreen,
            Gameplay,
            GameOverWin,
            GameOverLose
        }


        public FabulousAdventure()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            currentGameState = GameState.MenuScreen;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Sound.Initialise();
            Fireball.LoadContent(contentManager);
            Vortex.LoadContent(contentManager);
            Fireballer.LoadContent(contentManager);
            Vortexer.LoadContent(contentManager);
            arial = contentManager.Load<SpriteFont>(@"Content/Arial");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            contentManager.Dispose();
            Sound.Shutdown();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            KeyboardState keyboard = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (currentGameState == GameState.Gameplay)
            {
                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                character.Update(elapsedTime);
                currentLevel.Update(elapsedTime);
                ProjectileManager.Update(elapsedTime);
                EnemyManager.Update(elapsedTime);
                if (keyboard.IsKeyDown(Keys.NumPad1))
                {
                    levelNo = 1;
                    LoadLevel();
                }
            }
            if (currentGameState == GameState.MenuScreen)
            {
                if (keyboard.IsKeyDown(Keys.Space))
                {
                    LoadLevel();
                    currentGameState = GameState.Gameplay;
                }
            }
            if (currentGameState == GameState.GameOverWin)
            {

            }

            Sound.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {


            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            /*
            String x = "" + gameTime.ElapsedGameTime.TotalSeconds;
            spriteBatch.DrawString(arial, x, new Vector2(300, 0), Color.Red);
                                 */
            if (currentGameState == GameState.Gameplay)
            {
                currentLevel.Draw(spriteBatch, theCamera);
                ProjectileManager.Draw(spriteBatch, theCamera);
                EnemyManager.Draw(spriteBatch, theCamera);
                character.Draw(spriteBatch, theCamera);
            }
            if (currentGameState == GameState.MenuScreen)
            {
                graphics.GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(arial, "Press Space to Begin", Vector2.Zero, Color.Red);
            }
            if (currentGameState == GameState.GameOverWin)
            {

                graphics.GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(arial, "You Win!", Vector2.Zero, Color.Blue);
            }
            if (currentGameState == GameState.GameOverLose)
            {
                graphics.GraphicsDevice.Clear(Color.Black);
                spriteBatch.DrawString(arial, "You Lose!", Vector2.Zero, Color.Yellow);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }


        public void LoadLevel()
        {
            EnemyManager.Clear();
            ProjectileManager.Clear();
            currentLevel = new Level(levelNo, contentManager, this);

            //Create a new Camera at the starting position of the player character
            theCamera = new Camera(currentLevel.StartPosition, currentLevel.MapBounds);
            character = new PlayerCharacter(theCamera, currentLevel.StartPosition, currentLevel, 5, 5, 75, 5, this, 5);
            character.LoadContent(contentManager);
        }
    }
}
