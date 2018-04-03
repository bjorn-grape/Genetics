using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using OpenGL;

namespace Code_Lyoko
{
    public class Factory
    {
        private static List<Player> _listPlayer = null;
        private static string _pathLoad = null;
        private static string _pathSave = null;

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

        public static void SetPathLoadAndSave(string path1, string path2)
        {
            _pathLoad = path1;
            _pathSave = path2;
        }

        #endregion

        public static void Init(int size = 200)
        {
            if (_pathLoad is null)
                _listPlayer = SaveAndLoad.Load(_pathLoad);
            else
            {
                _listPlayer = new List<Player>();
                for (int i = 0; i < size; i++)
                {
                    _listPlayer.Add(new Player());
                }
            }
        }

        public static Player GetBestPlayer()
        {
            _listPlayer.Sort();
            return _listPlayer[_listPlayer.Count - 1];
        }

        public static void PrintScore()
        {
            for (int i = 0; i < _listPlayer.Count; i++)
            {
                Console.WriteLine("Player " + i + " has a score of " + _listPlayer[i]);
            }
        }

        public static void Train(int generationNumber)
        {
            for (int i = 0; i < generationNumber; i++)
            {
                foreach (var player in _listPlayer)
                {
                    player.ResetScore();
                    RessourceLoad.GoBackFirstMap();
                    for (int j = 0; j < 1000; j++)
                    {
                        player.PlayAFrame();
                    }
                }
                Regenerate();
            }
        }

        private static void Regenerate()
        {
            _listPlayer.Sort();
            int half = _listPlayer.Count / 2;
            for (int i = 0; i < half; i++)
            {
                _listPlayer[i].Replace(_listPlayer[i + half]); // replace weak
            }
        }
    }
}