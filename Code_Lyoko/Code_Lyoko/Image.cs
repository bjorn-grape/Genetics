using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Code_Lyoko
{
    public class Appearance
    {
        private Texture2D texture_;
        private List<Rectangle> rect_list_;
        private int _currentState;

        /// <summary>
        /// Create An apperance from texture
        /// </summary>
        /// <param name="texture">Texture to read</param>
        /// <param name="x">number of columns on image</param>
        /// <param name="y">number of rows on image</param>
        /// <param name="size">dimension of width and height on image</param>
        public Appearance(ref Texture2D texture, int x = 16, int y = 1, int size = 64)
        {
            texture_ = texture;

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

        public Texture2D get_texture()
        {
            return texture_;
        }

        public void DisplayAppearance(SpriteBatch sprt, float x, float y)
        {
            sprt.Draw(texture_, new Vector2(x, y), rect_list_[_currentState], Color.White);
            _currentState++;
            _currentState %= rect_list_.Count;
        }

        
        /// <summary>
        /// Draw image on board
        /// </summary>
        /// <param name="sprt">Current Sprite that will be sed for display</param>
        /// <param name="x">Position on x axis</param>
        /// <param name="y">Position on y axis</param>
        /// <param name="step">Step of animation to start on, previous step by default</param>
        public void DisplayAppearance(SpriteBatch sprt, float x, float y, uint step)
        {
            sprt.Draw(texture_, new Vector2(x, y), rect_list_[_currentState], Color.White);
            _currentState++;
            _currentState %= rect_list_.Count;
        }

        int GetTileValue(char c)
        {
            switch (c)
            {
                case 'W':
                    return 6;
                case ' ':
                    return 2;
                case 'D':
                    return 1;
                case 'S':
                    return 3;
                default:
                    throw new Exception("Tile does not exist !");
            }
        }

        public void DisplayMap(SpriteBatch sprt, Map map)
        {
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    int val = GetTileValue(map.Tab[i, j]);
                    sprt.Draw(texture_, new Vector2(j * 32, i * 32), rect_list_[val], Color.White);
                }
            }
        }
    }
}