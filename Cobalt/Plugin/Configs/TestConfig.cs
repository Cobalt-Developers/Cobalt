using CobaltCore.Attributes;
using CobaltCore.Storages.Configs;

namespace Cobalt.Plugin.Configs
{
    [FileStorage("config", FileStorageType.YAML)]
    public class TestConfig
    {
        public string TestField { get; set; } = "Wololo";
    }
}