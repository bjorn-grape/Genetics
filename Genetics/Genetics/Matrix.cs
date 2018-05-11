using System;
using System.Collections.Generic;
using System.Runtime.Remoting;

namespace Genetics
{
    public class Matrix
    {
        #region Attributes

        private int Height;
        private int Width;
        public float[,] Tab;
        public float[] Bias;
        private static readonly Random Rdn = new Random();

        #endregion

        #region Constructors

        public Matrix(int height, int width, bool init = false)
        {
           throw new NotImplementedException();
        }

        public Matrix(List<float> tab)
        {
            Height = 1;
            Width = tab.Count;
            Tab = new float[Height, Width];
            for (int j = 0; j < Width; j++)
            {
                Tab[0, j] = tab[j];
            }
        }

        #endregion

        public void MakeCopyFrom(Matrix copy)
        {
            throw new NotImplementedException();
        }


        public void ApplyMutation()
        {
            throw new NotImplementedException();
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }

        public  static float Sigmoid(float x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is not only a multiplication ! We also normalize !
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Normalized multiplication</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }


        public void Print()
        { 
            throw new NotImplementedException();
        }
    }
}