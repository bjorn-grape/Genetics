using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace Genetics.Tests
{
    [TestFixture]
    public class TestsMatrix
    {
        [Test, Timeout(100)] 
        public void TestMatrixConstructor()
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
        public void TestMatrixCopy()
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
        public void TestMutation()
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
        public void TestSigmoid()
        {
            Assert.GreaterOrEqual(Matrix.Sigmoid(100), 0.999f);
            Assert.LessOrEqual(1f, Matrix.Sigmoid(100));

            Assert.GreaterOrEqual(Matrix.Sigmoid(-100), 0f);
            Assert.LessOrEqual(Matrix.Sigmoid(-100), 0.0001f);

            Assert.GreaterOrEqual(Matrix.Sigmoid(0.5f), 0.622459340f);
            Assert.LessOrEqual(Matrix.Sigmoid(0.5f), 0.622459360f);

            Assert.GreaterOrEqual(Matrix.Sigmoid(0.7f), 0.668187790f);
            Assert.LessOrEqual(Matrix.Sigmoid(0.7f), 0.668187810f);

            Assert.GreaterOrEqual(Matrix.Sigmoid(0.3f), 0.5744424);
            Assert.LessOrEqual(Matrix.Sigmoid(0.3f), 0.5744426);

            Assert.GreaterOrEqual(Matrix.Sigmoid(0f), 0.4999);
            Assert.LessOrEqual(Matrix.Sigmoid(0f), 0.50001);
        }


        [Test, Timeout(100)]
        public void TestMatrixAddition([Range(-97, 17, 4)] int n)
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
    }

    [TestFixture]
    public class TestsFactory
    {
        [Test, Timeout(100)]
        public void TestSort()
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
            for (int i = 0; i < 9; i++)
            {
                Assert.LessOrEqual(list[i].GetScore(), list[i + 1].GetScore());
            }

            Factory.SetListPlayer(oldList);
        }

        [Test, Timeout(100)]
        public void TestGettersList()
        {
            var li = new List<Player>();
            Factory.SetListPlayer(li);
            Assert.AreEqual(Factory.GetListPlayer(), li);
        }

        [Test, Timeout(100)]
        public void TestGettersPlayerBest()
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

        [Test, Timeout(100)]
        public void TestSetters()
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
        public void TestSaveAndLoad()
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
        public void TestInit()
        {
            Factory.InitNew(49);
            Assert.AreEqual(49, Factory.GetListPlayer().Count);

            Factory.InitNew(27);
            Assert.AreEqual(27, Factory.GetListPlayer().Count);

            Factory.SetPathLoad("nothing to see here");
            Factory.Init();
            Assert.AreEqual(200, Factory.GetListPlayer().Count);
        }

        [Test, Timeout(100)]
        public void TestPrint()
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
    }
}