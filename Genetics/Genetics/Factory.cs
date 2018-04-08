using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;



namespace Genetics
{
    public static class Factory
    {
        #region Attributes

        private static List<Player> _listPlayer;
        private static string _pathLoad;
        private static string _pathSave;
       

        #endregion

        #region Getters

        public static List<Player> GetListPlayer()
        {
            return _listPlayer;
        }

        public static Player GetBestPlayer()
        {
            SimpleSort();
            return _listPlayer[_listPlayer.Count - 1];
        }
        
        public static Player GetNthPlayer(int nth)
        {
            SimpleSort();
            
            return _listPlayer[nth];
        }

        #endregion

        #region LoadAndSave

        public static void SetPathLoad(string path)
        {
            _pathLoad = path;
        }


        public static void SetPathSave(string path)
        {
            _pathSave = path;
        }

        public static void SetPathLoadAndSave(string path)
        {
            _pathLoad = path;
            _pathSave = path;
        }

        public static void SaveState()
        {
            
            if (_pathSave is null)
                throw new Exception("No path Specified when saving !");
            SaveAndLoad.Save(_pathSave, _listPlayer);
        }

        #endregion

        #region Init

        public static void InitNew(int size = 200)
        {
            
            _listPlayer = new List<Player>();
            for (int i = 0; i < size; i++)
            {
                _listPlayer.Add(new Player());
            }
        }

        public static void Init()
        {
            if (File.Exists(_pathLoad))
                _listPlayer = SaveAndLoad.Load(_pathLoad);
            else
                InitNew();


        }

        #endregion

        #region Display

        public static void PrintScore(bool extended = false)
        {
            SimpleSort();
            for (int i = 0; i < _listPlayer.Count; i++)
            {
                Console.WriteLine("Player " + i + " has a score of " + _listPlayer[i].GetScore());
                if (extended)
                {
                    _listPlayer[i].SetStart(RessourceLoad.GetCurrentMap());
                    var plop = _listPlayer[i].UseBrain(RessourceLoad.GetCurrentMap()
                        .GetMapAround(_listPlayer[i].Position.X, _listPlayer[i].Position.Y));
                    plop.Print();
                }
            }
        }
        
        public static void PrintScore()
        {
            SimpleSort();
            
            for (int i = 0; i < _listPlayer.Count; i++)
            {
                Console.WriteLine("Player " + i + " has a score of " + _listPlayer[i].GetScore());
            }
        }

        #endregion

        #region Training

        public static void TrainWithNew(int generationNumber)
        {
            Train(generationNumber,false);
        }
        
        public static void Train(int generationNumber, bool replaceWithMutation = true)
        {
            int FrameNb = RessourceLoad.GetCurrentMap().Timeout;
            for (int i = 0; i < generationNumber; i++)
            {
                Console.WriteLine("\nTraining " + (i + 1) + "/" + generationNumber);
                for (int k = 0; k < _listPlayer.Count; k++)
                {
                    Console.Write("\r\r\r\r\r\r" + k * 100 / _listPlayer.Count + "%    ");
                    _listPlayer[k].ResetScore();
                    _listPlayer[k].SetStart(RessourceLoad.GetCurrentMap());
                    for (int j = 0; j < FrameNb; j++)
                        _listPlayer[k].PlayAFrame();

                    Console.Write("\r\r\r\r\r\rDONE.");
                }

                Regenerate(replaceWithMutation);
            }
        }

        private static void Regenerate(bool replace_with_mutation = true)
        {
            SimpleSort();
            int half = _listPlayer.Count / 2;
            for (int i = 0; i < half; i++)
            {
                _listPlayer[i].Replace(_listPlayer[i + half], replace_with_mutation);
            }
        }


        private static void SimpleSort()
        {
            for (int i = 0; i < _listPlayer.Count; i++)
            {
                Player min = _listPlayer[i];
                int minIndex = i;
                for (int j = i; j < _listPlayer.Count; j++)
                {
                    if (_listPlayer[j] < min)
                    {
                        min = _listPlayer[j];
                        minIndex = j;
                    }
                }

                _listPlayer[minIndex] = _listPlayer[i];
                _listPlayer[i] = min;
            }
        }

        #endregion
    }
}