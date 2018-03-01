using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Code_Lyoko
{
    public class RessourceLoad
    {
        private static string _mapPath;
        private static string _imgPath;
        public static List<Map> maps_ = new List<Map>();
        static int CurrentMap = 0;

        public static void InitMap()
        {
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            path += "/../../map";
            if (!Directory.Exists(path))
                throw new Exception("Directory Doesn't exist");
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                Map map = new Map(file);
                maps_.Add(map);
                Console.WriteLine("Loaded map: " + Path.GetFileNameWithoutExtension(file));
            }
        }

        public static Map GetCurrentMap()
        {
            return maps_[CurrentMap];
        }

        public static bool SetNextMap()
        {
            if (CurrentMap + 1 < maps_.Count)
            {
                CurrentMap++;
                return true;
            }

            return false;
        }


        public static void GenerateMap(int nb, uint height, uint length, int mutation)
        {
            if (height < 4 || length < 10)
                throw new ArgumentException("height < 4 || length < 10");
            if (mutation > 100)
                mutation = 100;
            if (mutation < 0)
                mutation = 0;

            uint groundMax = (height - 2);
            uint groundCurrent = groundMax / 2;
            Random rnd = new Random();

            for (int i = 0; i < nb; i++)
            {
                char[,] tmp = new char[height, length];
                for (int j = 0; j < height; j++)
                {
                    tmp[j, 0] = 'W';
                }

                for (int k = 1; k < length - 1; k++)
                {
                    tmp[0, k] = 'W';
                    if (rnd.Next(0, 100) < mutation)
                    {
                        int act = rnd.Next(0, 2);
                        switch (act)
                        {
                            case 0:
                                if (groundCurrent < groundMax)
                                    groundCurrent++;
                                break;
                            case 1:
                                if (groundCurrent > 3)
                                    groundCurrent--;
                                break;
                        }
                    }

                    for (uint j = height - 2; j > 0; j--)
                    {
                        if (j > groundCurrent)
                            tmp[j, k] = 'W';
                        else
                            tmp[j, k] = ' ';
                    }

                    tmp[height - 1, k - 1] = 'W';
                }

                for (int j = 0; j < height; j++)
                {
                    tmp[j, length - 2] = 'D';
                    tmp[j, length - 1] = 'W';
                }

                Console.WriteLine("Added map " + (i + 1) + "/" + nb);
                for (int j = 0; j < height; j++)
                {
                    for (int k = 0; k < length; k++)
                    {
                        Console.Write(tmp[j, k]);
                    }

                    Console.Write('\n');
                }

                Map map = new Map(tmp, height, length);
                maps_.Add(map);
            }
        }

        /// <summary>
        /// Used for Debugging
        /// </summary>
        private static SpriteBatch _sprt;

        private static GraphicsDeviceManager _graphics;
        private static Dictionary<string, Appearance> _dico;
        private static string _basePath;

        /// <summary>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="dico">Dictionary to be filled, it must be initialized before</param>
        /// <exception cref="ArgumentNullException">Arguments must not be null</exception>
        public static void SetApperance(GraphicsDeviceManager graphics,
            ref Dictionary<string, Appearance> dico)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));
            if (dico == null) throw new ArgumentNullException(nameof(dico));
            _basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            _basePath += "../../img/";
            _dico = dico;
            _graphics = graphics;

            GiveApperanceFromPath("player/Aelita/Aelita idle.png", 16, 2, 64);
            GiveApperanceFromPath("player/Aelita/Aelita move.png", 14, 1, 64);
            GiveApperanceFromPath("player/Aelita/Aelita move red.png", 14, 1, 64);
            GiveApperanceFromPath("player/Aelita/Aelita death.png", 16, 2, 64);
            GiveApperanceFromPath("background/tiles.png", 10, 1, 32);
            GiveApperanceFromPath("background/tiles large.png", 10, 1, 64);
        }

        /// <summary>
        /// Load an image and throw an Exeption in case of error.
        /// </summary>
        /// <param name="link">Path related to img folder</param>
        /// <param name="cols">Number of columns of the image to be loaded</param>
        /// <param name="rows">Number of rows of the image to be loaded</param>
        /// <param name="width">Width and height of the image/param>
        /// <exception cref="Exception"></exception>
        static void GiveApperanceFromPath(string link, int cols = 16, int rows = 1, int width = 128)
        {
            string path = _basePath + link;
            //Console.WriteLine(path);
            //Console.WriteLine("File : " + File.Exists(path));
            if (!File.Exists(path))
                throw new Exception("Image " + path + "Not found");
            else
            {
                Console.WriteLine("Image " + path + " found");
            }

            FileStream fileStream = new FileStream(path, FileMode.Open);
            Texture2D plop = Texture2D.FromStream(_graphics.GraphicsDevice, fileStream);
            if (plop == null)
                throw new Exception("Can't load 2D texture ");
            fileStream.Dispose();
            Appearance tmp = new Appearance(ref plop, cols, rows, width);
            if (tmp.get_texture() == null)
                throw new Exception("Texture 2D is null ");
            Console.WriteLine("Added \"" + Path.GetFileName(path) + "\"");
            _dico.Add(Path.GetFileName(path), tmp);
        }
    }
}