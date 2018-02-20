using System;
using System.Collections.Generic;
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
        const int WindowHeight = 1024;
        bool FULLSCREEN = false;
        private int current_map = 0;
        
        private Dictionary<string, Appearance> _appearances_dico = new Dictionary<string, Appearance>();

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
            
            RessourceLoad.InitMap();
            var vecti = RessourceLoad.maps_[current_map].posInit;
            
            P1 = new Player(100, vecti);
            RessourceLoad.SetApperance(graphics, ref _appearances_dico);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                P1.Move(0, -1, RessourceLoad.maps_[current_map]);

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                P1.Move(0, 1, RessourceLoad.maps_[current_map]);

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                P1.Move(1, 0,RessourceLoad.maps_[current_map]);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                P1.Move(-1, 0, RessourceLoad.maps_[current_map]);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            // init
            _appearances_dico["tiles.png"].DisplayMap(_spriteBatch, RessourceLoad.maps_[current_map]);
            _appearances_dico["Aelita move.png"].DisplayAppearance(_spriteBatch, P1.Position.X, P1.Position.Y);
            Thread.Sleep(20);


            //end
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}