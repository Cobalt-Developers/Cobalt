using System.IO;

namespace Cobalt.Loader
{
    public static class CobaltMain
    {
        public static string PluginFolder => Path.GetFullPath("plugins");
        public static string DataFolder => Path.GetFullPath("data");
        
        public static PluginLoader PluginLoader { get; private set; }

        public static void Initialize()
        {
            PluginLoader = new PluginLoader();
        }
    }
}