using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Code_Lyoko
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const int WINDOW_WIDTH = 1024;
        const int WINDOW_HEIGHT = 1024;
        bool FULLSCREEN = false;

        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Put me in Update if you modify FULLSCREEN
        /// </summary>
        void update_fullscreen()
        {
            if (FULLSCREEN)
            {
                if(!graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
            }
            else
            {
                if(graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
            }
        }

        protected override void Initialize ()
        {
            // TODO: Add your initialization logic here
            base.Initialize ();
        }
        
        protected override void LoadContent ()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch (GraphicsDevice);
 
            //TODO: use this.Content to load your game content here 
        }
        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
 
            // TODO: Add your update logic here
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
 
            base.Draw(gameTime);
        }
    }
}