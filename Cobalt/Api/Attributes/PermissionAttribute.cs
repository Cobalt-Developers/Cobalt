using System;

namespace Cobalt.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PermissionAttribute : Attribute
    {
        public string Name { get; }

        public PermissionAttribute(string name)
        {
            Name = name;
        }
    }
}