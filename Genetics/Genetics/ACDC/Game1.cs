using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Genetics
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch _spriteBatch;
        const int WindowWidth = 1024;
        const int WindowHeight = 512;
        public int WindowCellHeight = 32;
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
        private bool _manualMode;

        public void SetPlayer(Player ply, bool manualMode = false)
        {
            P1 = ply;
            _manualMode = manualMode;
        }


        protected override void Initialize()
        {
            if (P1 is null)
                throw new Exception("Missing player in game !");
            RessourceLoad.SetApperance(graphics, ref _appearances_dico);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private int _currentFrame;

        protected override void Update(GameTime gameTime)
        {
            Console.Write("\r\r\r\r\r\r" + P1.GetScore() + "        ");

            if (_manualMode)
                P1.ReceiveOrder(Keyboard.GetState().IsKeyDown(Keys.Left), Keyboard.GetState().IsKeyDown(Keys.Right),
                    Keyboard.GetState().IsKeyDown(Keys.Up), Keyboard.GetState().IsKeyDown(Keys.R));
            else
                P1.PlayAFrame();

            //game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.F))
                graphics.ToggleFullScreen();

            int FrameNb = RessourceLoad.GetCurrentMap().Timeout;
            _currentFrame++;
            if (_currentFrame > FrameNb)
                Exit();
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            // init
            _appearances_dico["tiles.png"].DisplayMap(_spriteBatch, RessourceLoad.GetCurrentMap(), P1.Position);

            String spriteToLoad = "";
            switch (P1.lastDir)
            {
                    case Player.Direction.left :
                        spriteToLoad = "Aelita move left.png";
                        break;
                    case Player.Direction.right:
                        spriteToLoad = "Aelita move right.png";
                        break;
                    case Player.Direction.none:
                        spriteToLoad = "Aelita idle.png";
                        break;
            }
            
            _appearances_dico[spriteToLoad].DisplayAppearance(_spriteBatch,
                P1.Position.X * RessourceLoad.GetCurrentMap().Width, P1.Position.Y * WindowCellHeight);
            Thread.Sleep(30);
            //end
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}