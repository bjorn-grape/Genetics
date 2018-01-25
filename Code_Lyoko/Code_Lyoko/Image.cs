using System.Net.Mime;
using Microsoft.Xna.Framework.Graphics;

namespace Code_Lyoko
{
    public class PlayerAppearance
    {
        private Texture2D texture_;
        private int tile_size_;
        private int tile_number_x_;
        private int tile_number_y_;
        public string name_;
        public int tile_number;
        
        
        /// <summary>
        /// player tile constructor
        /// </summary>
        /// <param name="path"> Path to load</param>
        /// <param name="x">number of columns on image</param>
        /// <param name="y">number of rows on image</param>
        /// <param name="size">size of a tile</param>
        public PlayerAppearance(char path, int x = 16, int y = 1, int size = 128)
        {
            tile_number_x_ = x;
            tile_number_y_ = y;
            tile_size_ = size;
            //texture_ = Content.Load
            
        }

    }
}