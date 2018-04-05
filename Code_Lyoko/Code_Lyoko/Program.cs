using System;
using System.Collections.Generic;
using System.Data;
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
            RessourceLoad.SetCurrentMap("long");
            //RessourceLoad.GenerateMap(3,20,50,60);
            //PlayAsHuman();
            TrainFirstTime();
            Train();
            ShowNth(199);
        }

        static void TrainFirstTime()
        {
            Factory.SetPathSave("testoftrain2.save");
            Factory.Init_new();
            Factory.TrainWithNew(10);
            Factory.PrintScore();
            Factory.SaveState();
            
        }
        
        static void Train()
        {
            
            Factory.SetPathLoadAndSave("testoftrain2.save");
            Factory.InitFromPath();
            Factory.Train(10);
            Factory.PrintScore();
            Factory.SaveState();
        }

        static void Showbest()
        {
            Game1 game = new Game1();
            Factory.SetPathLoadAndSave("testoftrain2.save");
            Factory.InitFromPath();
            Factory.PrintScore(true);
            game.SetPlayer(Factory.GetBestPlayer());
            
            game.Run();
        }
        
        static void ShowNth(int nth)
        {
            Game1 game = new Game1();
            Factory.SetPathLoadAndSave("testoftrain2.save");
            Factory.InitFromPath();
            Factory.PrintScore(true);
            game.SetPlayer(Factory.GetNthPlayer(nth));
            
            game.Run();
        }

        static void  PlayAsHuman()
        {
            Game1 game = new Game1();
            Player player = new Player();
            game.SetPlayer(player,true);
            game.Run();
        }
    }
}