using System;

namespace CobaltCore.Storages.Configs
{
    public enum ConfigType
    {
        YAML
    }

    public static class ConfigTypeExtension
    {
        public static string GetFileEnding(this ConfigType type)
        {
            switch (type)
            {
                case ConfigType.YAML:
                    return ".yaml";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}