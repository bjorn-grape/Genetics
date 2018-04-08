using System;
using System.Collections.Generic;
using System.Runtime.Remoting;

namespace Code_Lyoko
{
    public class Matrix
    {
        #region Attributes

        private int Height;
        private int Width;
        public float[,] Tab;
        public float Bias;
        private static readonly Random Rdn = new Random();

        #endregion

        #region Constructors

        public Matrix(int height, int width, bool init = false)
        {
            Height = height;
            Width = width;
            Tab = new float[height, width];
            if (!init) return;

            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    Tab[i, j] = (float) Rdn.Next(100) / 100;


            Bias = (float) Rdn.Next(100) / 200 - 1f;
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
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tab[i, j] = copy.Tab[i, j];
                }
            }

            Bias = copy.Bias;
        }



        public void Applymutation()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    float k = (float) Rdn.Next(10) / 100 - 0.05f;
                    Tab[i, j] += k;
                    Tab[i, j] = sigmoid(Tab[i, j]);
                }
            }

            Bias += (float) Rdn.Next(10) / 100 - 0.05f;
            Bias = sigmoid(Bias);
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

        private static float sigmoid(float x)
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

                    C.Tab[i, j] = sigmoid(summ / b.Width + b.Bias); // this is not multiplication
                }
            }

            return C;
        }


        public void Print()
        {
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