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
            throw new NotImplementedException();
        }

        public static void SetListPlayer(List<Player> li)
        {
            _listPlayer = li;
        }

        public static Player GetBestPlayer()
        {
            throw new NotImplementedException();
        }

        public static Player GetNthPlayer(int nth)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LoadAndSave

        public static void SetPathLoad(string path)
        {
            throw new NotImplementedException();
        }

        public static void SetPathSave(string path)
        {
            throw new NotImplementedException();

        }

        public static void SetPathLoadAndSave(string path)
        {
            throw new NotImplementedException();
        }

        public static String GetPathLoad()
        {
            return _pathLoad;
        }
        public static String GetPathSave()
        {
            return _pathSave;
        }

        public static void SaveState()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Init

        public static void InitNew(int size = 200)
        {
            throw new NotImplementedException();
        }

        public static void Init()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Display

        public static void PrintScore()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Training

        public static void TrainWithNew(int generationNumber)
        {
            Train(generationNumber, false);
        }

        public static void Train(int generationNumber, bool replaceWithMutation = true)
        {
            throw new NotImplementedException();
        }

        private static void Regenerate(bool replace_with_mutation = true)
        {
            throw new NotImplementedException();
        }


        public  static void SimpleSort()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}