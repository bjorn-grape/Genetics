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
        private static Dictionary<string,Map> maps_;

        public static void InitMap()
        {
            maps_ = new Dictionary<string,Map>();
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            path += "/../../map";
            string[] files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                Map map = new Map(file);
                maps_.Add(Path.GetFileNameWithoutExtension(file), map);
            }
        }
        
    }
}