using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;


namespace Genetics
{
    public class Map
    {
        public char[,] Tab;

        public int PosX;
        public int PosY;
        public Vector2 PosInit = new Vector2(2, 2);
        public uint Height;
        public uint Width;
        public const uint SizeTile = 32;
        public int Timeout;

        public Map(string path)
        {
            Tab = ParseFromFile(path);
        }

        public Map(char[,] arr, uint h, uint w)
        {
            Tab = arr;
            Height = h;
            Width = w;
        }


        bool crush(char c)
        {
            switch (c)
            {
                case ' ':
                    return false;
                case 'W':
                    return true;
                case 'D':
                    return false;
                case 'S':
                    return false;
                default:
                    return true;
            }
        }

        float TileTypeForNeural(char c)
        {
            switch (c)
            {
                case ' ':
                    return 0f;
                case 'W':
                    return 0.5f;
                case 'D':
                    return 0;
                case 'S':
                    return 0;
                default:
                    return 1f;
            }
        }

        public bool IsColliding(float x, float y)
        {
            char br = Tab[Convert.ToInt32(y + 1.2f), Convert.ToInt32(x)];
            char tr = Tab[Convert.ToInt32(y + 1.2f), Convert.ToInt32(x + 0.8f)];
            char bl = Tab[Convert.ToInt32(y), Convert.ToInt32(x)];
            char tl = Tab[Convert.ToInt32(y), Convert.ToInt32(x + 0.8f)];
            // r & l are in case player is bigger than tile he/she crushes
            char l = Tab[Convert.ToInt32(y + 0.6f), Convert.ToInt32(x)];
            char r = Tab[Convert.ToInt32(y + 0.6f), Convert.ToInt32(x + 0.8f)];

            return crush(br) || crush(bl) || crush(tl) || crush(tr) || crush(r) || crush(l);
        }

        public bool IsGroundForPlayer(float x, float y)
        {
            char bl = Tab[Convert.ToInt32(y + 1.4f), Convert.ToInt32(x + 0.001f)];
            char br = Tab[Convert.ToInt32(y + 1.4), Convert.ToInt32(x + 0.799f)];
            return crush(br) || crush(bl);
        }

        public bool IsEndMap(float x, float y)
        {
            char c = Tab[Convert.ToInt32(y + 0.6f), Convert.ToInt32(x + 0.3f)];

            return c == 'D';
        }


        public char[,] ParseFromFile(string path)
        {
            IEnumerable<string> lines = File.ReadAllLines(path);
            var file = new StreamReader(path);
            Timeout = Convert.ToInt32(file.ReadLine());

            Height = (uint) lines.Count() - 1;
            Width = (uint) lines.Last().Count();

            char[,] tab = new char[Height, Width];


            for (int i = 0; i < Height; i++)
            {
                string str = file.ReadLine();
                if (str != null && str.Length != Width)
                    throw new Exception("Invalid File !");
                for (int j = 0; j < Width; j++)
                {
                    if (str[j] == 'S')
                    {
                        //Console.WriteLine("found Start : " + i + "  " + j);
                        PosInit = new Vector2(j, i);
                    }

                    tab[i, j] = str[j];
                }
            }

            return tab;
        }


        public Matrix GetMapAround(float xx, float yy)
        {
            var tab = new List<float>();
            int x = Convert.ToInt32(xx);
            int y = Convert.ToInt32(yy);
            for (int i = -2; i < 5; i++)
            {
                for (int j = -2; j < 5; j++)
                {
                    int tx = y + i;
                    int ty = x + j;
                    if (tx < 0 || tx >= Height || ty < 0 || ty >= Width)
                        tab.Add(0.5f);
                    else
                    {
                        tab.Add(TileTypeForNeural(Tab[tx, ty]));
                    }
                }
            }


            return new Matrix(tab);
        }
    }
}