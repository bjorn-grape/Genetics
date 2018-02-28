using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Mime;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Code_Lyoko
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        const int WindowWidth = 1024;
        const int WindowHeight = 512;
        public int WindowCellHeight = 32;
        public int WindowCellWidth = 32;
        bool FULLSCREEN = false;
        

        private Dictionary<string, Appearance> _appearances_dico = new Dictionary<string, Appearance>();

        public static Vector2 getDimension()
        {
            return new Vector2(WindowWidth, WindowHeight);
        }

        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = WindowWidth,
                PreferredBackBufferHeight = WindowHeight
            };
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Put me in Update if you modify FULLSCREEN
        /// </summary>
        private void update_fullscreen()
        {
            if (FULLSCREEN)
            {
                if (!graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
            }
            else
            {
                if (graphics.IsFullScreen)
                    graphics.ToggleFullScreen();
            }
        }

        private Player P1;

        protected override void Initialize()
        {
            //RessourceLoad.InitMap();
            RessourceLoad.GenerateMap(3,10,50,20);
            var vecti = RessourceLoad.GetCurrentMap().PosInit;

            P1 = new Player(100, vecti);
            RessourceLoad.SetApperance(graphics, ref _appearances_dico);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private bool UpKeyEnabled = true;
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Map mappy = RessourceLoad.GetCurrentMap();
            
            
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && UpKeyEnabled)
            {
                P1.Jump();
                UpKeyEnabled = false;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Up) && !UpKeyEnabled)
                UpKeyEnabled = true;
                
            if(Keyboard.GetState().IsKeyDown(Keys.R))
                P1.SetStart(mappy);

            P1.ApplyForce(mappy);
            P1.InteractEnv(mappy);

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                P1.Move(1, 0, mappy);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                P1.Move(-1, 0, mappy);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            // init
            _appearances_dico["tiles.png"].DisplayMap(_spriteBatch, RessourceLoad.GetCurrentMap(), P1.Position);

            _appearances_dico["Aelita move.png"].DisplayAppearance(_spriteBatch,
                P1.Position.X * RessourceLoad.GetCurrentMap().Width, P1.Position.Y * WindowCellHeight);

            Thread.Sleep(20);


            //end
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}