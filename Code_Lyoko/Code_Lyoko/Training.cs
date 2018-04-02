using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Code_Lyoko
{
    public class Training
    {
        private string _fileIn;
        private string _folderOut;
        private int _poolSize;
        private int _numberOfGeneration;
        private int _nthToShow;
        private List<Player> _list_player;

        private readonly Action _act;


        public enum Action
        {
            LoadFromFileAndTrain,
            GenerateFromScratchAndTrain,
            ShowBest,
            ShowNth
        }

        /// <summary>
        /// Function necessary for training
        /// </summary>
        /// <param name="act">Action to do</param>
        /// <param name="poolSize">Number of players to train</param>
        /// <param name="numberOfGeneration">Number Of Generation to train</param>
        /// <param name="outputFolder"></param>
        /// <param name="loadFile"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public Training(Action act, int poolSize = 200, int numberOfGeneration = 1, string outputFolder = null,
            string loadFile = null)
        {
            _act = act;
            switch (_act)
            {
                case Action.GenerateFromScratchAndTrain:
                    this._poolSize = poolSize;
                    this._numberOfGeneration = numberOfGeneration;
                    if (outputFolder == null)
                        throw new ArgumentException("Output folder must not be null");
                    this._folderOut = outputFolder;
                    break;
                case Action.LoadFromFileAndTrain:
                    this._poolSize = poolSize;
                    this._numberOfGeneration = numberOfGeneration;
                    if (outputFolder == null)
                        throw new ArgumentException("Output folder must not be null");
                    this._folderOut = outputFolder;
                    if (loadFile == null)
                        throw new ArgumentException("Input file must not be null");
                    this._fileIn = loadFile;
                    break;
                default:
                    throw new Exception("Wrong Use of constructor");
            }
        }

        /// <summary>
        /// Constructor used for displaying at a certain generation
        /// </summary>
        /// <param name="act"></param>
        /// <param name="loadFile"></param>
        /// <param name="poolSize"></param>
        /// <param name="nth"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public Training(Action act, string loadFile = null, int poolSize = 200, int nth = -1)
        {
            _act = act;
            switch (_act)
            {
                case Action.ShowBest:
                    if (loadFile == null)
                        throw new ArgumentException("Input file must not be null");
                    this._fileIn = loadFile;
                    this._poolSize = poolSize;
                    break;
                case Action.ShowNth:
                    if (loadFile == null)
                        throw new ArgumentException("Input file must not be null");
                    this._fileIn = loadFile;
                    this._poolSize = poolSize;
                    if (nth < 0)
                        throw new ArgumentException("Index of example cannot be undefined");
                    this._nthToShow = nth;
                    break;
                default:
                    throw new Exception("Wrong Use of constructor");
            }
        }
        
        
    }
}