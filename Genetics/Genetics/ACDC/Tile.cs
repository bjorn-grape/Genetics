using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Genetics
{
    public class Tile
    {
        protected Texture2D Texture;
        protected Rectangle Rect;
        private static ContentManager content;

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Rect, Color.White);
        }
    }
}