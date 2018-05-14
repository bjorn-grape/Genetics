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
            Height = height;
            Width = width;
            Tab = new float[height, width];
            Bias = new float[width];
            if (!init) return;

            for (int i = 0; i < Height; i++)
            for (int j = 0; j < Width; j++)
                Tab[i, j] = (float) Rdn.Next(100) / 100;
            for (int i = 0; i < Width; i++)
            {
                Bias[i] = (float) Rdn.Next(100) / 200 - 1f;
            }
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
            if (!(Width == copy.Width && Height == copy.Height))
            {
                Height = copy.Height;
                Width = copy.Width;
                Tab = new float[Height, Width];
                Bias = new float[Width];
            }

            for (int j = 0; j < Width; j++)
            {
                Bias[j] = copy.Bias[j];
            }
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tab[i, j] = copy.Tab[i, j];
                }
            }

            
        }


        public void ApplyMutation()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    float k = Rdn.Next(20) / 100f - 0.1f;
                    Tab[i, j] += k;
                    if (Tab[i, j] > 1)
                        Tab[i, j] = 1f;
                    
                    if (Tab[i, j] < 0)
                        Tab[i, j] = 0f;
                }
            }

            for (int j = 0; j < Width; j++)
            {
                Bias[j] += Rdn.Next(10) / 100f - 0.05f;
            }
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Wrong size for Matrix!");
            Matrix C = new Matrix(a.Height, a.Width);
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < a.Width; j++)
                {
                    C.Tab[i, j] = a.Tab[i, j] + b.Tab[i, j];
                }
            }

            return C;
        }

        public  static float Sigmoid(float x)
        {
            return 1 / (1 + (float) Math.Exp(-x));
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
            if (a.Width != b.Height)
                throw new ArgumentException("Wrong size for Matrix!");
            Matrix C = new Matrix(a.Height, b.Width);
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < b.Width; j++)
                {
                    float summ = 0;
                    for (int k = 0; k < a.Width; k++)
                        summ += a.Tab[i, k] * b.Tab[k, j];

                    C.Tab[i, j] = Sigmoid(summ / b.Width + b.Bias[j]); 
                }
            }

            return C;
        }


        public void Print()
        { 
            
            
            Console.Write("Matrix");
            for (int j = 0; j < Width; j++)
                Console.Write("-----");
            
            Console.WriteLine();
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    Console.Write(Tab[i, j] + "|");
                Console.WriteLine();
            }

            for (int j = 0; j < Width; j++)
                Console.Write("-----");

            Console.WriteLine();
        }
    }
}