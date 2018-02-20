using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;

namespace Code_Lyoko
{
    public class Map
    {
        private char[,] tab_;

        public char[,] Tab => tab_;
        public int pos_x;
        public int pos_y;
        public Vector2 posInit = new Vector2(2, 2);
        public UInt32 heigth = 32;
        public UInt32 width = 32;
        public UInt32 size_tile = 32;

        public Map(string path)
        {
            tab_ = ParseFromFile(path);
        }


        bool crush(char c, bool half)
        {
            switch (c)
            {
                case ' ':
                    return false;
                case 'W':
                    return true;
                case 'D':
                    return false;
                default:
                    return true;
            }
        }

        public bool IsColliding(int x, int y)
        {
            int xcolli = Convert.ToInt32(x / size_tile);
            int ycolli = Convert.ToInt32(y / size_tile);
            char elm = tab_[xcolli, ycolli];
            bool ishalf = x % size_tile < size_tile / 2;
            return crush(elm, ishalf);
        }


        public char[,] ParseFromFile(string path)
        {
            char[,] tab = new char[heigth, width];
            var file = new StreamReader(path);

            for (int i = 0; i < heigth; i++)
            {
                string str = file.ReadLine();
                if (str != null && str.Length != width)
                    throw new Exception("Invalid File !");
                for (int j = 0; j < width; j++)
                {
                    if (str[j] == 'S')
                    {
                        Console.WriteLine("found S : " + i + "  " + j);
                        posInit = new Vector2(j, i);
                    }

                    tab[i, j] = str[j];
                }
            }

            return tab;
        }

        public void Print()
        {
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    Console.Write(tab_[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}