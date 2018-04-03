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
            RessourceLoad.InitMap();
            //RessourceLoad.GenerateMap(3,20,50,60);

            Game1 game = new Game1();
            List<Player> listi = new List<Player>();
            for (int i = 0; i < 5; i++)
            {
                Player pl1 = new Player();
                listi.Add(pl1);
            }

            game.SetPlayer(listi[0], true);

            game.Run();


            SaveAndLoad.Save("testttt.save", listi);
            SaveAndLoad.Load("testttt.save");
        }
    }
}