using System;

namespace Cobalt.Api.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SettingsAttribute : System.Attribute
    {
        public Type ImplType { get; }

        public SettingsAttribute(Type implType)
        {
            ImplType = implType;
        }
    }
}