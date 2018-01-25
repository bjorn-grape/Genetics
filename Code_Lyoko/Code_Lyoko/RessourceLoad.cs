using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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
    }
}