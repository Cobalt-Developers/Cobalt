using System;

namespace CobaltCore.Storages.Configs
{
    public enum FileStorageType
    {
        YAML
    }

    public static class ConfigTypeExtension
    {
        public static string GetFileEnding(this FileStorageType type)
        {
            switch (type)
            {
                case FileStorageType.YAML:
                    return ".yaml";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}