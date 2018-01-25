using System;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace Code_Lyoko
{
    public class Map
    {
        private char[,] tab_;

        public char[,] Tab => tab_;
        public int pos_x;
        public int pos_y;
        

        public Map(string path)
        {
            tab_ = ParseFromFile(path);
        }

        
        static int get_tile(char c)
        {
            switch (c)
            {
                case ' ':
                    return 1;
                case 'W':
                    return 2;
                case 'D':
                    return 3;
                default:
                    return 0;
            }
        }

        
        public static char[,] ParseFromFile(string path)
        {
            char[,] tab = new char[32, 32];
            var file = new StreamReader(path);
            
            for (int i = 0; i < 32; i++)
            {
                string str = file.ReadLine();
                if(str.Length != 32)
                    throw new Exception("Invalid File !");
                for (int j = 0; j < 32; j++)
                {
                    
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