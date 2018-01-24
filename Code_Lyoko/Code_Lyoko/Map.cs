using System;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace Code_Lyoko
{
    public class Map
    {
        private int[,] tab;

        public Map(string path)
        {
            tab = ParseFromFile(path);
        }

        static int get_tile(char c)
        {
            switch (c)
            {
                case ' ':
                    return 0;
                case 'W':
                    return 1;
                default:
                    return -1;
            }
        }

        public static int[,] ParseFromFile(string path)
        {
            int[,] tab = new int[32, 32];

            for (int i = 0; i < 32; i++)
            {
                var str = File.ReadLines(path).ToString();
                if (str.Length != 32)
                    throw new Exception("Invalid File");
                for (int j = 0; j < 32; j++)
                {
                    tab[i, j] = get_tile(str[i]);
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
                    Console.Write(tab[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}