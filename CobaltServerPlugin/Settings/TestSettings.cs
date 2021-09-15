using CobaltCore.Attributes;
using CobaltCore.Storages.Configs;

namespace CobaltServerPlugin.Storage
{
    [FileStorage("test", FileStorageType.YAML)]
    public class TestSettings
    {
        public string TestField { get; set; } = "Wololo";
    }
}