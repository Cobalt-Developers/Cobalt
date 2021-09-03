using System;

namespace CobaltCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ConfigurationAttribute : Attribute
    {
        public Type ConfigType { get; }

        public ConfigurationAttribute(Type configType)
        {
            ConfigType = configType;
        }
    }
}