using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net.Configuration;
using System.Net.Mime;
using System.Reflection;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace Genetics
{
    internal class Program
    {
        private const string PathForTest = "fullnew.save";
        private const string PathBotToSubmit = "../../save/bot.save";

        public static void Main(string[] args)
        {
            RessourceLoad.InitMap();

            Console.WriteLine(TestScore());
            // Feel free to use all the function below in order to train your players
        }

        /// <summary>
        /// This function create a whole new population
        /// and trains a population of 200 players by using new players at each generation
        /// </summary>
        /// <param name="n">number of generations you want to proceed</param>
        private static void NewTraining(int n)
        {
            Factory.SetPathSave(PathForTest);
            Factory.Init();
            Factory.TrainWithNew(n);
            Factory.PrintScore();
            Factory.SaveState();
        }


        /// <summary>
        ///  This function trains a population of 200 players by using new players at each generation
        /// </summary>
        /// <param name="n">number of generations you want to proceed</param>
        private static void TrainWithNew(int n)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.TrainWithNew(n);
            Factory.PrintScore();
            Factory.SaveState();
        }

        /// <summary>
        /// This function trains a population of 200 players by duplicating and applying modification to the copy of 
        /// the best players
        /// </summary>
        /// <param name="n">number of generations you want to proceed</param>
        private static void Train(int n)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.Train(n);
            Factory.PrintScore();
            Factory.SaveState();
        }
/*
        private static void TrainMt(int n)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.TrainMt(n);
            Factory.PrintScore();
            Factory.SaveState();
        }

        private static void NewTrainMt(int n)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.TrainMt(n, false);
            Factory.PrintScore();
            Factory.SaveState();
        }*/

        /// <summary>
        /// Show the current best player
        /// </summary>
        private static void Showbest()
        {
            Game1 game = new Game1();
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.GetBestPlayer().SetStart(RessourceLoad.GetCurrentMap());
            game.SetPlayer(Factory.GetBestPlayer());
            game.Run();
        }

        /// <summary>
        /// Show the nth player sorted by score in increasing order
        /// </summary>
        /// <param name="nth">player you want to access to, should be between 0 and 199</param>
        private static void ShowNth(int nth)
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
        private static void PlayAsHuman()
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
        private static void SaveBest()
        {
            Factory.SetPathLoad(PathForTest);
            Factory.Init();
            var soloList = new List<Player> {Factory.GetBestPlayer()};
            SaveAndLoad.Save(PathBotToSubmit, soloList);
            Console.WriteLine("Saved Best Player");
        }

        // Students will not use that 

        /*

        private static void MultiTrain(int nb, bool mutations)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.TrainAllMaps(nb, mutations);
            Factory.PrintScore();
            Factory.SaveState();
        }
        */
        
        public static float TestScore()
        {
    
            Assert.AreEqual(true,File.Exists(PathBotToSubmit));
          
            Factory.SetListPlayer(SaveAndLoad.Load(PathBotToSubmit));
            var ply = Factory.GetBestPlayer();
            int sum = 0;
            foreach (var tuple in RessourceLoad.MapGet())
            {
                RessourceLoad.SetCurrentMap(tuple.Key);
                int FrameNb = RessourceLoad.GetCurrentMap().Timeout;
                ply.ResetScore();
                ply.SetStart(RessourceLoad.GetCurrentMap());
                for (int j = 0; j < FrameNb; j++)
                    ply.PlayAFrame();
                sum += ply.GetScore();
               
                ply.SetStart(RessourceLoad.GetCurrentMap()); 
            }
            float result = (float)sum / 45000;
            if (result < 0)
                result = 0;
            else if (result > 1)
                result = 1;
            return result;
        }
    }
}