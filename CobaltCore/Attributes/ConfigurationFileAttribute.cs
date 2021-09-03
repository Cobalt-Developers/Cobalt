using System;
using CobaltCore.Storages.Configs;

namespace CobaltCore.Attributes
{
    public class ConfigurationFileAttribute : Attribute
    {
        private string name;
        private ConfigType type;

        public ConfigurationFileAttribute(string name, ConfigType type)
        {
            this.name = name;
            this.type = type;
        }

        public string GetFileName()
        {
            return name+type.GetFileEnding();
        }
    }
}