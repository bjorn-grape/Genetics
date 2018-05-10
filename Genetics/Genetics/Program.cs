using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Genetics
{
    internal class Program
    {
        private const string PathForTest = "test.save";
        private const string PathBotToSubmit = "../../save/bot.save";

        public static void Main(string[] args)
        {
            RessourceLoad.InitMap();
            RessourceLoad.SetCurrentMap("example2"); //with this line you can set the current map from folder map
            multiTrain(1);
            //TrainWithNew(10);
            //PlayAsHuman();
            Showbest();

          

            //SaveBest();
            // Feel free to use all the function below in order to train your players
        }

        /// <summary>
        /// This function trains a population of 200 players by using new players at each generation
        /// </summary>
        /// <param name="n">number of generations you want to proceed</param>
        static void NewTraining(int n)
        {
            Factory.SetPathSave(PathForTest);
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
        static void TrainWithNew(int n)
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
            Factory.GetBestPlayer().SetStart(RessourceLoad.GetCurrentMap());
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
            Console.WriteLine("Saved Best Player");
        }

        static void FromTerminalMakeTests(String[] args)
        {

            bool isValid = args.Length == 1;
            
            RessourceLoad.SetCurrentMap("long"); //with this line you can set the current map from folder map

            if (isValid)
            {
                try
                {
                    if (File.Exists(args[0]))
                        Factory.SetListPlayer(SaveAndLoad.Load(args[0]));
                    else
                        isValid = false;
                    if (Factory.GetListPlayer().Count != 1)
                        isValid = false;
                }
                catch (Exception e)
                {
                    isValid = false;
                }
            }
            

            var sizeMaps = RessourceLoad.MapGet().Count;
            var incr = 0;
            Console.WriteLine("{");
            foreach (var tuple in RessourceLoad.MapGet())
            {
                Console.Write("\"" + tuple.Key + "\" : ");
                if (isValid)
                {
                    RessourceLoad.SetCurrentMap(tuple.Key);
                    Factory.test();
                }
                else
                {
                    Console.Write("0");
                }

                if (++incr < sizeMaps)
                {
                    Console.WriteLine(",");
                }
            }
            Console.WriteLine("}");

        }

        static void multiTrain(int nb)
        {
            Factory.SetPathLoadAndSave(PathForTest);
            Factory.Init();
            Factory.TrainAllMaps(nb);
            Factory.PrintScore();
            Factory.SaveState();
        }
    }
}