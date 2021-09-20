using Cobalt.Api.Attributes;
using Cobalt.Api.Storages;

namespace Cobalt.Plugin.Configs
{
    [FileStorage("config", FileStorageType.YAML)]
    public class TestConfig
    {
        public string TestField { get; set; } = "Wololo";
    }
}