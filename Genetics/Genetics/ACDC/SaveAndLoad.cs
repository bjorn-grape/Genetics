using System;
using System.Collections.Generic;
using System.IO;

namespace Genetics
{
    public static class SaveAndLoad
    {
        public static void Save(string path, List<Player> listPlayer)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(listPlayer.Count.ToString());

            foreach (var player in listPlayer)
            {
                sw.WriteLine(player.GetScore());


                foreach (var mat in player.Getbrains())
                {
                    foreach (var bia in mat.Bias)
                    {
                        sw.Write(bia + "|");
                    }
                    sw.WriteLine();
                    foreach (var elm in mat.Tab)
                    {
                        sw.Write(elm + "|");
                    }

                    sw.WriteLine();
                }
            }

            sw.Flush();
            sw.Close();
        }

        public static List<Player> Load(string path)
        {
            StreamReader sr = new StreamReader(path);
            int size = Convert.ToInt32(sr.ReadLine());
            List<Player> listPlayer = new List<Player>();

            for (int i = 0; i < size; i++)
            {
                int score = Convert.ToInt32(sr.ReadLine());

                int[] listDim =
                {
                    49, 16,
                    16, 16,
                    16, 4
                };

                List<Matrix> liMat = new List<Matrix>();
                for (int brainIndex = 0; brainIndex < 3; brainIndex++)
                {
                    var li = sr.ReadLine()?.Split('|');
                    int index = 0;
                    int dimHeight = listDim[brainIndex * 2];
                    int dimWidth = listDim[brainIndex * 2 + 1];
                    var m1 = new Matrix(dimHeight, dimWidth);
                     
                    for (int k = 0; k < dimWidth; k++)
                    {
                        m1.Bias[k] = Convert.ToSingle(li?[index++]);
                    }
                    li = sr.ReadLine()?.Split('|');
                    index = 0;
                    for (int j = 0; j < dimHeight; j++) // size brain 1
                    {
                        for (int k = 0; k < dimWidth; k++)
                        {
                            m1.Tab[j, k] = Convert.ToSingle(li?[index++]);
                        }
                    }

                    liMat.Add(m1);
                }

                var plyy = new Player(liMat);
                plyy.SetScore(score);
                listPlayer.Add(plyy);
            }

            sr.Close();
            return listPlayer;
        }
    }
}