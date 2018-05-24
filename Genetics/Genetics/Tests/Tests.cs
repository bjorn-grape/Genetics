using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Net;
using System.Threading;
using NUnit.Framework;

namespace Genetics.Tests
{
    [TestFixture]
    public class Part1Matrix
    {
        [Test, Timeout(100)]
        public void T0MatrixConstructor()
        {
            Matrix m = new Matrix(3, 4, true);
            var sum = 0f;
            foreach (var elm in m.Tab)
            {
                sum += elm;
            }

            Assert.AreNotEqual(0f, sum);
            sum = 0f;
            foreach (var elm in m.Bias)
            {
                sum += elm;
            }

            Assert.AreNotEqual(0f, sum);


            Assert.AreEqual(3, m.Tab.GetLength(0));
            Assert.AreEqual(4, m.Tab.GetLength(1));
            Assert.AreEqual(m.Tab.GetLength(1), m.Bias.GetLength(0));
        }

        [Test, Timeout(100)]
        public void T1MatrixCopy()
        {
            Matrix m = new Matrix(2, 5, true);

            Matrix m2 = new Matrix(0, 0);
            m2.MakeCopyFrom(m);
            Assert.AreEqual(m2.Tab.GetLength(0), m.Tab.GetLength(0));
            Assert.AreEqual(m2.Tab.GetLength(1), m.Tab.GetLength(1));
            m.Tab[0, 0] = 5f;
            m.Tab[0, 1] = 5f;
            Assert.AreNotEqual(m.Tab[0, 0], m2.Tab[0, 0]);
            Assert.AreNotEqual(m.Tab[0, 1], m2.Tab[0, 1]);
            Assert.AreEqual(m.Tab[1, 1], m2.Tab[1, 1]);
            Assert.AreEqual(m.Tab[1, 2], m2.Tab[1, 2]);

            Assert.AreEqual(m.Bias.GetLength(0), m2.Bias.GetLength(0));
            m.Bias[0] = 6f;
            Assert.AreNotEqual(m.Bias[0], m2.Bias[0]);
            Assert.AreEqual(m.Bias[1], m2.Bias[1]);
        }


        [Test, Timeout(100)]
        public void T2Mutation()
        {
            Matrix m = new Matrix(10, 10, true);
            m.ApplyMutation();
            foreach (var elm in m.Tab)
            {
                Assert.GreaterOrEqual(elm, 0);
                Assert.LessOrEqual(elm, 1);
            }
        }

        [Test, Timeout(100)]
        public void T3Sigmoid()
        {
            Assert.AreEqual(Matrix.Sigmoid(100), 1f, 0.0001f);

            Assert.AreEqual(Matrix.Sigmoid(-100), 0f, 0.0001f);

            Assert.AreEqual(Matrix.Sigmoid(0.5f), 0.622459350f, 0.00000001);

            Assert.AreEqual(Matrix.Sigmoid(0.7f), 0.668187800f, 0.00000001);

            Assert.AreEqual(Matrix.Sigmoid(0.3f), 0.5744425, 0.0000001);

            Assert.AreEqual(Matrix.Sigmoid(0f), 0.5f, 0.00001);
        }


        [Test, Timeout(100)]
        public void T4MatrixAddition([Range(-97, 17, 4)] int n)
        {
            var m = new Matrix(5, 5);
            var m2 = new Matrix(5, 5);

            var a = n;
            var b = n - 12;
            var c = n - 6;
            var d = n + 8;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    m.Tab[i, j] = i * a + j * b;
                    m2.Tab[i, j] = i * c + j * d;
                }
            }

