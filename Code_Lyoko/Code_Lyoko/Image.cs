using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Code_Lyoko
{
    public class Appearance
    {
        private Texture2D texture_;
        private SpriteBatch _sprt;
        private List<Rectangle> rect_list_;
        private int _currentState;

        /// <summary>
        /// Create An apperance from texture
        /// </summary>
        /// <param name="sprt">Sprite to write on</param>
        /// <param name="texture">Texture to read</param>
        /// <param name="x">number of columns on image</param>
        /// <param name="y">number of rows on image</param>
        /// <param name="size">dimension of width and height on image</param>
        public Appearance(SpriteBatch sprt, Texture2D texture, int x = 16, int y = 1, int size = 128)
        {
            texture_ = texture;
            _sprt = sprt;
            _currentState = 0;
            rect_list_ = new List<Rectangle>();
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Rectangle tmp = new Rectangle(j * size, i * size, size, size);
                    rect_list_.Add(tmp);
                }
            }
        }

        public void DisplayAppearance(float x, float y)
        {
            _sprt.Draw(texture_,new Vector2(x,y),rect_list_[_currentState],Color.White);
            _currentState++;
        }
    }
}