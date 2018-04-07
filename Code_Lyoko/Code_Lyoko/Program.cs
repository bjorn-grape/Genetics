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
        private const string PathForTest = "test.save";
        private const string PathBotToSubmit = "../../save/bot.save";

        public static void Main(string[] args)
        {
            RessourceLoad.InitMap();

            RessourceLoad.SetCurrentMap("long"); //with this line you can set the current map from folder map

            // Feel free to use all the function below in order to train your players
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function trains a population of 200 players by using new players at each generation
        /// </summary>
        /// <param name="n">number of generations you want to proceed</param>
        static void TrainWithNew(int n)
        {
            Factory.SetPathSave(PathForTest);
            Factory.Init_new();
            Factory.TrainWithNew(n);
            Factory.PrintScore();
            Factory.SaveState();
        }

        /// <summary>
        /// This function trains a population of 200 players by duplicating and applying modification to the copy of 
        /// the best players
        /// </summary>
        /// <param name="n">number of generations you want to proceed</param>
        static void Train(int n)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.Train(n);
            Factory.PrintScore();
            Factory.SaveState();
        }

        /// <summary>
        /// Show the current best player
        /// </summary>
        static void Showbest()
        {
            Game1 game = new Game1();
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.PrintScore(true);
            game.SetPlayer(Factory.GetBestPlayer());
            game.Run();
        }

        /// <summary>
        /// Show the nth player sorted by score in increasing order
        /// </summary>
        /// <param name="nth">player you want to access to, should be between 0 and 199</param>
        static void ShowNth(int nth)
        {
            Game1 game = new Game1();
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.PrintScore(true);
            game.SetPlayer(Factory.GetNthPlayer(nth));

            game.Run();
        }

        /// <summary>
        /// This function allows you to try the game
        /// </summary>
        static void PlayAsHuman()
        {
            Game1 game = new Game1();
            Player player = new Player();
            player.SetStart(RessourceLoad.GetCurrentMap());
            game.SetPlayer(player, true);
            game.Run();
        }

        /// <summary>
        /// save best player in folder save, you will be marked on this, so DON'T forget it!
        /// </summary>
        static void SaveBest()
        {
            Factory.SetPathLoad(PathForTest);
            Factory.Init();
            var soloList = new List<Player> {Factory.GetBestPlayer()};
            SaveAndLoad.Save(PathBotToSubmit, soloList);
        }
    }
}