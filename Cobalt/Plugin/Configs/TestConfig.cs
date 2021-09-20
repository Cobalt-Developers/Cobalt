using Cobalt.Api.Attribute;
using Cobalt.Api.Storage;

namespace Cobalt.Plugin.Configs
{
    [FileStorage("config", FileStorageType.YAML)]
    public class TestConfig
    {
        public string TestField { get; set; } = "Wololo";
    }
}