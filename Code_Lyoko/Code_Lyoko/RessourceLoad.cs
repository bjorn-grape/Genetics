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
        private static string map_path_;
        private static string img_path;
        public static List<Map> maps_;

        public static void InitMap()
        {
            maps_ = new List<Map>();
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            path += "/../../map";
            if (!Directory.Exists(path))
                throw new Exception("Directory Doesn't exist");
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                Map map = new Map(file);
                maps_.Add(map);
                Console.WriteLine("Loaded map : " + Path.GetFileNameWithoutExtension(file));
            }
        }

        /// <summary>
        /// Used for Debugging
        /// </summary>
        public static void PrintMaps()
        {
            foreach (var map in maps_)
            {
                map.Print();
            }
        }

        private static SpriteBatch sprt_;
        private static GraphicsDeviceManager graphics_;
        private static Dictionary<string, Appearance> Dico_;
        private static string base_path_;

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
            base_path_ = System.AppDomain.CurrentDomain.BaseDirectory;
            base_path_ += "../../img/";
            Dico_ = dico;
            graphics_ = graphics;

            GiveApperanceFromPath("player/Aelita/Aelita idle.png", 16, 2);
            GiveApperanceFromPath("player/Aelita/Aelita move.png", 14);
            GiveApperanceFromPath("player/Aelita/Aelita death.png", 16, 2);
            GiveApperanceFromPath("background/tiles.png", 10, 1, 32);
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
            string path = base_path_ + link;
            //Console.WriteLine(path);
            //Console.WriteLine("File : " + File.Exists(path));
            if (!File.Exists(path))
                throw new Exception("Image " + path + "Not found");
            else
            {
                Console.WriteLine("Image " + path + " found");
            }

            FileStream fileStream = new FileStream(path, FileMode.Open);
            Texture2D plop = Texture2D.FromStream(graphics_.GraphicsDevice, fileStream);
            if (plop == null)
                throw new Exception("Can't load 2D texture ");
            fileStream.Dispose();
            Appearance tmp = new Appearance(ref plop, cols, rows, width);
            if (tmp.get_texture() == null)
                throw new Exception("Texture 2D is null ");
            Console.WriteLine("Added \"" + Path.GetFileName(path) + "\"");
            Dico_.Add(Path.GetFileName(path), tmp);
        }
    }
}