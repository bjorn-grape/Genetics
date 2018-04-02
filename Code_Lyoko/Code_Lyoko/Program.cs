using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Code_Lyoko
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Game1 game = new Game1();
            
            game.Run();
           /* List<Player> listi = new List<Player>();
            for (int i = 0; i < 5; i++)
            {
                Player pl1 = new Player(100,new Vector2(0,0));
                listi.Add(pl1);
            }*/
            
            //SaveAndLoad.Save("testttt.save", listi);
            //SaveAndLoad.Load("testttt.save");

        }
    }
}