            var result = m + m2;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(result.Tab[i, j], i * a + j * b + i * c + j * d);
                }
            }
        }

        public static Matrix PropagateRef(Matrix a, Matrix b)
        {
            Matrix C = new Matrix(a.Tab.GetLength(0), b.Tab.GetLength(1));
            for (int i = 0; i < a.Tab.GetLength(0); i++)
            {
                for (int j = 0; j < b.Tab.GetLength(1); j++)
                {
                    float summ = 0;
                    for (int k = 0; k < a.Tab.GetLength(1); k++)
                        summ += a.Tab[i, k] * b.Tab[k, j];

                    C.Tab[i, j] = Matrix.Sigmoid(summ / b.Tab.GetLength(1) + b.Bias[j]);
                }
            }

            return C;
        }

        [Test, Timeout(100)]
        public void T5MatrixMult([Range(-97, 17, 4)] int n)
        {
            var m = new Matrix(5, 5);
            var m2 = new Matrix(5, 5);

            var a = n;
            var b = n - 12;
            var c = n - 6;
            var d = n + 8;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    m.Tab[i, j] = i * a + j * b;
                    m2.Tab[i, j] = i * c + j * d;
                }
            }

            var reference = PropagateRef(m, m2);
            var result = m * m2;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(result.Tab[i, j], reference.Tab[i, j]);
                }
            }
        }
    }

    [TestFixture]
    public class Part2Factory
    {
        [Test, Timeout(100)]
        public void T0Sort()
        {
            var list = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                var ply = new Player();
                ply.SetScore((i * 337) % 20);
                list.Add(ply);
            }

            var oldList = Factory.GetListPlayer();
            Factory.SetListPlayer(list);
            Factory.SimpleSort();
            list = Factory.GetListPlayer();
            for (int i = 0; i < 9; i++)
            {
                Assert.LessOrEqual(list[i].GetScore(), list[i + 1].GetScore());
            }

            Factory.SetListPlayer(oldList);
        }

        [Test, Timeout(100)]
        public void T1GetListPlayer()
        {
            var li = new List<Player>();
            Factory.SetListPlayer(li);
            Assert.AreEqual(Factory.GetListPlayer(), li);
        }

        [Test, Timeout(100)]
        public void T2GetPlayerBest()
        {
            var li = new List<Player>();
            for (int i = 0; i < 10; i++)
            {
                var ply = new Player();
                ply.SetScore((i * 337) % 20);
                li.Add(ply);
            }

            Factory.SetListPlayer(li);
            var max = 0;
            foreach (var elm in li)
            {
                if (elm.GetScore() > max)
                {
                    max = elm.GetScore();
                }
            }

            Assert.AreEqual(Factory.GetBestPlayer().GetScore(), max);
        }

        private static Random rng = new Random();

        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        [Test, Timeout(100)]
        public void T3GetNthPlayer()
        {
            var liply = new List<Player>();
            for (int i = 0; i < 20; i++)
            {
                var nn = new Player();
                nn.SetScore(100 * i);
                liply.Add(nn);
            }

            Shuffle(liply);

            Factory.SetListPlayer(liply);
            for (int i = 0; i < 20; i++)
            {
                Assert.AreEqual(i * 100, Factory.GetNthPlayer(i).GetScore());
            }
        }

        [Test, Timeout(100)]
        public void T4SetPathLoad()
        {
            Factory.SetPathLoad("test");
            Assert.AreEqual(Factory.GetPathLoad(), "test");
            Factory.SetPathSave("testing is good");
            Assert.AreEqual(Factory.GetPathSave(), "testing is good");
            Factory.SetPathLoadAndSave("Both at the same time");
            Assert.AreEqual(Factory.GetPathLoad(), "Both at the same time");
            Assert.AreEqual(Factory.GetPathSave(), "Both at the same time");
        }

        [Test, Timeout(100)]
        public void T5SaveAndLoad()
        {
            var pathload = AppDomain.CurrentDomain.BaseDirectory + "../../Tests/test.save";
            Factory.SetPathLoadAndSave(pathload);
            Assert.AreEqual(true, File.Exists(pathload));
            Factory.Init();
            Assert.AreEqual(Factory.GetListPlayer().Count, 10);
            Factory.SaveState();
            Factory.Init();
            Assert.AreEqual(Factory.GetListPlayer().Count, 10);
        }

        [Test, Timeout(100)]
        public void T6InitNew()
        {
            Factory.InitNew(49);
            Assert.AreEqual(49, Factory.GetListPlayer().Count);

            Factory.InitNew(27);
            Assert.AreEqual(27, Factory.GetListPlayer().Count);
        }

        [Test, Timeout(100)]
        public void T7Init()
        {
            Factory.SetPathLoad("nothing to see here");
            Factory.Init();
            Assert.AreEqual(200, Factory.GetListPlayer().Count);
        }

        [Test, Timeout(100)]
        public void T8Print()
        {
            var pathload = AppDomain.CurrentDomain.BaseDirectory + "../../Tests/test.save";
            Factory.SetPathLoadAndSave(pathload);
            Assert.AreEqual(true, File.Exists(pathload));
            Factory.Init();

            string student;
            var reference = "";

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Factory.PrintScore();
                student = sw.ToString();
                sw.Flush();
                sw.Close();
            }

            for (int i = 0; i < 10; i++)
            {
                reference += "Player " + i + " has a score of 100" + Environment.NewLine;
            }

            Assert.AreEqual(reference, student);
        }

        [Test, Timeout(3000)]
        public void T9Train()
        {
            RessourceLoad.GenerateMap(3, 10, 30, 10);
            RessourceLoad.SetCurrentMap("generatedMap_1");
            Factory.InitNew(40);
            var old = Factory.GetListPlayer();
            var oldlist = new List<Player>();
            foreach (var elm in old)
            {
                oldlist.Add(elm);
            }

            Factory.Train(1,false);
            int counter = 0;
            old = Factory.GetListPlayer();
            for (int i = 0; i < 40; i++)
            {
                    if (old[i].Getbrains()[0].Tab[3,3] == oldlist[i].Getbrains()[0].Tab[3,3])
                        counter++;
            }

            Assert.AreNotEqual(40, counter);
        }

        [Test, Timeout(3000)]
        public void T9XRegenerate() // For alphabetical reasons
        {
            
            RessourceLoad.SetCurrentMap("generatedMap_1");
            Factory.InitNew(40);
            var old = Factory.GetListPlayer();
            var oldlist = new List<Player>();
            foreach (var elm in old)
            {
                oldlist.Add(elm);
            }

            Factory.Train(1,false);
            int counter = 0;
            old = Factory.GetListPlayer();
            for (int i = 0; i < 40; i++)
            {
                if (old[i].Getbrains()[0].Tab[3,3] == oldlist[i].Getbrains()[0].Tab[3,3])
                    counter++;
            }

            Assert.AreNotEqual(40, counter);
        }
    }
}