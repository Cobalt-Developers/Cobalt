using System;
using System.IO;

namespace Cobalt.Loader
{
    public static class CobaltMain
    {
        public static string CobaltFolder => Path.GetFullPath("cobalt");
        public static string PluginFolder => Path.Combine(CobaltFolder, "plugins");
        public static string DataFolder => Path.Combine(CobaltFolder, "data");
        
        public static PluginLoader PluginLoader { get; private set; }

        public static void Initialize()
        {
            PluginLoader = new PluginLoader();
        }
    }
}