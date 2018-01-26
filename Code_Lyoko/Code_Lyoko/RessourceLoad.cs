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
        private static Dictionary<string, Map> maps_;

        public static void InitMap()
        {
            maps_ = new Dictionary<string, Map>();
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            path += "/../../map";
            if (!Directory.Exists(path))
                throw new Exception("Directory Doesn't exist");
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                Map map = new Map(file);
                maps_.Add(Path.GetFileNameWithoutExtension(file), map);
                Console.WriteLine("Loaded map : " + Path.GetFileNameWithoutExtension(file));
            }
        }

        public static void PrintMaps()
        {
            foreach (var map in maps_)
            {
                map.Value.Print();
            }
        }

        private static SpriteBatch sprt_;
        private static GraphicsDeviceManager graphics_;
        private static Dictionary<string, Appearance> Dico_;
        private static string base_path_;
        
        public static void SetApperance(SpriteBatch sprt, GraphicsDeviceManager graphics,
            Dictionary<string, Appearance> dico)
        {
            base_path_ = System.AppDomain.CurrentDomain.BaseDirectory;
            base_path_ += "../../img/";
            dico = new Dictionary<string, Appearance>();
            Dico_ = dico;
            sprt_ = sprt;
            graphics_ = graphics;
            
            giveApperanceFromPath("player/Aelita/Aelita_attack.png");
        }

        static void giveApperanceFromPath(string link)
        {
            string path = base_path_ + link;
            //Console.WriteLine(path);
            //Console.WriteLine("File : " + File.Exists(path));
            if(!File.Exists(path))
                throw new Exception("Image " + path + "Not found");
            FileStream fileStream = new FileStream(path, FileMode.Open);
            Texture2D plop = Texture2D.FromStream(graphics_.GraphicsDevice, fileStream);
            fileStream.Dispose();
            Appearance tmp = new Appearance(sprt_, plop);
            Console.WriteLine("Added \"" + Path.GetFileName(path) + "\"");
            Dico_.Add( "1",tmp);

        }
    }
}