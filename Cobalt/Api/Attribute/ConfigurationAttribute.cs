using System;

namespace Cobalt.Api.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ConfigurationAttribute : System.Attribute
    {
        public Type ImplType { get; }

        public ConfigurationAttribute(Type implType)
        {
            ImplType = implType;
        }
    }
}