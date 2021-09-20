using System;

namespace Cobalt.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SettingsAttribute : Attribute
    {
        public Type ImplType { get; }

        public SettingsAttribute(Type implType)
        {
            ImplType = implType;
        }
    }
}